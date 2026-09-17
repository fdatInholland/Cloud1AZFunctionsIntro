using FunctionAppDependencyInjection.Domain;
using FunctionAppDependencyInjection.FakeProductDB;
using System.Collections.Generic;

namespace FunctionAppDependencyInjection.Services
{
    public class OrderService : IOrderService
    {
        private readonly IFakeOrderDB _fakeOrderDB;

        public OrderService(IFakeOrderDB fakeorderDB)
        {
            _fakeOrderDB = fakeorderDB;
        }
        public IEnumerable<Order> GetAllOrdersByCustomerID(string CustomerId)
        {
            return _fakeOrderDB.GetAllOrdersByCustomerID(CustomerId);
        }
    }
}
