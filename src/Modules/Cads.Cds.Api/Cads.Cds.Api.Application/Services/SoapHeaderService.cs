using Microsoft.AspNetCore.Http;
using System.Xml;

namespace Cads.Cds.Api.Application.Services;

/// <summary>
/// Service for extracting and processing SOAP headers
/// </summary>
public class SoapHeaderService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SoapHeaderService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public (string systemUser, string endUser, string clientRequestId) ExtractHeaders()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            return (string.Empty, string.Empty, string.Empty);
        }

        string systemUser = string.Empty;
        string endUser = string.Empty;
        string clientRequestId = string.Empty;

        // Try to read from items (SoapCore may store headers here)
        if (httpContext.Items.TryGetValue("SoapHeaders", out var headersObj) && headersObj is XmlDocument doc)
        {
            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("wsse", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
            nsmgr.AddNamespace("ctx", "http://types.defra.gov.uk/context");
            nsmgr.AddNamespace("soap", "http://www.w3.org/2003/05/soap-envelope");

            // Extract system user (role="system")
            var systemSecurityNode = doc.SelectSingleNode("//wsse:Security[@soap:role='system']/wsse:UsernameToken/wsse:Username", nsmgr);
            if (systemSecurityNode != null)
            {
                systemUser = systemSecurityNode.InnerText;
            }

            // Extract end user (role="user")
            var userSecurityNode = doc.SelectSingleNode("//wsse:Security[@soap:role='user']/wsse:UsernameToken/wsse:Username", nsmgr);
            if (userSecurityNode != null)
            {
                endUser = userSecurityNode.InnerText;
            }

            // Extract client request ID
            var contextNode = doc.SelectSingleNode("//ctx:requestContext/@clientRequestId", nsmgr);
            if (contextNode != null)
            {
                clientRequestId = contextNode.Value ?? string.Empty;
                // Store in HttpContext for response header generation
                httpContext.Items["ClientRequestId"] = clientRequestId;
            }
        }

        return (systemUser, endUser, clientRequestId);
    }
}