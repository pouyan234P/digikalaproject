using Digikala.Services.Product.Data.Repository;
using Digikala.Services.Product.DTO;
using Digikala.Services.Product.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digikala.Services.Product.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpPost("addCategory")]
        public async Task<IActionResult> addCategory([FromBody]SetCategoryDTO categoryDTO)
        {
            var getparent = await _categoryRepository.Getcategoryidbyname(categoryDTO.CategoryName);
            var getparentid = await _categoryRepository.GetCategory(categoryDTO.ID);
            if (getparent != null || getparentid!=null)
            {
                if ((getparent == null & getparentid != null) ||(getparent!=null &getparentid==null))
                {
                    var mycategory = new Category
                    {
                        CategoryName = categoryDTO.CategoryName,
                        CategoryParent = getparentid.ID
                    };
                    _categoryRepository.addCategory(mycategory);
                }
                else
                {
                    return BadRequest("nothing");
                }
            }

            else
            {
                Category parentCategory = null;
                var mycategory = new Category
                {
                    CategoryName = categoryDTO.CategoryName,
                    ParentCategory = parentCategory
                };
                _categoryRepository.addCategory(mycategory);
            }
            return Ok();
        }
    }
}
