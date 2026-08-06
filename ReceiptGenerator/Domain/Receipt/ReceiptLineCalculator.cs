using ReceiptGenerator.Domain.Discount;
using ReceiptGenerator.Domain.Product;
using ReceiptGenerator.Domain.Tax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptGenerator.Domain.Receipt
{
    public class ReceiptLineCalculator
    {
        private readonly ITaxStrategy _taxStrategy; 
        private readonly IDiscountStrategy _discountStrategy;
        public ReceiptLineCalculator(ITaxStrategy taxStrategy,IDiscountStrategy discountStrategy)
        {
            _taxStrategy = taxStrategy;
            _discountStrategy = discountStrategy;
        }
        public ReceiptLine CalculateLineTotal(BasketItem item )
        {
            decimal subtotal = item.Quantity * item.UnitPrice;
            decimal discount = _discountStrategy.calculateDiscount(item, subtotal);
            decimal tax = _taxStrategy.CalculateTax(item);

            
            return new ReceiptLine(item, subtotal, discount, tax)  ;
        }

      
    }
}
