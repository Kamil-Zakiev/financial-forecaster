using System;
using System.Collections.Generic;

namespace FF.Engine
{
    public sealed class Forecast
    {
        public DateTime Date { get; set; }
        public int EodBalance { get; set; }
        public IReadOnlyCollection<Payment> IncludedPayments { get; set; } = new List<Payment>();
    }
}