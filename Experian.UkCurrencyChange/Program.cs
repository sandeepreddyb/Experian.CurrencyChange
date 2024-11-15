// See https://aka.ms/new-console-template for more information

using Experian.CurrencyChange.Business;
using Experian.CurrencyChange.Interfaces;

try
{
    bool continueCalculation=false;
    do
    {
        decimal totalAmount = GetAmount("Enter the total given amount:");
        decimal purchasePrice = GetAmount("Enter the total purchase amount:");

        if (purchasePrice > totalAmount)
        {
            Console.WriteLine("Purchase price should not be greater than total amount.");
            continue;
        }

        ICurrencyOperations uc = new UKCurrencyOperations(purchasePrice, totalAmount);
        var change = await uc.GetChange();
        uc.PrintOutPut(change);

        continueCalculation = AskToContinue();
    } while (continueCalculation);
}
catch (OverflowException ex)
{
    Console.WriteLine("An overflow error occurred: " + ex.Message);
}
catch (Exception ex)
{
    Console.WriteLine("An unexpected error occurred: " + ex.Message);
}
  

static decimal GetAmount(string prompt)
{
    decimal amount;
    while (true)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();
        if (decimal.TryParse(input, out amount) && amount >= 0)
        {
            return amount;
        }
        Console.WriteLine("Invalid input. Please enter a non-negative decimal number.");
    }
}

static bool AskToContinue()
{
    while (true)
    {
        Console.Write("Do you want to calculate another? (Y/N): ");
        var response = Console.ReadLine()?.ToUpper();
        switch (response)
        {
            case "Y":
                return true;
            case "N":
                return false;
            default:
                Console.WriteLine("Invalid input. Please enter 'Y' or 'N'.");
                break;
        }
    }
}

