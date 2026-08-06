using ReceiptGenerator.Domain.Product;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptGenerator
{

    ///member of type BasketItem
    internal class ReceiptLine
    {
        public ReceiptLine(BasketItem basketItem, decimal subtotal, decimal discount, decimal tax)
        {
            this.basketItem = basketItem;
            this.subtotal = subtotal;
            this.discount = discount;
            this.tax = tax;
        }

        internal BasketItem basketItem { get; set; }

        internal decimal LineTotal
        {
            get
            {
                return subtotal - discount + tax;
            }
        }


        internal decimal subtotal { get; private set; }
        internal decimal discount { get; private set; }

        internal decimal tax { get; private set; }
    }
}
