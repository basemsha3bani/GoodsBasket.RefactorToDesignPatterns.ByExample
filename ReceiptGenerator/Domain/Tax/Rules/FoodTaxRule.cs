using ReceiptGenerator.Domain.Product;

namespace ReceiptGenerator.Domain.Tax.Rules
{
    public sealed class FoodTaxRule : ITaxRule
    {
        public int Priority => 100;

        public bool AppliesTo(BasketItem item)
        {
           return item.product.Category == "food";
        }

        public decimal CalculateTax(BasketItem item, decimal taxableAmount)
        {
            return 0;
        }

       
    }
}
