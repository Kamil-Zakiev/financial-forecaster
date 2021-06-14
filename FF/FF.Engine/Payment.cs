using System;
using System.Collections.Generic;
using System.Linq;

namespace FF.Engine
{
    public abstract class Payment
    {
        private readonly IPaymentType _paymentType;
        protected int LumpSum { get; }

        public string Description { get; }

        protected Payment(int lumpSum, string description, IPaymentType paymentType)
        {
            _paymentType = paymentType;
            LumpSum = lumpSum;
            Description = description;
        }

        public (bool, int) HappensOn(DateTime date)
        {
            if (_happenedOn.Contains(date) || _dropped.Contains(date))
            {
                return (false, default);
            }
            
            return _paymentType.HappensOn(date, LumpSum);
        }

        private readonly HashSet<DateTime> _happenedOn = new();
        private readonly HashSet<DateTime> _dropped = new();

        public void TrackHappened(DateTime date)
        {
            _happenedOn.Add(date);
        }

        public void DropOn(DateTime date)
        {
            _dropped.Add(date);
        }
    }
}