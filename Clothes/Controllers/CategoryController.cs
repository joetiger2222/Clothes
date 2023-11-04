using AutoMapper;
using Clothes.DTO;
using Clothes.Models;
using Clothes.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CategoryController : ControllerBase
    {
        private readonly ICateogriesRepository cateogriesRepository;
        private readonly IMapper mapper;

        public CategoryController(ICateogriesRepository cateogriesRepository , IMapper mapper)
        {
            this.cateogriesRepository = cateogriesRepository;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewCategory([FromBody]CreateNewCategoryDto createNewCategoryDto)
        {
            var createdCategory = await cateogriesRepository.CreateNewCategory(mapper.Map<Category>(createNewCategoryDto));
            return Ok(mapper.Map<CategoryDto>(createdCategory));
        }
    }
}
