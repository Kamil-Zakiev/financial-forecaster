namespace FF.Engine
{
    public sealed class Expense
    {
        public int Amount { get; }
        public string Description { get; }

        public Expense(int amount, string description)
        {
            Amount = amount;
            Description = description;
        }
    }
}