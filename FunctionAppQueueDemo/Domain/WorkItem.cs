using System;

namespace FunctionAppQueueDemo.Domain
{
    public record WorkItem(string Id, string Data, DateTime CreatedAt);
}
