using ETradeStudy.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> models);

        bool Remove(T model);
        bool RemoveRange(List<T> models);
        Task<bool> RemoveAsync(string id);
        Task<int> SaveAsync();
        bool Update(T model);

    }
}
