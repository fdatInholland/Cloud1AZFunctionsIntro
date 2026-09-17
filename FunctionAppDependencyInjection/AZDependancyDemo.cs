using FunctionAppDependencyInjection.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FunctionAppDependencyInjection;

public class AZDependancyDemo
{
    private readonly ILogger<AZDependancyDemo> _logger;
    private readonly IOrderService _orderService;
    public AZDependancyDemo(ILogger<AZDependancyDemo> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    [Function("AZDependancyDemo")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        string inputString = req.Query["customerid"];

        if (string.IsNullOrEmpty(inputString))
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            if (!string.IsNullOrEmpty(requestBody))
            {
                var data = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(requestBody);
                if (data != null && data.ContainsKey("customerid"))
                {
                    inputString = data["customerid"];
                }
                else
                {
                    inputString = requestBody;
                }
            }
        }

        if (string.IsNullOrEmpty(inputString))
        {
            return new BadRequestObjectResult("Please pass a string in the query string or request body.");
        }
        return new OkObjectResult(_orderService.GetAllOrdersByCustomerID(inputString));
    }
}