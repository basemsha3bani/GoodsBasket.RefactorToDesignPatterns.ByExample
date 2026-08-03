using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptGenerator
{
    internal class BasketItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Category { get; set; }
        public BasketItem(string name, int quantity, decimal price, string category)
        {
            Name = name;
            Quantity = quantity;
            UnitPrice = price;
            Category = category;
        }
    }
}
