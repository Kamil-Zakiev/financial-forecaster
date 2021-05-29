namespace FF.Engine
{
    internal sealed class Accountant : IAccountant
    {
        public ISpecificAccountant<Income> Income { get; } = new SpecificAccountant<Income>();
        public ISpecificAccountant<Expense> Expense { get; } = new SpecificAccountant<Expense>();
    }
}