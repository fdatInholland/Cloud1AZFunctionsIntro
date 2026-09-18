namespace FunctionAppDependencyInjection.DTO
{
    public record CustomerRequest
    {
        public string CustomerId { get; set; } = string.Empty;
    }
}
