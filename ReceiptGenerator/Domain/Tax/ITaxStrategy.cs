using ReceiptGenerator.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptGenerator.Domain.Tax
{
    public interface ITaxStrategy
    {
        decimal CalculateTax(BasketItem item);
    }
    public class NoTaxStrategy : ITaxStrategy
    {
        public decimal CalculateTax(BasketItem item)
        {
            return 0;
        }
    }
    public class PercentageTaxStrategy : ITaxStrategy
    {
       
        public PercentageTaxStrategy()
        {
           
        }
        public decimal CalculateTax(BasketItem item)
        {
            decimal taxableAmount = item.Quantity * item.UnitPrice;
            decimal tax = 0;
            if (item.product.Category == "food")
                tax = 0;
            else if (item.product.Category == "luxury")
                tax = taxableAmount * 0.20m;
            else
                tax = taxableAmount * 0.10m;
            return tax;
        }
    }
}
