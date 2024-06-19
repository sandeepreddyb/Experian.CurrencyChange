// See https://aka.ms/new-console-template for more information

using Experian.CurrencyChange.Business;
using Experian.CurrencyChange.Interfaces;

string restart ;
do
{
    //main part of program
    Console.Write("Enter the total given amount:");
    decimal totalAmount = Convert.ToDecimal(Console.ReadLine());

    Console.Write("Enter the purchase price:");
    decimal purchasePrice = Convert.ToDecimal(Console.ReadLine());
    ICurrencyOperations uc = new UKCurrencyOperations(purchasePrice, totalAmount);
    var res = await uc.GetChange();
    uc.PrintOutPut(res);

    Console.Write("Do you want to calculate another? (Y/N):");
    restart = Console.ReadLine().ToUpper();
    while ((restart != "Y") && (restart != "N"))
    {
        Console.WriteLine("Error");
        Console.WriteLine("Do you want to calculate another? (Y/N): ");
        restart = Console.ReadLine().ToUpper();
    }
} while (restart == "Y");
