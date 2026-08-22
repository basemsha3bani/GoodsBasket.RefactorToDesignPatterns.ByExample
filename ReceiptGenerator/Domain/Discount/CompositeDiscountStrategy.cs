using ReceiptGenerator.Domain.Product;

namespace ReceiptGenerator.Domain.Discount
{
    public class CompositeDiscountStrategy : IDiscountStrategy
    {
        private readonly IEnumerable<IDiscountStrategy> discountStrategies;

        public CompositeDiscountStrategy(IEnumerable<IDiscountStrategy> discountStrategies)
        {
            this.discountStrategies = discountStrategies;
        }

        public decimal calculateDiscount(BasketItem item, decimal lineTotal)
        {
            decimal discount = 0;
            if(discountStrategies != null)
            {
                foreach (var strategy in discountStrategies)
                {
                    discount += strategy.calculateDiscount(item, lineTotal);
                }
            }
            return discount > lineTotal ? lineTotal : discount;
        }
    }
}
