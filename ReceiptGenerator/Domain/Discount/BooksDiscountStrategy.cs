using ReceiptGenerator.Domain.Product;

namespace ReceiptGenerator.Domain.Discount
{
    public class BooksDiscountStrategy:IDiscountStrategy
    {
                public decimal calculateDiscount(BasketItem item, decimal lineTotal)
        {
            decimal discount = 0;
            if (item.product.Category == "book")
                discount += lineTotal * 0.05m;
            if (discount > lineTotal)
                discount = lineTotal;
            return discount;
        }
    }   
}
