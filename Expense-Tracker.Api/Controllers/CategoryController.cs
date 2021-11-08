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
    }
}
