using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptGenerator
{
    internal class BasketItem
    {
       
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

       public Product product { get; }
       
        public BasketItem(string name, int quantity, decimal price, string category)
        {
           
            Quantity = quantity;
            UnitPrice = price;
            product = new Product(name, category);
        }
    }
    
    

}
