using Clothes.Models;

namespace Clothes.Repositories
{
    public interface ICateogriesRepository
    {
        Task<Category> CreateNewCategory(Category category);
    }
}
