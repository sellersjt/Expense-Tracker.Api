using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Api.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreaterId  { get; set; }
        public bool IsGlobal { get; set; }
    }
}
