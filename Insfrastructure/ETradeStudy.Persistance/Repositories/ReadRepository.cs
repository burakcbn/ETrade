using ETradeStudy.Application.Repositories;
using ETradeStudy.Application.Repositories.Dynamic;
using ETradeStudy.Domain.Entities.Common;
using ETradeStudy.Percistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Percistance.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ETradeStudyContext _context;

        public ReadRepository(ETradeStudyContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();


        public IQueryable<T> GetListByDynamic(Dynamic dynamic, bool enableTracking = true)
        {
            IQueryable<T> queryable = Table.ToDynamic(dynamic);
            if (!enableTracking) queryable = queryable.AsNoTracking();
            return queryable;
        }
        public IQueryable<T> GetAll(bool isTracking=true)
        {
            var table= Table.AsQueryable();
            if (!isTracking)
            {
                table.AsNoTracking();
            }
            return table;
        }

        public async Task<T> GetByIdAsync(string id, bool isTracking)
        {
            var table = Table.AsQueryable();
            if (!isTracking)
            {
                table.AsNoTracking();
            }
            return await table.FirstOrDefaultAsync(x=>x.Id==Guid.Parse(id));
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool isTracking = true)
        {
            var table = Table.AsQueryable();
            if (!isTracking)
            {
                table.AsNoTracking();
            }
            return await table.FirstOrDefaultAsync(method);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool isTracking)
        {
            var table = Table.AsQueryable();
            if (!isTracking)
            {
                table.AsNoTracking();
            }
            return table.Where(method);
        }
    }
}
