using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace FunctionAppHttpTriggerSaveToQueue
{
    public class HttpTriggerDemo
    {
        private readonly ILogger<HttpTriggerDemo> _logger;

        // Use Constructor Dependency Injection for logging
        public HttpTriggerDemo(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HttpTriggerDemo>();
        }

        [Function("HttpTriggerDemo")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            // Create an HTTP response with a 200 OK status
            var response = req.CreateResponse(HttpStatusCode.OK);

            // Set content type and write the string body
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            await response.WriteStringAsync("Boeie");

            return response;
        }
    }
}
