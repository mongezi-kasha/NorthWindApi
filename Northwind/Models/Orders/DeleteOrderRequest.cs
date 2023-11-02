namespace Northwind.Models.Order
{
    public class DeleteOrderRequest
    {
        public string ShipName { get; set; }
        public int OrderID { get; set; }
    }
}
