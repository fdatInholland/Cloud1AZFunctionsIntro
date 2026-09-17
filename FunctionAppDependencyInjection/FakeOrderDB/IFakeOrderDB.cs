using FunctionAppDependencyInjection.Domain;
using System.Collections.Generic;

namespace FunctionAppDependencyInjection.FakeProductDB
{
    public interface IFakeOrderDB
    {
        IEnumerable<Order> GetAllOrdersByCustomerID(string customerId);
    }
}
