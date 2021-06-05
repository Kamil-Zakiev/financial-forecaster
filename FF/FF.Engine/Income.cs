namespace FF.Engine
{
    public abstract class Income : Payment
    {
        protected Income(int amount, string description) : base(amount, description)
        {
        }
    }
}