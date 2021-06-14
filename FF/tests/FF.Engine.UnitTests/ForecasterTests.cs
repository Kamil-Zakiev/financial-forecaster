using System;
using System.Linq;
using AutoFixture;
using Xunit;

namespace FF.Engine.UnitTests
{
    public class ForecasterTests
    {
        private static readonly DateTime Day1 = new (2021, 6, 5);
        private static readonly DateTime Day2 = Day1.AddDays(1);
        private static readonly DateTime Day3 = Day2.AddDays(1);
        private static readonly DateTime Day4 = Day3.AddDays(1);
        private static readonly DateTime Day5 = Day4.AddDays(1);
        private static readonly DateTime Day6 = Day5.AddDays(1);
        private static readonly DateTime Day7 = Day6.AddDays(1);

        public ForecasterTests()
        {
            _payments = new Payment[]
            {
                Income1,
                Income2,
                Income3,
                Expense1,
                Expense2,
                Expense3
            };
        }

        private static readonly Fixture Fixture = new();
        
        private readonly Payment[] _payments;
        private static readonly Income Income1 = new(100, "Income1", new CustomPaymentType(Day1, Day3));
        private static readonly Income Income2 = new(200, "Income2", new CustomPaymentType(Day1, Day4));
        private static readonly Income Income3 = new(300, "Income3", new CustomPaymentType(Day3, Day4, Day6));
        private static readonly Expense Expense1 = new(100, "Expense1",  new CustomPaymentType(Day2, Day5));
        private static readonly Expense Expense2 = new(20, "Expense2",  new CustomPaymentType(Day1, Day6));
        private static readonly Expense Expense3 = new(70, "Expense3",  new CustomPaymentType(Day2, Day4, Day7));

        [Fact]
        public void Should_ReturnCorrectForecast_When_ProvidedPreparedAccountant()
        {
            // Arrange
            var target = GetTarget();
            
            // Act
            var forecasts = target.Forecast(_payments, Day1, Day7);
            
            // Assert
            // ReSharper disable once PossibleMultipleEnumeration
            Assert.Equal(7, forecasts.Count());
            // ReSharper disable once PossibleMultipleEnumeration
            using var enumerator = forecasts.GetEnumerator();
            AssertNextBalance(280, Income1, Income2, Expense2);
            AssertNextBalance(-170, Expense1, Expense3);
            AssertNextBalance(400, Income1, Income3);
            AssertNextBalance(430, Income2, Income3, Expense3);
            AssertNextBalance(-100, Expense1);
            AssertNextBalance(280, Income3, Expense2);
            AssertNextBalance(-70, Expense3);

            void AssertNextBalance(int expected, params Payment[] payments)
            {
                enumerator.MoveNext();
                Assert.Equal(expected, enumerator.Current!.EodBalance);
                foreach (var payment in payments)
                {
                    Assert.Contains(payment, enumerator.Current!.IncludedPayments);
                }
            }
        }

        private static IForecaster GetTarget()
        {
            return new Forecaster();
        }
        
        private sealed class CustomPaymentType : IPaymentType
        {
            private readonly DateTime[] _happensOn;

            public CustomPaymentType(params DateTime[] happensOn)
            {
                _happensOn = happensOn;
            }

            public (bool, int) HappensOn(DateTime date, int lumpSum)
            {
                if (_happensOn.Contains(date))
                {
                    return (true, lumpSum);
                }

                return (false, default);
            }
        }
    }
}