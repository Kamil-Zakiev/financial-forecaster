using System;

namespace FF.Engine
{
    public abstract class Payment : IPayment
    {
        public int Amount { get; }
        public string Description { get; }

        protected Payment(int amount, string description)
        {
            Amount = amount;
            Description = description;
        }

        public abstract bool HappensOn(DateTime date);
    }

    public interface IPayment
    {
        int Amount { get; }
        string Description { get; }
        bool HappensOn(DateTime date);
    }
}