using FunctionAppDependencyInjection.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunctionAppDependencyInjection.FakeProductDB
{
    public interface IFakeOrderDB
    {
       Task<IEnumerable<Order>> GetAllOrdersByCustomerID(string customerId);
    }
}
