using AutoMapper;
using Clothes.DTO;
using Clothes.Models;
using Clothes.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothesController : ControllerBase
    {
        private readonly IClothItemRespository clothItemRespository;
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;

        public ClothesController(IClothItemRespository clothItemRespository,IMapper mapper,IImageRepository imageRepository)
        {
            this.clothItemRespository = clothItemRespository;
            this.mapper = mapper;
            this.imageRepository = imageRepository;
        }

        [HttpGet]
        
        public async Task<IActionResult> GetAllClothes([FromQuery]string? name)
        {
            var clothes= await clothItemRespository.GetAllClothes(name);
            return Ok(mapper.Map<List<ClothItemDto>>(clothes));
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewClothItem([FromBody]CreateNewClothItemDto createNewClothItemDto)
        {
            var createdItem=await clothItemRespository.CreateNewClothItem(mapper.Map<ClotheItem>(createNewClothItemDto));
            return Ok(mapper.Map<ClotheItem>(createdItem));
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateClothItem([FromRoute]int id, [FromBody]UpdateClothItemDto updateClothItemDto)
        {
            var updatedClothes = await clothItemRespository.UpdateClothItem(mapper.Map<ClotheItem>(updateClothItemDto), id);
            if(updatedClothes is null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ClothItemDto>(updatedClothes));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteClothItem([FromRoute]int id)
        {
            var deletedCloth = await clothItemRespository.DeleteClothItem(id);
            if(deletedCloth is null)
            {
                return NotFound();
            }
            return Ok(new{ message="Item Deleted Successfully"});
        }

        [HttpPost]
        [Route("AddNewImg")]
        public async Task<IActionResult> UploadImg([FromForm]ImageUploadDto imageUploadDto)
        {
            var imageModel = new Image()
            {
                File = imageUploadDto.File,
                FileExtestion = Path.GetExtension(imageUploadDto.File.FileName),
                FileName = imageUploadDto.FileName,
                ClotheItemId = imageUploadDto.ClotheItemId,
            };
            await imageRepository.UploadImage(imageModel);
            return Ok(imageModel);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult>GetClothItemById(int id)
        {
            var clothItem = await clothItemRespository.GetClothItemById(id);
            if(clothItem is null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ClothItemDto>(clothItem));
        }
    }
}
