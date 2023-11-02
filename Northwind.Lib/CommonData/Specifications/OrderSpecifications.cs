using Ardalis.Specification;
using NorthWind.DAL;

namespace Northwind.Lib.CommonData.Specifications
{
    public class OrderByCustomerIdSpec : SingleResultSpecification<Order>
    {
        public OrderByCustomerIdSpec(string customerId)
        {
           Query.Where(x => x.CustomerId == customerId).OrderByDescending(x => x.CustomerId);
        }
    }
}

