using System;
using FunctionAppTimer.Domain;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionAppTimer
{
    public class AZFunctionTimer
    {
        private readonly ILogger<AZFunctionTimer> _logger;

        public AZFunctionTimer(ILogger<AZFunctionTimer> logger)
        {
            _logger = logger;
        }

        //Run trigger every 5 minutes - crontab.guru
        [Function("TimerTriggerDemo")]
        public void Run([TimerTrigger("0 */5 * * * *")] MyInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");


            // 1. Check if the function missed its scheduled time (e.g., app was down or recovering)
            if (myTimer.IsPastDue)
            {
                _logger.LogWarning("Timer execution is running late! Skipping heavy report batch processing.");
                return;
            }

            // 2. Access schedule metadata to log timing details
            if (myTimer.ScheduleStatus != null)
            {
                _logger.LogInformation($"Last successful execution was at: {myTimer.ScheduleStatus.Last}");
                _logger.LogInformation($"Next scheduled execution will be at: {myTimer.ScheduleStatus.Next}");
            }

            // 3. Perform business logic
            ExecuteReportGeneration();
        }

        private void ExecuteReportGeneration()
        {
            _logger.LogInformation("Report generation completed successfully.");
        }
    }
}
