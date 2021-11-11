using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Api.Models.Category
{
    public class CreateCategoryRequest
    {
        [Required]
        public string Name { get; set; }
        public bool IsGlobal { get; set; }
    }
}
