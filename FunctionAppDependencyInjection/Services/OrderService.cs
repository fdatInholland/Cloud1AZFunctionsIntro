using FunctionAppDependencyInjection.Domain;
using FunctionAppDependencyInjection.FakeProductDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunctionAppDependencyInjection.Services
{
    public class OrderService : IOrderService
    {
        private readonly IFakeOrderDB _fakeOrderDB;

        public OrderService(IFakeOrderDB fakeorderDB)
        {
            _fakeOrderDB = fakeorderDB;
        }

        public Task<IEnumerable<Order>> GetAllOrdersByCustomerID(string CustomerId)
        {
            var foundorders = _fakeOrderDB.GetAllOrdersByCustomerID(CustomerId);

            return foundorders;
        }
    }
}
