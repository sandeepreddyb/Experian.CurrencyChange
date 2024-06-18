namespace Experian.CurrencyChange.Interfaces
{
    public interface ICurrencyOperations
    {
        Task<Dictionary<string, decimal>> GetChange();
        void PrintOutPut(Dictionary<string, decimal> changeByDenomination);
    }
}
