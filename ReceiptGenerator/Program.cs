using ReceiptGenerator;
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

public static int  Run(string[] args)
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
                items.Add(new BasketItem(name, quantity,price,category));
            }

            decimal subtotal = 0;
            decimal discountTotal = 0;
            decimal taxTotal = 0;
            var output = new StringBuilder();
            output.AppendLine("========================================");
            output.AppendLine("             CORNER SHOP");
            output.AppendLine("========================================");

            foreach (var item in items)
            {
                decimal lineTotal = item.Quantity * item.UnitPrice;
                decimal discount = 0;

                if (item.Quantity >= 3)
                    discount += lineTotal * 0.10m;

                if (item.product.Category == "book")
                    discount += lineTotal * 0.05m;

                if (item.product.Name.ToLowerInvariant().Contains("clearance"))
                    discount += 2.00m;

                if (discount > lineTotal)
                    discount = lineTotal;

                decimal taxableAmount = lineTotal - discount;
                decimal tax;

                if (item.product.Category == "food")
                    tax = 0;
                else if (item.product.Category == "luxury")
                    tax = taxableAmount * 0.20m;
                else
                    tax = taxableAmount * 0.10m;

                subtotal += lineTotal;
                discountTotal += discount;
                taxTotal += tax;

                string displayName = item.product.Name;
                if (displayName.Length > 18)
                    displayName = displayName.Substring(0, 15) + "...";

                output.AppendLine($"{displayName,-18} {item.Quantity,3} x {item.UnitPrice,7:0.00} = {lineTotal,8:0.00}");
                if (discount > 0)
                    output.AppendLine($"  discount                              -{discount,8:0.00}");
                output.AppendLine($"  tax                                    {tax,8:0.00}");
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
}
