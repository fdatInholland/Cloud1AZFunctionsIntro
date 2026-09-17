using System;

namespace FunctionAppQueueTriggerSaveToQueue.DTO
{
    public class ProcessedOrderResponseDTO
    {
        public string OrderId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmountWithTax { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}
