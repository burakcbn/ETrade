using ETradeStudy.Application.Repositories;
using ETradeStudy.Percistance.Contexts;

namespace ETradeStudy.Percistance.Repositories.Category
{
    public class CategoryRead : ReadRepository<Domain.Entities.Category>, ICategoryRead
    {
        public CategoryRead(ETradeStudyContext context) : base(context)
        {
        }
    }

}
