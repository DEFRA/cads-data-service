using Cads.Cds.BuildingBlocks.Infrastructure.Authentication.Configuration;
using Cads.Cds.SystemAdmin.Infrastructure.GraphQL.Contexts;
using Cads.Cds.SystemAdmin.Infrastructure.Persistance.Contexts;
using EntityGraphQL;
using EntityGraphQL.Schema;
using EntityGraphQL.Schema.QueryLimits;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cads.Cds.SystemAdmin.Controllers;

[ApiController]
//[Authorize(Policy = AuthenticationConstants.ApiKeyOrCognitoPolicy)]
[Route("api/v1/[controller]")]
public class GraphQLController : ControllerBase
{
    private readonly SchemaProvider<GraphQLDbContext> _schemaProvider;
    private ILogger<GraphQLController> _logger;

    private GraphQLDbContext _dbContext;

    public GraphQLController(SchemaProvider<GraphQLDbContext> schemaProvider, GraphQLDbContext dbContext, ILogger<GraphQLController> logger)
    {
        _schemaProvider = schemaProvider;
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<object> Post([FromBody] QueryRequest query)
    {
        var options = new ExecutionOptions
        { 
            IncludeQueryInfo = false,
            MaxQueryDepth = 10,
            MaxFieldSelections = 500,
            MaxFieldAliases = 30,
            MaxQueryComplexity = 1000,
            QueryLimitsMode = QueryLimitsMode.ReportOnly,
            OnQueryLimitExceeded = ctx =>
                _logger.LogWarning("GraphQL query limit {Limit} exceeded: {Actual} > {Maximum} (operation {Operation})",
           ctx.Limit, ctx.Actual, ctx.Maximum, ctx.OperationName),
        };

        var results = await _schemaProvider.ExecuteRequestAsync(query, HttpContext.RequestServices, HttpContext.User, options);

        // gql compile errors show up in results.Errors
        return results;
    }

    [HttpGet()]
    public ContentResult Get()
    {
        return Content(_schemaProvider.ToGraphQLSchemaString());
    }
}