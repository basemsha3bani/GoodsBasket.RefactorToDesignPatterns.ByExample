using ReceiptGenerator.Domain.Product;
using ReceiptGenerator.Domain.Tax.Rules;

namespace ReceiptGenerator.Domain.Tax
{
    public sealed class StandardTaxRule : ITaxRule
    {
        public int Priority => Rules.Priority.TaxRulePriorities.Fallback;

        public bool AppliesTo(BasketItem item)
        {
            return true;
        }

        public decimal CalculateTax(BasketItem item, decimal taxableAmount)
        {
                return taxableAmount*0.10m;
        }

      
    }
}
