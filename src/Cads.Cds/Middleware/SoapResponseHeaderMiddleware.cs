using System.Text;
using System.Xml;

namespace Cads.Cds.Middleware;

/// <summary>
/// Middleware to inject custom headers into SOAP responses
/// </summary>
public class SoapResponseHeaderMiddleware
{
    private readonly RequestDelegate _next;

    public SoapResponseHeaderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Only process SOAP requests
        if (!context.Request.Path.StartsWithSegments("/api/soap"))
        {
            await _next(context);
            return;
        }

        // Capture the response
        var originalBodyStream = context.Response.Body;

        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        try
        {
            await _next(context);

            // Only modify successful SOAP responses
            if (context.Response.StatusCode == 200 &&
                context.Response.ContentType?.Contains("xml") == true)
            {
                responseBody.Seek(0, SeekOrigin.Begin);
                var responseContent = await new StreamReader(responseBody).ReadToEndAsync();

                // Inject headers into SOAP envelope
                var modifiedResponse = InjectSoapHeaders(responseContent, context);

                context.Response.Body = originalBodyStream;
                context.Response.ContentLength = Encoding.UTF8.GetByteCount(modifiedResponse);
                await context.Response.WriteAsync(modifiedResponse);
            }
            else
            {
                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
        finally
        {
            context.Response.Body = originalBodyStream;
        }
    }

    private string InjectSoapHeaders(string soapResponse, HttpContext context)
    {
        try
        {
            var doc = new XmlDocument();
            doc.LoadXml(soapResponse);

            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("soap", "http://www.w3.org/2003/05/soap-envelope");
            nsmgr.AddNamespace("s12", "http://schemas.xmlsoap.org/soap/envelope/");

            // Try both SOAP 1.2 namespaces
            var headerNode = doc.SelectSingleNode("//soap:Header", nsmgr)
                          ?? doc.SelectSingleNode("//s12:Header", nsmgr);
            var bodyNode = doc.SelectSingleNode("//soap:Body", nsmgr)
                        ?? doc.SelectSingleNode("//s12:Body", nsmgr);

            if (headerNode == null && bodyNode != null)
            {
                // Create header node if it doesn't exist
                var envelopeNode = bodyNode.ParentNode;
                headerNode = doc.CreateElement("Header", bodyNode.NamespaceURI);
                envelopeNode?.InsertBefore(headerNode, bodyNode);
            }

            if (headerNode != null)
            {
                // Extract client request ID from original request if stored
                var clientRequestId = context.Items["ClientRequestId"]?.ToString()
                                   ?? Guid.NewGuid().ToString();
                var serverTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

                // Add context header
                var contextElement = doc.CreateElement("context", "http://types.defra.gov.uk/context");

                var requestContextElement = doc.CreateElement("requestContext", "http://types.defra.gov.uk/context");
                requestContextElement.SetAttribute("clientRequestId", clientRequestId);
                requestContextElement.SetAttribute("serverTimestamp", serverTimestamp);

                contextElement.AppendChild(requestContextElement);
                headerNode.AppendChild(contextElement);
            }

            return doc.OuterXml;
        }
        catch
        {
            // If we can't parse/modify, return original
            return soapResponse;
        }
    }
}