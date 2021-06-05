using System;

namespace FF.Engine
{
    public abstract class Payment
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
}