using ReceiptGenerator.Domain.Product;
using ReceiptGenerator.Domain.Tax.Rules;
using ReceiptGenerator.Domain.Tax.Rules.Resolving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptGenerator.Domain.Tax
{
    public class RuleBasedTaxStrategy : ITaxStrategy
    {
        private readonly Resolver resolver;
        public RuleBasedTaxStrategy(Resolver resolver)
        {
            this.resolver = resolver;
        }
        public decimal CalculateTax(BasketItem item)
        {
            decimal taxableAmount = item.Quantity * item.UnitPrice;
            ITaxRule taxRule = resolver.resolve(item);
            return taxRule.CalculateTax(item,taxableAmount); // Default to no tax if no rules apply
        }
    }
}
