namespace Core.Currency.Calculator.Runtime
{
    public interface ICurrencyCalculator
    {
        CalculationResult Add(long value1, long value2, long maxValue = long.MaxValue);
        CalculationResult Subtract(long value1, long value2);
    }
}