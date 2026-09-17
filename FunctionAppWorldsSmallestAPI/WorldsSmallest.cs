using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace FunctionAppWorldsSmallestAPI
{
    //Is this an web API endpoint?
    //is it a microservice?
    //bursty traffic
    public class WorldsSmallest
    {
        private readonly ILogger<WorldsSmallest> _logger;

        public WorldsSmallest(ILogger<WorldsSmallest> logger)
        {
            _logger = logger;
        }

        [Function("WorldsSmallest")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            await response.WriteStringAsync("Welcome to Azure Functions!");

            return response;
        }
    }
}
