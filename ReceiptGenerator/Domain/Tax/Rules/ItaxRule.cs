using ReceiptGenerator.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptGenerator.Domain.Tax.Rules
{
    public interface ITaxRule
    {
        int Priority { get; }

        bool AppliesTo(BasketItem item);

        decimal CalculateTax(
            BasketItem item,
            decimal taxableAmount);
    }
}
