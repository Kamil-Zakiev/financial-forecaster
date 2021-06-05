using System;

namespace FF.Engine
{
    public abstract class Expense : Payment
    {
        protected Expense(int amount, string description) : base(amount, description)
        {
        }
    }
}