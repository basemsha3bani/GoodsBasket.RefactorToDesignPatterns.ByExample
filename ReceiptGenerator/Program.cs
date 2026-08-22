using ReceiptGenerator.Domain.Discount;
using ReceiptGenerator.Domain.Product;
using ReceiptGenerator.Domain.Receipt;
using ReceiptGenerator.Domain.Tax;
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

            decimal subtotal = 0;
            decimal discountTotal = 0;
            decimal taxTotal = 0;
            var output = new StringBuilder();
            output.AppendLine("========================================");
            output.AppendLine("             CORNER SHOP");
            output.AppendLine("========================================");
            var receiptLines = new List<ReceiptLine>();
            receiptLines = items.Select(item => CalculateReceiptLine(item)).ToList();
            Receipt x= new Receipt(receiptLines);

            foreach (var receiptLine in x.Lines)
            {
                string displayName = receiptLine.basketItem.product.Name;

                output.AppendLine(
                    $"{displayName,-18} " +
                    $"{receiptLine.basketItem.Quantity,3} x " +
                    $"{receiptLine.basketItem.UnitPrice,7:0.00} = " +
                    $"{receiptLine.subtotal,8:0.00}");

                if (receiptLine.discount > 0)
                {
                    output.AppendLine(
                        $"  discount                              -" +
                        $"{receiptLine.discount,8:0.00}");
                }

                output.AppendLine(
                    $"  tax                                    " +
                    $"{receiptLine.tax,8:0.00}");
                
                subtotal += receiptLine.subtotal;
                discountTotal += receiptLine.discount;
                taxTotal += receiptLine.tax;
            }
            decimal total = subtotal - discountTotal + taxTotal;
            output.AppendLine("----------------------------------------");
            output.AppendLine($"Subtotal:                        {subtotal,10:0.00}");
            output.AppendLine($"Discounts:                      -{discountTotal,10:0.00}");
            output.AppendLine($"Tax:                             {taxTotal,10:0.00}");
            output.AppendLine($"TOTAL:                           {total,10:0.00}");
            output.AppendLine("========================================");

            File.WriteAllText(args[1], output.ToString());
            Console.WriteLine("Receipt written to " + args[1]);
            return 0;
        }
        catch (Exception exception)
        {
            Console.WriteLine("Something went wrong: " + exception.Message);
            return 1;
        }
    }
    private static  ReceiptLine CalculateReceiptLine(BasketItem item)
    {
        decimal lineTotal = item.Quantity * item.UnitPrice;
        var receiptLineCalculator =
    new ReceiptLineCalculator(
         new PercentageTaxStrategy(),
       new CappedDiscountStrategy(new CompositeDiscountStrategy
       (new List<IDiscountStrategy> { new BooksDiscountStrategy(), new QuantityDiscountStrategy(), new ClearanceDiscountStrategy() })
       )
       );
        
  

        return receiptLineCalculator.CalculateLineTotal(item);
    }
}
