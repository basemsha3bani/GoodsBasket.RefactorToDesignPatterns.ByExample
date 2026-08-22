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
}
