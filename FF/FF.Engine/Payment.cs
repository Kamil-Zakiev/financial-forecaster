using System;

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
            return _paymentType.HappensOn(date, LumpSum);
        }
    }
}