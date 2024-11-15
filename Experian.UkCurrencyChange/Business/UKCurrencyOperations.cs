using Experian.CurrencyChange.Interfaces;

namespace Experian.CurrencyChange.Business
{
    public class UKCurrencyOperations : ICurrencyOperations
    {
        public decimal PurchasePrice { get; set; }
        public decimal GivenAmount { get; set; }

        //UK currency denoms in descending order
        private readonly decimal[] denoms;


        public UKCurrencyOperations(decimal purchasePrice, decimal givenAmount)
        {
            PurchasePrice = purchasePrice;
            GivenAmount = givenAmount;
            denoms= new decimal[] { 50m, 20m, 10m, 5m, 2m, 1m, 0.5m, 0.2m, 0.1m, 0.05m, 0.02m, 0.01m };
        }

        /// <summary>
        /// Calculates the change to be returned based on given amount and purchase price
        /// using available denominations.
        /// </summary>
        /// <returns>A dictionary containing the count of each denomination needed for change</returns>
        /// <exception cref="InvalidOperationException">Thrown when given amount is less than purchase price</exception>
        public async Task<Dictionary<string, decimal>> GetChange()
        {
            var changeByDenomination = new Dictionary<string, decimal>();

            // Validate inputs
            if (GivenAmount < 0 || PurchasePrice < 0)
            {
                throw new ArgumentException("Given amount and purchase price must be non-negative values.");
            }

            if (GivenAmount < PurchasePrice)
            {
                throw new InvalidOperationException("Given amount must be greater than or equal to purchase price.");
            }

            decimal change = GivenAmount - PurchasePrice;

            // Early return for exact payment
            if (change == 0)
            {
                changeByDenomination.Add("0", 0m);
                return changeByDenomination;
            }

            // Validate denominations
            if (denoms == null || !denoms.Any())
            {
                throw new InvalidOperationException("No valid denominations available.");
            }

            try
            {
                await Task.Run(() =>
                {
                    // Sort denominations in descending order for optimal change calculation
                    var sortedDenoms = denoms.Where(d => d > 0).OrderByDescending(d => d).ToList();

                    foreach (var denom in sortedDenoms)
                    {
                        int count = (int)(change / denom);
                        if (count > 0)
                        {
                            changeByDenomination[denom.ToString("0.00")] = count;
                            change -= count * denom;
                        }

                        // Break early if no more change needed
                        if (change == 0) break;
                    }
                });

                // Handle remaining amount that couldn't be broken down
                if (change > 0)
                {
                    changeByDenomination["Remaining"] = Math.Round(change, 2);
                }

                return changeByDenomination;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error calculating change.", ex);
            }
        }

       

        private string FormatCurrencyOutput(decimal denomination, decimal quantity)
        {
            return denomination < 1
                ? $"{quantity} x {(int)(denomination * 100)}p"
                : $"{quantity} x £{denomination:N2}";
        }

        public void PrintOutPut(Dictionary<string, decimal> changeByDenomination)
        {
            if (changeByDenomination == null || !changeByDenomination.Any())
            {
                Console.WriteLine("No change to return.");
                return;
            }

            Console.WriteLine("\n=== Change Breakdown ===");

            var orderedChange = changeByDenomination
                .OrderByDescending(x => decimal.TryParse(x.Key, out decimal value) ? value : 0)
                .ToList();

            foreach (var (denomination, quantity) in orderedChange)
            {
                if (quantity <= 0) continue;

                string formattedOutput;
                if (decimal.TryParse(denomination, out decimal value))
                {
                    formattedOutput = FormatCurrencyOutput(value, quantity);
                }
                else
                {
                    formattedOutput = $"{quantity} x £{denomination}";
                }

                Console.WriteLine(formattedOutput);
            }

            Console.WriteLine("=====================");
        }
    }
}
