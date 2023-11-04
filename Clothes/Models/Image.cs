using System.ComponentModel.DataAnnotations.Schema;

namespace Clothes.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
        public int ClotheItemId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileExtestion { get; set; }
    }
}
