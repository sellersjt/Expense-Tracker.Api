using AutoMapper;
using Expense_Tracker.Api.Entities;
using Expense_Tracker.Api.Helpers;
using Expense_Tracker.Api.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Api.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryResponse> GetAll(Account account);
        CategoryResponse GetById(int id, Account account);
    }

    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoryService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<CategoryResponse> GetAll(Account account)
        {
            if (account.Role == Role.Admin)
            {
                var categories = _context.Categories;
                return _mapper.Map<IList<CategoryResponse>>(categories);
            }
            else
            {
                var categories = _context.Categories.Where(x => x.IsGlobal | x.CreaterId == account.Id);
                return _mapper.Map<IList<CategoryResponse>>(categories);
            }
        }

        public CategoryResponse GetById(int id, Account account)
        {
            var category = getCategory(id);

            // users can get their own category and admins can get any account
            if (category.CreaterId != account.Id && account.Role != Role.Admin)
                throw new AppException("Unauthorized");

            return _mapper.Map<CategoryResponse>(category);
        }


        // helper methods
        private Category getCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) throw new KeyNotFoundException("Category not found");
            return category;
        }
    }
}
