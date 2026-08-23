using ReceiptGenerator.Application;
using ReceiptGenerator.Domain.Discount;
using ReceiptGenerator.Domain.Product;

using ReceiptGenerator.Domain.ReceiptEntities;
using ReceiptGenerator.Domain.Tax;
using ReceiptGenerator.infrastructure;
using ReceiptGenerator.infrastructure.Rendering;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

// Intentionally messy practice code.
// It works, but mixes command-line handling, file I/O, parsing, business rules,
// calculations, formatting, and persistence in one method.
public static class Program
{
    public static int Main(string[] args)
    {
        return Run(args);
    }

    public static int Run(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: ReceiptGenerator <basket.csv> <receipt.txt>");
            return 1;
        }

        try
        {
            string[] lines = File.ReadAllLines(args[0]);
            var items = new List<BasketItem>();
           
            foreach (string rawLine in lines)
            {
                if (string.IsNullOrWhiteSpace(rawLine) || rawLine.TrimStart().StartsWith("#"))
                    continue;

                string[] parts = rawLine.Split(',');
                if (parts.Length != 4)
                {
                    Console.WriteLine("Skipping invalid line: " + rawLine);
                    continue;
                }

                string name = parts[0].Trim();
                int quantity = int.Parse(parts[1].Trim());
                decimal price = decimal.Parse(parts[2].Trim(), CultureInfo.InvariantCulture);
                string category = parts[3].Trim().ToLowerInvariant();
                BasketItem basketItem = new BasketItem(name, quantity, price, category);
                items.Add(basketItem);
              
            }

CappedDiscountStrategy cappedDiscountStrategy = new CappedDiscountStrategy(new CompositeDiscountStrategy(new List<IDiscountStrategy>
            {
                new BooksDiscountStrategy(),
                new QuantityDiscountStrategy(),
                new ClearanceDiscountStrategy()
            }));
ReceiptLineCalculator receiptLineCalculator = new ReceiptLineCalculator(new PercentageTaxStrategy(), cappedDiscountStrategy);
            var application =
     new ReceiptApplication(
          new receiptRenderer(),
         new receiptwriter(),
         new BaseketReader(),
         receiptLineCalculator
        );

            application.GenerateReceipt(args[0], args[1]);


            Console.WriteLine("Receipt written to " + args[1]);
            return 0;
        }
        catch (Exception exception)
        {
            Console.WriteLine("Something went wrong: " + exception.Message);
            return 1;
        }
    }
    
}
