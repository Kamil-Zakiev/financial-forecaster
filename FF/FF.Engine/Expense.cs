using System;

namespace FF.Engine
{
    public class Expense : Payment
    {
        public Expense(int lumpSum, string description, IPaymentType paymentType) 
            : base(lumpSum, description, paymentType)
        {
        }
    }

    public interface IPaymentType
    {
        (bool, int) HappensOn(DateTime date, int lumpSum);
    }

    public sealed class OneTimePayment : IPaymentType
    {
        public DateTime DateTime { get; }

        public OneTimePayment(DateTime dateTime)
        {
            DateTime = dateTime;
        }

        public (bool, int) HappensOn(DateTime date, int lumpSum)
        {
            return date.Date == DateTime.Date 
                ? (true, lumpSum) 
                : (false, default);
        }
    }

    public sealed class MonthlyPayment : IPaymentType
    {
        public int Day { get; }

        public MonthlyPayment(int day)
        {
            if (day >= 1 && day <= 28)
            {
                Day = day;
            }

            throw new ArgumentOutOfRangeException(nameof(day), day, "Incorrect day");
        }

        public (bool, int) HappensOn(DateTime date, int lumpSum)
        {
            return date.Day == Day 
                ? (true, lumpSum) 
                : (false, default);
        }
    }

    public sealed class AnnualPayment : IPaymentType
    {
        public int Month { get; }
        public int Day { get; }

        public AnnualPayment(int day, int month)
        {
            if (month <= 0 || month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month));
            }
            
            Month = month;
            
            if (day >= 1 && day <= 28)
            {
                Day = day;
                return;
            }

            throw new ArgumentOutOfRangeException(nameof(day), day, "Incorrect day");
        }

        public (bool, int) HappensOn(DateTime date, int lumpSum)
        {
            if (date.Day == Day && date.Month == Month)
            {
                return (true, lumpSum);
            }

            return (false, default);
        }
    }

    public sealed class ContinuousPayment : IPaymentType
    {
        public Period ContinuationBasis { get; }

        public ContinuousPayment(Period continuationBasis)
        {
            ContinuationBasis = continuationBasis;
        }
        
        public enum Period
        {
            Week = 1,
            Month = 2,
            Year = 3
        }

        public (bool, int) HappensOn(DateTime date, int lumpSum)
        {
            var divider = ContinuationBasis switch
            {
                Period.Week => 7,
                Period.Month => 30,
                Period.Year => 365,
                _ => throw new ArgumentOutOfRangeException(nameof(ContinuationBasis))
            };

            return (true, lumpSum / divider);
        }
    }
    
}