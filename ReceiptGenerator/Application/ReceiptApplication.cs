using ReceiptGenerator.Domain.Product;
using ReceiptGenerator.Domain.ReceiptEntities;
using ReceiptGenerator.infrastructure;
using ReceiptGenerator.infrastructure.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptGenerator.Application
{
    internal class ReceiptApplication
    {
        readonly infrastructure.Rendering.IReceiptRenderer _renderer;
        readonly infrastructure.IreceiptWriter _writer;
        private readonly IBasketReader basketReader;
        private readonly ReceiptLineCalculator receiptLineCalculator;

        public ReceiptApplication(IReceiptRenderer renderer, IreceiptWriter writer, IBasketReader basketReader, ReceiptLineCalculator receiptLineCalculator)
        {
            _renderer = renderer;
            _writer = writer;
            this.basketReader = basketReader;
            this.receiptLineCalculator = receiptLineCalculator;
        }

        public  void GenerateReceipt(string sourcePath, string outputpath   )
        {
            IReadOnlyList<BasketItem> items =
             basketReader.ReadBasket(sourcePath);

            var lines = items
                .Select(receiptLineCalculator.CalculateLineTotal)
                .ToList();

            var receipt = new Receipt(lines);
            var output = _renderer.RenderReceipt(receipt);
            _writer.WriteReceipt(outputpath, new StringBuilder(output));
        }   
    }
}
