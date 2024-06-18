// See https://aka.ms/new-console-template for more information

using Experian.CurrencyChange.Business;
using Experian.CurrencyChange.Interfaces;

Console.Write("Enter the total given amount:");
decimal totalAmount = Convert.ToDecimal(Console.ReadLine());

Console.Write("Enter the purchase price:");
decimal purchasePrice = Convert.ToDecimal(Console.ReadLine());
ICurrencyOperations uc = new UKCurrencyOperations(purchasePrice, totalAmount);
uc.GetChange();