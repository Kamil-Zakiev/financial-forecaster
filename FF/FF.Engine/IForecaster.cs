using System;
using System.Collections.Generic;

namespace FF.Engine
{
    public interface IForecaster
    {
        IReadOnlyCollection<Forecast> Forecast(IAccountant accountant, DateTime from, DateTime to);
    }

    internal class Forecaster : IForecaster
    {
        public IReadOnlyCollection<Forecast> Forecast(IAccountant accountant, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
    }

    public class Forecast
    {
    }
}