using ReceiptGenerator.Application;
using ReceiptGenerator.Domain.Discount;
using ReceiptGenerator.Domain.ReceiptEntities;
using ReceiptGenerator.Domain.Tax;
using ReceiptGenerator.infrastructure;
using ReceiptGenerator.infrastructure.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptGenerator.Composition
{
    internal class ReceiptGeneratorBuilder
    {
        IDiscountStrategy discountStrategy;
        ITaxStrategy taxStrategy;
        BaseketReader BaseketReader;
        receiptwriter receiptwriter;
        receiptRenderer receiptRenderer;
        public ReceiptGeneratorBuilder UseDiscountStrategy(IDiscountStrategy discountStrategy)
        {
            this.discountStrategy = discountStrategy;
            return this;
        }
        public ReceiptGeneratorBuilder UseTaxStrategy(ITaxStrategy taxStrategy)
        {
            this.taxStrategy = taxStrategy;
            return this;
        }
        public ReceiptGeneratorBuilder UseBasketReader(BaseketReader BaseketReader)
        {
            this.BaseketReader = BaseketReader;
            return this;
        }
        public ReceiptGeneratorBuilder UseReceiptWriter(receiptwriter receiptwriter)
        {
            this.receiptwriter = receiptwriter;
            return this;
        }
        public ReceiptGeneratorBuilder UseReceiptRenderer(receiptRenderer receiptRenderer)
        {
            this.receiptRenderer = receiptRenderer;
            return this;
        }
        public ReceiptApplication Build()
        {
            if (discountStrategy == null)
                throw new InvalidOperationException("Discount strategy is not set.");
            if (taxStrategy == null)
                throw new InvalidOperationException("Tax strategy is not set.");
            if (BaseketReader == null)
                throw new InvalidOperationException("Basket reader is not set.");
            if (receiptwriter == null)
                throw new InvalidOperationException("Receipt writer is not set.");
            if (receiptRenderer == null)
                throw new InvalidOperationException("Receipt renderer is not set.");
            ReceiptLineCalculator calculator= new ReceiptLineCalculator(taxStrategy, discountStrategy);
            return new ReceiptApplication(receiptRenderer, receiptwriter, BaseketReader, calculator);
        }
    }
}
