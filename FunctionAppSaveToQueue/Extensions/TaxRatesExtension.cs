using FunctionAppQueueTriggerSaveToQueue.Models;
using System;

namespace FunctionAppQueueTriggerSaveToQueue.Extensions
{
    internal static class TaxRatesExtension
    {
        public static decimal GetRate(this TaxRates taxRates) => taxRates switch
        {
            TaxRates.High => 0.20m,
            TaxRates.Low => 0.09m,
            TaxRates.None => 0.00m,
            _ => throw new ArgumentOutOfRangeException(nameof(taxRates))
        };
    }
}
