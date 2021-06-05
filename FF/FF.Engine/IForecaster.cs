using System;
using System.Collections.Generic;

namespace FF.Engine
{
    public interface IForecaster
    {
        IEnumerable<Forecast> Forecast(IReadOnlyCollection<IPayment> payments, DateTime from, DateTime to);
    }
}