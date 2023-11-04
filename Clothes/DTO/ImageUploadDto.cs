using System.ComponentModel.DataAnnotations.Schema;

namespace Clothes.DTO
{
    public class ImageUploadDto
    {
       
      
        public IFormFile File { get; set; }
        public int ClotheItemId { get; set; }
        public string FileName { get; set; }
        
    }
}
