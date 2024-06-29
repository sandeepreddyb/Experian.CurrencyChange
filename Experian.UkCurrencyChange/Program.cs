// See https://aka.ms/new-console-template for more information

using Experian.CurrencyChange.Business;
using Experian.CurrencyChange.Interfaces;

string restart ;

try
{
    do
    {
        //main part of program
        Console.Write("Enter the total given amount:");
        var input1 = Console.ReadLine();
        var res = Decimal.TryParse(input1, out decimal result);
        if (!res)
        {
            Console.WriteLine("Invalid input");
            goto repeat;
        }
        decimal totalAmount = Convert.ToDecimal(result);
        if (totalAmount < 0)
        {
            Console.WriteLine("The money cant be in negative");
            goto repeat;
        }
        Console.Write("Enter the total purchase amount:");
        var input2 = Console.ReadLine();
        var res2 = decimal.TryParse(input2, out decimal result2);
        if (!res2)
        {
            Console.WriteLine("Invalid input");
            goto repeat;
        }
        

        decimal purchasePrice = Convert.ToDecimal(result2);
        if (purchasePrice < 0)
        {
            Console.WriteLine("The money cant be in negative");
            goto repeat;
        }
        if (purchasePrice > totalAmount)
        {
            Console.WriteLine("Purchase price should not be greater than total amount.");
            goto repeat;
        }
        ICurrencyOperations uc = new UKCurrencyOperations(purchasePrice, totalAmount);
        var res1 = await uc.GetChange();
        uc.PrintOutPut(res1);

    //label
    repeat:
        Console.Write("Do you want to calculate another? (Y/N):");
        restart = Console.ReadLine().ToUpper();
        while ((restart != "Y") && (restart != "N"))
        {
            Console.WriteLine("Error");
            Console.Write("Do you want to calculate another? (Y/N): ");
            restart = Console.ReadLine().ToUpper();
        }
    } while (restart == "Y");
}
catch(OverflowException ex)
{
    
}
