using ReceiptGenerator.Domain.Product;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptGenerator.infrastructure
{
    interface IBasketReader
    {
        List<BasketItem> ReadBasket(string filePath);
    }
    public class BaseketReader : IBasketReader
    {
        public List<BasketItem> ReadBasket(string filePath)
        {
            var items = new List<BasketItem>();
            string[] lines = File.ReadAllLines(filePath);
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
                BasketItem basketItem = new     BasketItem(name, quantity, price, category);
                items.Add(basketItem);
            }
            return items;
        }
    }
}   