using Experian.CurrencyChange.Interfaces;

namespace Experian.CurrencyChange.Business
{
    public class UKCurrencyOperations : ICurrencyOperations
    {
        public decimal PurchasePrice { get; set; }
        public decimal GivenAmount { get; set; }

        //UK currency denoms in descending order
        private readonly decimal[] denoms =[]; 

        public UKCurrencyOperations(decimal purchasePrice, decimal givenAmount)
        {
            PurchasePrice = purchasePrice;
            GivenAmount = givenAmount;
            denoms=[ 50m, 20m, 10m, 5m, 2m, 1m, 0.5m, 0.2m, 0.1m, 0.05m, 0.02m, 0.01m ];
        }
        public async Task<Dictionary<string, decimal>> GetChange()
        {
            Dictionary<string, decimal> changeByDenomination = new Dictionary<string, decimal>();
            decimal change = GivenAmount - PurchasePrice;

            if(change == 0)
            {
                changeByDenomination.Add("0", 0m);
               return changeByDenomination;
            }

            await Task.Run(() =>
            {
                foreach (var denom in denoms)
                {
                    int count = (int)(change / denom);
                    if (count > 0)
                    {
                        if (count > 0)
                        {
                            changeByDenomination[denom.ToString()] = count;
                            change -= count * denom;
                        }
                    }
                }
            });
           
            return changeByDenomination;
        }
        public void PrintOutPut(Dictionary<string, decimal> changeByDenomination)
        {
            Console.WriteLine("Your change is:");
            foreach (var kvp in changeByDenomination)
            {
                decimal.TryParse(kvp.Key, out decimal i);
                decimal val = kvp.Value;
                string key = i < 1 ? (int)(i * 100) + "p" : kvp.Key;
                Console.WriteLine($"{val} x £{key} ");
            }
        }
    }
}
