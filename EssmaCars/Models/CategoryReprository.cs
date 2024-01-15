using EssmaCars.Models;

namespace SweetEssma.Models
{
    public class CategoryReprository :ICategoryRepository
    {
        private readonly SweetEssmaDbContext _sweetEssmaDbContext;

        public CategoryReprository(SweetEssmaDbContext sweetEssmaDbContext)
        {
            _sweetEssmaDbContext = sweetEssmaDbContext;
        }

        public IEnumerable<Category> AllCategories =>
            _sweetEssmaDbContext.Categories.OrderBy(p => p.CategoryName);
    }
}
