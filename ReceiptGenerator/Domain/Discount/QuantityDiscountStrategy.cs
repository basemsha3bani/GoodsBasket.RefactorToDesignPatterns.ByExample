using ReceiptGenerator.Domain.Product;

namespace ReceiptGenerator.Domain.Discount
{
    public class QuantityDiscountStrategy : IDiscountStrategy
    {
        public QuantityDiscountStrategy()
        {
        }

        public decimal calculateDiscount(BasketItem item, decimal lineTotal)
        {
            decimal discount = 0;
            if (item.Quantity >= 3)
                discount += lineTotal * 0.10m;
            if (discount > lineTotal)
                discount = lineTotal;
            return discount;
        }
    }
}
