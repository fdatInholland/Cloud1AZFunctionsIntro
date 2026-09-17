using FunctionAppQueueDemo.Domain;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FunctionAppQueueDemo
{
    public class QueueProcessorFunction
    {
        private readonly ILogger<QueueProcessorFunction> _logger;

        public QueueProcessorFunction(ILogger<QueueProcessorFunction> logger)
        {
            _logger = logger;
        }

        [Function("QueueDemo")]
        public async Task Run([QueueTrigger("orders-queue", Connection = "AzureWebJobsStorage")] WorkItem item)
        {
            _logger.LogInformation("Processing work item ID: {Id} created at {CreatedAt}", item.Id, item.CreatedAt);

            if (string.IsNullOrWhiteSpace(item.Data))
            {
                _logger.LogWarning("Work item {Id} contained empty data", item.Id);
                return;
            }

            // Perform async work (e.g., database operation, external API call)
            await ProcessWorkItemAsync(item);

            _logger.LogInformation("Successfully processed work item ID: {Id}", item.Id);
        }

        private Task ProcessWorkItemAsync(WorkItem item)
        {
            // Business logic goes here
            return Task.CompletedTask;
        }
    }
}
