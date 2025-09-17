namespace Core.Currency.Calculator.Runtime
{
    public enum CalculationErrors
    {
        Success,
        Overflow,
        BiggerThanMax,
        ValueBelowZero,
        CurrencyNotEnough
    }
}