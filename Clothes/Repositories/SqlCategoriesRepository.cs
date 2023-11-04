using Clothes.Data;
using Clothes.DTO;
using Clothes.Models;

namespace Clothes.Repositories
{
    public class SqlCategoriesRepository:ICateogriesRepository
    {
        private readonly DataDbContext dbContext;

        public SqlCategoriesRepository(DataDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Category> CreateNewCategory(Category category)
        {
            await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }
    }
}
