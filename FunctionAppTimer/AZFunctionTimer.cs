using System;
using FunctionAppTimer.Domain;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionAppTimer
{
    public static class AZFunctionTimer
    {
        [Function("TimerTriggerDemo")]

        //Run trigger at minute 0 every 5th hour
        public static void Run([TimerTrigger("0 */5 * * * *")] MyInfo myTimer, FunctionContext context)
        {
            var logger = context.GetLogger("TimerTriggerDemo");
            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");

            // do stuff eg. reporting, data ingestion, db maintenance...
        }
    }
}
