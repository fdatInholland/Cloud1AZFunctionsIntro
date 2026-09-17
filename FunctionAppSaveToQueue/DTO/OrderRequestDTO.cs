using System;

namespace FunctionAppSaveToQueue.DTO
{
    public class OrderRequestDTO
    {
        public string OrderId { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
