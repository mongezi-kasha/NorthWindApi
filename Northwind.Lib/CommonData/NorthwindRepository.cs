using Ardalis.Specification.EntityFrameworkCore;
using NorthWind.DAL;

namespace Northwind.Lib.CommonData
{
    public class NorthWindRepository<T> : RepositoryBase<T> where T : class
    {
        private readonly NorthWindContext _dbContext;
        public NorthWindRepository(NorthWindContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
