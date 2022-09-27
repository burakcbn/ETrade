using ETradeStudy.Application.Repositories;
using ETradeStudy.Percistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Percistance.Repositories.File
{
    public class FileWrite:WriteRepository<ETradeStudy.Domain.Entities.File>,IFileWrite
    {
        public FileWrite(ETradeStudyContext context):base(context)
        {

        }
    }
}
