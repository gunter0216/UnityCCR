namespace Core.Currency.Calculator.Runtime
{
    public struct CalculationResult
    {
        public long Value;
        public CalculationErrors Result;

        public CalculationResult(long value, CalculationErrors result)
        {
            Value = value;
            Result = result;
        }
    }
}