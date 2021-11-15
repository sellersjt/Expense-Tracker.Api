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
        CategoryResponse Create(CreateCategoryRequest model, Account account);
        CategoryResponse Update(int id, UpdateCategoryRequest model, Account account);
        void Delete(int id, Account account);
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

            // users can get their own category and admins can get any category
            if (category.CreaterId != account.Id && account.Role != Role.Admin)
                throw new AppException("Unauthorized");

            return _mapper.Map<CategoryResponse>(category);
        }

        public  CategoryResponse Create(CreateCategoryRequest model, Account account)
        {
            var category = _mapper.Map<Category>(model);
            category.CreaterId = account.Id;
            if (account.Role != Role.Admin)
            {
                category.IsGlobal = false;
            }

            _context.Categories.Add(category);
            _context.SaveChanges();

            return _mapper.Map<CategoryResponse>(category);
        }

        public CategoryResponse Update(int id, UpdateCategoryRequest model, Account account)
        {
            var category = getCategory(id);

            // users can update their own category and admins can update any category
            if (category.CreaterId != account.Id && account.Role != Role.Admin)
                throw new AppException("Unauthorized");

            // only admins can update IsGlobal
            if (account.Role != Role.Admin)
                model.IsGlobal = false;

            _mapper.Map(model, category);

            _context.Categories.Update(category);
            _context.SaveChanges();

            return _mapper.Map<CategoryResponse>(category);
        }

        public void Delete(int id, Account account)
        {
            var category = getCategory(id);

            // users can delete their own category and admins can delete any category
            if (category.CreaterId != account.Id && account.Role != Role.Admin)
                throw new AppException("Unauthorized");

            // ToDo - Check if category is used, if so change where used to category 1 "Uncategorized" befor deleteing.

            _context.Categories.Remove(category);
            _context.SaveChanges();
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
