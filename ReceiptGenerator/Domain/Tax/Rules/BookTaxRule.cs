using ReceiptGenerator.Domain.Product;

namespace ReceiptGenerator.Domain.Tax.Rules
{
    internal sealed class BookTaxRule : ITaxRule
    {
        public int Priority => Rules.Priority.TaxRulePriorities.Specific;

        public bool AppliesTo(BasketItem item) =>
                 item.product.Category == "food";

        public decimal CalculateTax(
     BasketItem item,
     decimal taxableAmount) =>
     0;


    }
}
