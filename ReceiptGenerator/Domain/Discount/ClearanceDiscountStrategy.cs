using ReceiptGenerator.Domain.Product;

namespace ReceiptGenerator.Domain.Discount
{
    public class ClearanceDiscountStrategy : IDiscountStrategy
    {
        public decimal calculateDiscount(BasketItem item, decimal lineTotal)
        {
            decimal discount = 0;
            if (item.product.Name.ToLowerInvariant().Contains("clearance"))
                discount += lineTotal * 0.20m;
            if (discount > lineTotal)
                discount = lineTotal;
            return discount;
        }
    }
}
