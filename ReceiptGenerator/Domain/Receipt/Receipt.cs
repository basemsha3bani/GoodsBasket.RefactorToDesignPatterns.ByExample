using ReceiptGenerator;

internal class Receipt
{
    private List<ReceiptLine> receiptLines;

    public Receipt(List<ReceiptLine> receiptLines)
    {
        this.receiptLines = receiptLines;
    }

    public IEnumerable<ReceiptLine> Lines { get
        {
            return receiptLines;
        }
    }
}