namespace FF.Engine
{
    public interface IAccountant
    {
        ISpecificAccountant<Income> Income { get; }
        ISpecificAccountant<Expense> Expense { get; }
    }
}