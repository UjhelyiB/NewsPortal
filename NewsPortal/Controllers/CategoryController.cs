using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Models.CSharpModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsPortal.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private DAL dal = new DAL();

        [HttpGet("[action]")]
        public IEnumerable<CategoryModel> GetAllCategories()
        {
            return dal.GetAllCategories();
        }

        [HttpGet("{id}")]
        public async Task<CategoryModel> GetCategory([FromRoute]int id)
        {
            return await dal.GetCategory(id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute]int id)
        {
            await dal.DeleteCategory(id);
            return NoContent();
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateCategory([FromBody]CategoryModel model)
        {
            var createdCategory = await dal.CreateCategory(model);
            return Created($"api/category/{createdCategory.Id}", createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute]int id, [FromBody]CategoryModel model)
        {
            await dal.UpdateCategory(id, model);
            return Accepted(new CategoryModel
            {
                Id = id,
                Title = model.Title
            });
        }
    }
}
