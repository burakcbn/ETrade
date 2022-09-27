using ETradeStudy.Application.Repositories;
using ETradeStudy.Percistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Percistance.Repositories
{
    public class FileRead:ReadRepository<ETradeStudy.Domain.Entities.File>,IFileRead
    {
        public FileRead(ETradeStudyContext context):base(context)
        {

        }
    }
}
