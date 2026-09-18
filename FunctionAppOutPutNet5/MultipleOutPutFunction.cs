using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace FunctionAppOutPutNet5
{
    public  class MultipleOutputFunction
    {
        private readonly ILogger<MultipleOutputFunction> _logger;

        public MultipleOutputFunction(ILogger<MultipleOutputFunction> logger)
        {
            _logger = logger;
        }

        [Function("MultipleOutputFunction")]
        public async Task<MultipleOutputType> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            _logger.LogInformation("Processing multiple output trigger request.");

            using var reader = new StreamReader(req.Body);
            string requestBody = await reader.ReadToEndAsync();

            if (string.IsNullOrWhiteSpace(requestBody))
            {
                return new MultipleOutputType
                {
                    Result = new BadRequestObjectResult("Please pass a string in the request body."),
                    QueueMessage = null
                };
            }

            return new MultipleOutputType
            {
                // Returns HTTP 200 OK with message
                Result = new OkObjectResult($"Successfully queued message: {requestBody}"),

                // Sends the payload to the Azure Queue
                QueueMessage = requestBody
            };
        }
    }

    // Class defining the combined outputs
    public class MultipleOutputType
    {
        // The HTTP response bound to ASP.NET Core IActionResult -- hmm?
        public IActionResult Result { get; set; }

        // Output binding pushing the payload to Azure Storage Queue
        [QueueOutput("test-queue", Connection = "AzureWebJobsStorage")]
        public string QueueMessage { get; set; }
    }
}
