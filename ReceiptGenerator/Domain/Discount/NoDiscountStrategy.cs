using ReceiptGenerator.Domain.Product;

namespace ReceiptGenerator.Domain.Discount
{
    public class NoDiscountStrategy : IDiscountStrategy
    {
        public decimal calculateDiscount(BasketItem basketItem,decimal subtotal)
        {
            return 0;
        }
    }
}
