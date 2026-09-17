using FunctionAppDependencyInjection.FakeProductDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Builder;
using FunctionAppDependencyInjection.Services;

namespace FunctionAppDependencyInjection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Dependency Injection - conform .net 8.0
            var builder = FunctionsApplication.CreateBuilder(args);
            builder.ConfigureFunctionsWebApplication();
           
            builder.Services.AddTransient<IFakeOrderDB, FakeOrderDB>();
            builder.Services.AddTransient<IOrderService, OrderService>();

            builder.Build().Run();
        }
    }
}