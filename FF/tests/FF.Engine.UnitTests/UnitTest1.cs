using System;
using System.Threading.Tasks;
using Xunit;

namespace FF.Engine.UnitTests
{
    public class ForecasterTests
    {
        [Fact]
        public async Task Should_ReturnCorrectForecast_When_ProvidedPreparedAccountant()
        {
            // Arrange
            var accountant = await GetAccountant();
            var target = GetTarget();
            
            // Act
            var forecasts = target.Forecast(accountant, DateTime.Now, DateTime.Now.Add(TimeSpan.FromDays(2)));
            
            // Assert
            
        }

        private static async Task<Accountant> GetAccountant()
        {
            var accountant = new Accountant();

            await accountant.Income.Add(new Income(100, "salary"));
            await accountant.Expense.Add(new Expense(10, "food"));
            await accountant.Income.Add(new Income(100, "random income"));
            
            return accountant;
        }

        private static IForecaster GetTarget()
        {
            return new Forecaster();
        }
    }
}