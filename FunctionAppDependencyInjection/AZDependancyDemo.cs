using FunctionAppDependencyInjection.DTO;
using FunctionAppDependencyInjection.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
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

    private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };


    //Postman: http://localhost:7071/api/AZDependancyDemo/?customerid=CUST-8041
    [Function("AZDependancyDemo")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {

        //e.g. "CUST-8041"
        string customerId = null;

        _logger.LogInformation("Processing customer orders request.");

        // 1. Check Query String
        string queryVal = req.Query.Get("customerId");
        if (!string.IsNullOrEmpty(queryVal))
        {
            customerId = queryVal;
        }

        // 2. Fallback to Body for POST requests using strongly typed DTO
        if (string.IsNullOrEmpty(customerId) && req.Body.CanRead)
        {
            try
            {
                var requestData = await JsonSerializer.DeserializeAsync<CustomerRequest>(req.Body, JsonOptions);
                customerId = requestData?.CustomerId;
            }
            catch (JsonException ex)
            {
                _logger.LogWarning(ex, "Failed to deserialize JSON request body.");

                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Please provide a valid customerId.");
                return badResponse;
            }
        }

        // 3. Validation Guard Clause
        if (string.IsNullOrEmpty(customerId))
        {
            var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
            await badResponse.WriteStringAsync("Please provide a valid customerId.");
            return badResponse;
        }

        // 4. Return Orders
        var orders = await _orderService.GetAllOrdersByCustomerID(customerId);
        var okResponse = req.CreateResponse(HttpStatusCode.OK);
        await okResponse.WriteAsJsonAsync(orders);

        return okResponse;
    }
}