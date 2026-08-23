using ReceiptGenerator.Domain.ReceiptEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptGenerator.infrastructure
{
    internal interface IreceiptWriter
    {
        void WriteReceipt(string filePath, StringBuilder output);
    }
    class receiptwriter:IreceiptWriter
    {
        public void WriteReceipt(string filePath, StringBuilder output  )
        {
           

            File.WriteAllText(filePath, output.ToString());
        }
    }
}
