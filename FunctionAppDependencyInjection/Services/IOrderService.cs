using FunctionAppDependencyInjection.Domain;
using System.Collections.Generic;

namespace FunctionAppDependencyInjection.Services
{
    public interface IOrderService
    {
         IEnumerable<Order> GetAllOrdersByCustomerID(string CustomerId);
    }
}
