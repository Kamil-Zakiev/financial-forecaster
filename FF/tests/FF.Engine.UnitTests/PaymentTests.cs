using System;
using Xunit;

namespace FF.Engine.UnitTests
{
    public class PaymentTests
    {
        [Fact]
        public void Should_Happen_When_Planned()
        {
            var payment = new MyPayment(1000, "test", new OneTimePayment(new DateTime(2021, 6, 12)));

            var result = payment.HappensOn(new DateTime(2021, 6, 12));
            Assert.True(result.Item1);
            Assert.Equal(1000, result.Item2);
        }

        [Fact]
        public void Should_NotHappen_When_Dropped()
        {
            var date = new DateTime(2021, 6, 12);
            var payment = new MyPayment(1000, "test", new OneTimePayment(date));

            payment.DropOn(date);
            var result = payment.HappensOn(date);
            Assert.False(result.Item1);
        }

        [Fact]
        public void Should_NotHappen_When_AlreadyHappened()
        {
            var date = new DateTime(2021, 6, 12);
            var payment = new MyPayment(1000, "test", new OneTimePayment(date));

            payment.TrackHappened(date);
            var result = payment.HappensOn(date);
            Assert.False(result.Item1);
        }

        private class MyPayment : Payment
        {
            public MyPayment(int lumpSum, string description, IPaymentType paymentType) : base(lumpSum, description,
                paymentType)
            {
            }
        }
    }
}