using ETradeStudy.Application.Abstractions.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Abstractions
{
    public interface IStorageService : IStorage
    {
        public string StorageName { get; }

    }
}
