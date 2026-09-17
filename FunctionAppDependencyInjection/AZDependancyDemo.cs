using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionAppDependencyInjection;

public class AZDependancyDemo
{
    private readonly ILogger<AZDependancyDemo> _logger;

    public AZDependancyDemo(ILogger<AZDependancyDemo> logger)
    {
        _logger = logger;
    }

    [Function("AZDependancyDemo")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}