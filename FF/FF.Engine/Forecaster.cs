using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Engine
{
    internal class Forecaster : IForecaster
    {
        public IEnumerable<Forecast> Forecast(IReadOnlyCollection<Payment> payments, DateTime from, DateTime to)
        {
            var date = from;
            while (date <= to)
            {
                Forecast? f = null;
                List<Payment>? includedPayments = null;
                foreach (var payment in payments)
                {
                    var (happens, amount) = payment.HappensOn(date);
                    if (!happens)
                    {
                        continue;
                    }
                    
                    f ??= new Forecast
                    {
                        Date = date
                    };
                    var delta = payment switch
                    {
                        Income => amount,
                        Expense => -1 * amount,
                        _ => throw new ArgumentOutOfRangeException(nameof(payment))
                    };

                    f.EodBalance += delta;

                    includedPayments ??= new List<Payment>();
                    includedPayments.Add(payment);
                }
                date = date.AddDays(1);

                if (f is null)
                {
                    continue;
                }
                
                f.IncludedPayments = includedPayments;
                yield return f;
            }
        }
    }
}