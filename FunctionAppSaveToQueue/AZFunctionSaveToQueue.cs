using FunctionAppQueueTriggerSaveToQueue.DTO;
using FunctionAppQueueTriggerSaveToQueue.Extensions;
using FunctionAppQueueTriggerSaveToQueue.Models;
using FunctionAppSaveToQueue.DTO;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace FunctionAppSaveToQueue
{
    public class AzFunctionSaveToQueue
    {
        private const decimal TaxRate = 0.18m;

        [Function("ProcessOrderAndEnqueue")]
        [QueueOutput("ordersprocessed", Connection = "AzureWebJobsStorage")]
        public async Task<string> RunAsync(
            [QueueTrigger("qdemo", Connection = "AzureWebJobsStorage")] string rawQueueItem,
            FunctionContext context)
        {
            var logger = context.GetLogger<AzFunctionSaveToQueue>();
            logger.LogInformation("Processing raw queue item: {RawData}", rawQueueItem);

            if (string.IsNullOrWhiteSpace(rawQueueItem))
            {
                logger.LogWarning("Received empty or whitespace payload from 'qdemo'.");
                throw new ArgumentNullException(nameof(rawQueueItem), "Queue payload cannot be empty.");
            }

            try
            {
                var order = JsonSerializer.Deserialize<OrderRequestDTO>(rawQueueItem, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (order == null || string.IsNullOrEmpty(order.OrderId))
                {
                    logger.LogError("Failed to deserialize payload into a valid OrderRequest.");
                    throw new InvalidOperationException("Invalid order data schema.");
                }

                // Simulate processing logic (e.g. external API calls, DB updates)
                await Task.Delay(100);

                TaxRates taxRates = TaxRates.High;

                var processedResult = new ProcessedOrderResponseDTO
                {
                    OrderId = order.OrderId,
                    Status = OrderStatus.Succes.ToString(),
                    TotalAmountWithTax = order.Amount * (1 + taxRates.GetRate()),
                    ProcessedAt = DateTime.UtcNow
                };

                logger.LogInformation("Order {OrderId} successfully processed for Customer {CustomerId}.",
                    order.OrderId, order.CustomerId);

                // Serialize output payload to JSON for the 'ordersprocessed' queue
                return JsonSerializer.Serialize(processedResult);
            }
            catch (JsonException ex)
            {
                logger.LogError(ex, "JSON Deserialization failed for payload: {RawData}", rawQueueItem);
                throw; // Rethrowing ensures message is handled by Poison Queue after max retry attempts
            }
        }
    }
}


