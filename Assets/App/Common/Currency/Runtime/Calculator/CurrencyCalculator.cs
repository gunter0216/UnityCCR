using System;

namespace Core.Currency.Calculator.Runtime
{
    public class CurrencyCalculator : ICurrencyCalculator
    {
        public CalculationResult Add(long value1, long value2, long maxValue = long.MaxValue)
        {
            if (value2 < 0)
            {
                return new CalculationResult(value1, CalculationErrors.ValueBelowZero);
            }

            try
            {
                checked
                {
                    var value = value1 + value2;
                }
            }
            catch (OverflowException e)
            {
                return new CalculationResult(long.MaxValue, CalculationErrors.Overflow);
            }

            value1 += value2;
            if (value1 > maxValue)
            {
                value1 = maxValue;
                return new CalculationResult(value1, CalculationErrors.BiggerThanMax);
            }

            return new CalculationResult(value1, CalculationErrors.Success);
        }
        
        public CalculationResult Subtract(long value1, long value2)
        {
            if (value2 < 0)
            {
                return new CalculationResult(value1, CalculationErrors.ValueBelowZero);
            }

            if (value1 - value2 < 0)
            {
                return new CalculationResult(value1, CalculationErrors.CurrencyNotEnough);
            }

            try
            {
                checked
                {
                    var value = value1 - value2;
                }
            }
            catch (OverflowException e)
            {
                return new CalculationResult(value1, CalculationErrors.Overflow);
            }
            
            value1 -= value2;

            return new CalculationResult(value1, CalculationErrors.Success);
        }
        
    }
}