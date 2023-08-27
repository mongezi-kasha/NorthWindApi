namespace Northwind.Models.Invoices
{
    public class UpdateInvoiceRequest
    {
        public int InvoiceId { get; set; }
        public string InvoiceName { get; set; }
    }
}
