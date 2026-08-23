using ReceiptGenerator.Domain.ReceiptEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptGenerator.infrastructure.Rendering
{
    internal interface IReceiptRenderer
    {
        string RenderReceipt(ReceiptGenerator.Domain.ReceiptEntities.Receipt receipt);
    }
    class receiptRenderer : IReceiptRenderer
    {
        public string RenderReceipt(ReceiptGenerator.Domain.ReceiptEntities.Receipt receipt)
        {
            var output = new StringBuilder();
            output.AppendLine("========================================");
            output.AppendLine("             CORNER SHOP");
            output.AppendLine("========================================");
            decimal subtotal = 0;
            decimal discountTotal = 0;
            decimal taxTotal = 0;
            foreach (var line in receipt.Lines)
            {
                output.AppendLine($"{line.basketItem.Quantity} {line.basketItem.product.Name}: {line.subtotal - line.discount + line.tax:C2}");
                subtotal += line.subtotal;
                discountTotal += line.discount;
                taxTotal += line.tax;
            }
            decimal total = subtotal - discountTotal + taxTotal;
            output.AppendLine("----------------------------------------");
            output.AppendLine($"Subtotal:                        {subtotal,10:0.00}");
            output.AppendLine($"Discounts:                      -{discountTotal,10:0.00}");
            output.AppendLine($"Tax:                             {taxTotal,10:0.00}");
            output.AppendLine($"TOTAL:                           {total,10:0.00}");
            output.AppendLine("========================================");
            return output.ToString();
        }
    }   
}
