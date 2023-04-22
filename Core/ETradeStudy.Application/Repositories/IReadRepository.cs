using ETradeStudy.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using D = ETradeStudy.Application.Repositories.Dynamic;
namespace ETradeStudy.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetListByDynamic(D.Dynamic dynamic, bool enableTracking = true);
        IQueryable<T> GetAll(bool isTracking = true);
        /* IQueryable<T> GetAll(Expression<Func<T, bool>> method=null);*/
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool isTracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool isTracking = true);
        Task<T> GetByIdAsync(string id, bool isTracking = true);
    }
}
