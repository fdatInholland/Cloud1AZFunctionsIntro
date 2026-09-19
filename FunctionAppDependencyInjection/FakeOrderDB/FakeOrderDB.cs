using FunctionAppDependencyInjection.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunctionAppDependencyInjection.FakeProductDB
{
    public class FakeOrderDB : IFakeOrderDB
    {
        private IEnumerable<Order> orders;

        public FakeOrderDB()
        {
            orders = GetOrders();
        }

        Task<IEnumerable<Order>> IFakeOrderDB.GetAllOrdersByCustomerID(string customerId)
        {
            var foundorders= orders.Where(o => o.CustomerId == customerId);
            return Task.FromResult(foundorders);
        }

        private IEnumerable<Order> GetOrders()
        {
            List<Order> fakeOrders = new List<Order>
            {
                    new Order
                {
                    OrderId = "ORD-2026-001",
                    CustomerId = "CUST-8041",
                    Amount = 149.99m,
                    CreatedAt = new DateTime(2026, 9, 10, 08, 30, 00, DateTimeKind.Utc)
                },
                new Order
                {
                    OrderId = "ORD-2026-002",
                    CustomerId = "CUST-3109",
                    Amount = 89.50m,
                    CreatedAt = new DateTime(2026, 9, 11, 11, 15, 30, DateTimeKind.Utc)
                },
                new Order
                {
                    OrderId = "ORD-2026-003",
                    CustomerId = "CUST-8041",
                    Amount = 450.00m,
                    CreatedAt = new DateTime(2026, 9, 11, 14, 05, 12, DateTimeKind.Utc)
                },
                new Order
                {
                    OrderId = "ORD-2026-004",
                    CustomerId = "CUST-9923",
                    Amount = 12.99m,
                    CreatedAt = new DateTime(2026, 9, 12, 09, 45, 00, DateTimeKind.Utc)
                },
                new Order
                {
                    OrderId = "ORD-2026-005",
                    CustomerId = "CUST-5512",
                    Amount = 1299.00m,
                    CreatedAt = new DateTime(2026, 9, 13, 16, 20, 45, DateTimeKind.Utc)
                },
                new Order
                {
                    OrderId = "ORD-2026-006",
                    CustomerId = "CUST-1044",
                    Amount = 74.25m,
                    CreatedAt = new DateTime(2026, 9, 14, 10, 00, 00, DateTimeKind.Utc)
                },
                new Order
                {
                    OrderId = "ORD-2026-007",
                    CustomerId = "CUST-3109",
                    Amount = 210.80m,
                    CreatedAt = new DateTime(2026, 9, 15, 13, 11, 05, DateTimeKind.Utc)
                },
                new Order
                {
                    OrderId = "ORD-2026-008",
                    CustomerId = "CUST-6671",
                    Amount = 55.00m,
                    CreatedAt = new DateTime(2026, 9, 15, 18, 50, 22, DateTimeKind.Utc)
                },
                new Order
                {
                    OrderId = "ORD-2026-009",
                    CustomerId = "CUST-9923",
                    Amount = 340.15m,
                    CreatedAt = new DateTime(2026, 9, 16, 07, 30, 00, DateTimeKind.Utc)
                },
                new Order
                {
                    OrderId = "ORD-2026-010",
                    CustomerId = "CUST-4008",
                    Amount = 599.99m,
                    CreatedAt = new DateTime(2026, 9, 17, 10, 05, 18, DateTimeKind.Utc)
                }
            };
            return fakeOrders;
        }
    }
}
