using Clothes.Data;
using Clothes.Models;
using Microsoft.EntityFrameworkCore;

namespace Clothes.Repositories
{
    public class SqlClothItemRepository : IClothItemRespository
    {
        private readonly DataDbContext dbContext;

        public SqlClothItemRepository(DataDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ClotheItem> CreateNewClothItem(ClotheItem item)
        {
            await dbContext.Clothes.AddAsync(item);
            await dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<ClotheItem> DeleteClothItem(int id)
        {
            var clothItemToDelete=await dbContext.Clothes.FirstOrDefaultAsync(c => c.Id == id);
            if (clothItemToDelete == null)
            {
                return null;
            }
            dbContext.Clothes.Remove(clothItemToDelete);
            await dbContext.SaveChangesAsync();
            return clothItemToDelete;
        }

        public async Task<List<ClotheItem>> GetAllClothes(string? name=null)
        {

            if (string.IsNullOrEmpty(name)==false)
            {
                var clothes = await dbContext.Clothes.Where(x=>x.Name.Contains(name)).Include("Category").ToListAsync();
                foreach (var item in clothes)
                {
                    var imgPaths = await dbContext.Images.Where(img => img.ClotheItemId == item.Id).ToListAsync();
                    item.ImgsPaths = imgPaths;
                }
                return clothes;

            }
           else
            {
                var clothes = await dbContext.Clothes.Include("Category").ToListAsync();

                foreach (var item in clothes)
                {
                    var imgPaths = await dbContext.Images.Where(img => img.ClotheItemId == item.Id).ToListAsync();
                    item.ImgsPaths = imgPaths;
                }
                return clothes;
            }
        }

        public async Task<ClotheItem> GetClothItemById(int id)
        {
            var clotheItem=await dbContext.Clothes.Include("Category").FirstOrDefaultAsync(x=>x.Id == id);
            if (clotheItem is null)
            {
                return null;
            }
            var imgPaths = await dbContext.Images.Where(img => img.ClotheItemId == clotheItem.Id).ToListAsync();
            clotheItem.ImgsPaths = imgPaths;
            
            return clotheItem;
        }

        public async Task<ClotheItem> UpdateClothItem(ClotheItem item,int id)
        {
            var cloth=await dbContext.Clothes.FirstOrDefaultAsync(x=>x.Id==id);
            if (cloth==null)
            {
                return null;
            }
            cloth.Name = item.Name;
            cloth.Description = item.Description;
            cloth.CategoryId = item.CategoryId;
            cloth.discount = item.discount;
            cloth.Quanitity= item.Quanitity;
            cloth.hasDiscount= item.hasDiscount;
            cloth.discount=item.discount;
            cloth.price=item.price;
            await dbContext.SaveChangesAsync();
            return item;
        }
    }
}
