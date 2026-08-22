using ReceiptGenerator.Domain.Product;

namespace ReceiptGenerator.Domain.Discount
{
    public class CappedDiscountStrategy : IDiscountStrategy
    {
        IDiscountStrategy inner;

        public CappedDiscountStrategy(IDiscountStrategy inner)
        {
            this.inner = inner;
        }

        public decimal calculateDiscount(BasketItem item, decimal lineTotal)
        {
            decimal discount = inner.calculateDiscount(item, lineTotal);
            return discount > lineTotal ? lineTotal : discount;
        }
    }
}
