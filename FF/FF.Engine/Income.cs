namespace FF.Engine
{
    public class Income : Payment
    {
        public Income(int lumpSum, string description, IPaymentType paymentType) 
            : base(lumpSum, description, paymentType)
        {
        }
    }
}