using ReceiptGenerator.Domain.Product;
using ReceiptGenerator.Domain.Tax.Rules.Priority;

namespace ReceiptGenerator.Domain.Tax.Rules
{
    public sealed class LuxuryTaxRule : ITaxRule
    {
        public int Priority =>
            TaxRulePriorities.Specific;

        public bool AppliesTo(BasketItem item) =>
            item.product.Category == "luxury";

        public decimal CalculateTax(
            BasketItem item,
            decimal taxableAmount) =>
            taxableAmount * 0.20m;
    }
}
