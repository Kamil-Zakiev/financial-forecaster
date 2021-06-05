using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Engine
{
    internal class Forecaster : IForecaster
    {
        public IEnumerable<Forecast> Forecast(IReadOnlyCollection<IPayment> payments, DateTime from, DateTime to)
        {
            var date = from;
            while (date <= to)
            {
                Forecast? f = null;
                foreach (var payment in payments)
                {
                    if (!payment.HappensOn(date))
                    {
                        continue;
                    }
                    
                    f ??= new Forecast
                    {
                        Date = date
                    };
                    var delta = payment switch
                    {
                        Income income => income.Amount,
                        Expense expense => -1 * expense.Amount,
                        _ => throw new ArgumentOutOfRangeException(nameof(payment))
                    };

                    f.EodBalance += delta;

                    f.IncludedPayments = f.IncludedPayments.Concat(payments).ToArray();
                }
                date = date.AddDays(1);

                if (f is not null)
                {
                    yield return f;
                }
            }
        }
    }
}