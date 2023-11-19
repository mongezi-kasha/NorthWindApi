using Microsoft.EntityFrameworkCore;
using Moq;
using NorthWind.DAL;
using NorthWind.Services;
using Xunit;

namespace NorthWind.UnitTest
{
    public class InoiceServiceTest
    {
        [Fact]
        public async Task Should_Return_All_Services()
        {
            //Arrange
            int invoiceId = 1;
            var mockDbContext = new Mock<NorthWindContext>();
            var invoices = new List<Invoice>
            {
                new Invoice{invoiceId = 1}
            };

            var mockDbSet = new Mock<DbSet<Invoice>>();
            mockDbSet.As<IQueryable<Invoice>>().Setup(m => m.Provider).Returns(invoices.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Invoice>>().Setup(m => m.Expression).Returns(invoices.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Invoice>>().Setup(m => m.ElementType).Returns(invoices.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Invoice>>().Setup(m => m.GetEnumerator()).Returns(() => invoices.AsQueryable().GetEnumerator());

            mockDbContext.Setup(x => x.Invoices).Returns(mockDbSet.Object);

            var invoiceService = new InvoiceService(mockDbContext.Object);

            //Act
            var result = await invoiceService.GetInvoice(invoiceId);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(invoiceId, result.invoiceId);
        }
    }
}