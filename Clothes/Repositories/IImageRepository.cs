using Clothes.Models;

namespace Clothes.Repositories
{
    public interface IImageRepository
    {
        Task<Image> UploadImage(Image image);
    }
}
