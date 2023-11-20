using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using NorthWind.DAL;
using NorthWind.Services;
using System.Linq.Expressions;

namespace NorthWind.UnitTest
{
    public class InvoiceServiceTest
    {
        [Fact]
        public async Task Should_Return_Specific_Invoice_By_Id()
        {
            // Arrange
            int invoiceId = 1;
            var mockDbContext = new Mock<NorthWindContext>();
            var invoices = new List<Invoice>
            {
                new Invoice { invoiceId = 1 }
            };

            var mockDbSet = new Mock<DbSet<Invoice>>();
            var queryable = invoices.AsQueryable();

            mockDbSet.As<IQueryable<Invoice>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Invoice>(queryable.Provider));
            mockDbSet.As<IQueryable<Invoice>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockDbSet.As<IQueryable<Invoice>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockDbSet.As<IQueryable<Invoice>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            // Use a TestAsyncQueryProvider for the asynchronous operations
            mockDbSet.As<IQueryable<Invoice>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Invoice>(queryable.Provider));

            mockDbContext.Setup(x => x.Invoices).Returns(mockDbSet.Object);

            var invoiceService = new InvoiceService(mockDbContext.Object);

            // Act
            var result = await invoiceService.GetInvoice(invoiceId);

            // Assert
            Assert.Equal(invoiceId, result?.invoiceId);
        }
    }
}

public class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
{
    private readonly IQueryProvider _inner;

    internal TestAsyncQueryProvider(IQueryProvider inner)
    {
        _inner = inner;
    }

    public IQueryable CreateQuery(Expression expression)
    {
        return new TestAsyncEnumerable<TEntity>(expression);
    }

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
        return new TestAsyncEnumerable<TElement>(expression);
    }

    public object Execute(Expression expression)
    {
        return _inner.Execute(expression);
    }

    public TResult Execute<TResult>(Expression expression)
    {
        return _inner.Execute<TResult>(expression);
    }

    public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
    {
        return new TestAsyncEnumerable<TResult>(expression);
    }

    public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
    {
        return Task.FromResult(Execute<TResult>(expression));
    }

    TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

// Helper class to implement IAsyncEnumerable
public class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
{
    public TestAsyncEnumerable(IEnumerable<T> enumerable)
        : base(enumerable)
    { }

    public TestAsyncEnumerable(Expression expression)
        : base(expression)
    { }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
    }

    IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<T>(this);
}

// Helper class to implement IAsyncEnumerator
public class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
{
    private readonly IEnumerator<T> _inner;

    public TestAsyncEnumerator(IEnumerator<T> inner)
    {
        _inner = inner;
    }

    public T Current => _inner.Current;

    public ValueTask<bool> MoveNextAsync()
    {
        return new ValueTask<bool>(_inner.MoveNext());
    }

    public ValueTask DisposeAsync()
    {
        _inner.Dispose();
        return default;
    }
}