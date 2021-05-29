namespace FF.Engine
{
    public sealed class Income
    {
        public int Amount { get; }
        public string Description { get; }

        public Income(int amount, string description)
        {
            Amount = amount;
            Description = description;
        }
    }
}