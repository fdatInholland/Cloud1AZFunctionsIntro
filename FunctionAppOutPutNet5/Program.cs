using Microsoft.Extensions.Hosting;

namespace FunctionAppOutPutNet5
{
    public class Program
    {
        public static void Main()
        {

            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .Build();

            host.Run();
        }
    }
}