using Expense_Tracker.Api.Entities;
using Expense_Tracker.Api.Helpers;
using Expense_Tracker.Api.Models.Category;
using Expense_Tracker.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Expense_Tracker.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryResponse>> GetAll()
        {
            var categories = _categoryService.GetAll(Account);
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public ActionResult<IEnumerable<CategoryResponse>> GetById(int id)
        {
            var category = _categoryService.GetById(id, Account);
            return Ok(category);
        }

        [HttpPost]
        public ActionResult<CategoryResponse> Create(CreateCategoryRequest model)
        {
            var category = _categoryService.Create(model, Account);
            return Ok(category);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoryResponse> Update(int id, UpdateCategoryRequest model)
        {
            var category = _categoryService.Update(id, model, Account);
            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id, Account);
            return Ok(new { message = "Category deleted successfully" });
        }
    }
}
