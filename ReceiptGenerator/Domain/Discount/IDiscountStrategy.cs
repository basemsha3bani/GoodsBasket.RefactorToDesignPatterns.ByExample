using ReceiptGenerator.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptGenerator.Domain.Discount
{
    public interface IDiscountStrategy
    {
        public decimal calculateDiscount(BasketItem basketItem,decimal subtotal);
    }
    public class NoDiscountStrategy : IDiscountStrategy
    {
        public decimal calculateDiscount(BasketItem basketItem,decimal subtotal)
        {
            return 0;
        }
    }
    public  class PercentageDiscountStrategy : IDiscountStrategy
    {
        
        public PercentageDiscountStrategy()
        {
           
        }
        public decimal calculateDiscount(BasketItem item, decimal lineTotal)
        {
            decimal discount = 0;
            if (item.Quantity >= 3)
                discount += lineTotal * 0.10m;

            if (item.product.Category == "book")
                discount += lineTotal * 0.05m;

            if (item.product.Name.ToLowerInvariant().Contains("clearance"))
                discount += 2.00m;

            if (discount > lineTotal)
                discount = lineTotal;

            return discount;
        }
    }
}
