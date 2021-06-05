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
            _payments = new IPayment[]
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
        
        private readonly IPayment[] _payments;
        private static readonly CustomIncome Income1 = new(100, Day1, Day3);
        private static readonly CustomIncome Income2 = new(200, Day1, Day4);
        private static readonly CustomIncome Income3 = new(300, Day3, Day4, Day6);
        private static readonly CustomExpense Expense1 = new(100, Day2, Day5);
        private static readonly CustomExpense Expense2 = new(20, Day1, Day6);
        private static readonly CustomExpense Expense3 = new(70, Day2, Day4, Day7);

        [Fact]
        public void Should_ReturnCorrectForecast_When_ProvidedPreparedAccountant()
        {
            // Arrange
            var target = GetTarget();
            
            // Act
            var forecasts = target.Forecast(_payments, Day1, Day7);
            
            // Assert
            Assert.Equal(7, forecasts.Count());
            using var enumerator = forecasts.GetEnumerator();
            AssertNextBalance(280, Income1, Income2, Expense2);
            AssertNextBalance(-170, Expense1, Expense3);
            AssertNextBalance(400, Income1, Income3);
            AssertNextBalance(430, Income2, Income3, Expense3);
            AssertNextBalance(-100, Expense1);
            AssertNextBalance(280, Income3, Expense2);
            AssertNextBalance(-70, Expense3);

            void AssertNextBalance(int expected, params IPayment[] payments)
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
        
        private sealed class CustomIncome : Income
        {
            private readonly DateTime[] _happensOn;

            public CustomIncome(int amount, params DateTime[] happensOn) : base(amount, Fixture.Create("Income"))
            {
                _happensOn = happensOn;
            }

            public override bool HappensOn(DateTime date)
            {
                return _happensOn.Contains(date);
            }
        }

        private sealed class CustomExpense : Expense
        {
            private readonly DateTime[] _happensOn;

            public CustomExpense(int amount, params DateTime[] happensOn) : base(amount, Fixture.Create("Expense"))
            {
                _happensOn = happensOn;
            }

            public override bool HappensOn(DateTime date)
            {
                return _happensOn.Contains(date);
            }
        }
    }
}