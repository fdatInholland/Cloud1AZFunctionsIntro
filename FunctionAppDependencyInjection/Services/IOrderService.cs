using FunctionAppDependencyInjection.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunctionAppDependencyInjection.Services
{
    public interface IOrderService
    {
         Task<IEnumerable<Order>> GetAllOrdersByCustomerID(string CustomerId);
    }
}
