using Clothes.Models;

namespace Clothes.Repositories
{
    public interface IClothItemRespository
    {
        Task<ClotheItem> CreateNewClothItem(ClotheItem item);
        Task<List<ClotheItem>> GetAllClothes(string? name=null);
        Task<ClotheItem>UpdateClothItem(ClotheItem item,int id);
        Task<ClotheItem>DeleteClothItem(int id);
        Task<ClotheItem>GetClothItemById(int id);
    }
}
