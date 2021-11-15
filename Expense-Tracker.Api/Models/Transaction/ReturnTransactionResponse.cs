using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Api.Models.Transaction
{
    public class ReturnTransactionResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int CreaterId { get; set; }
        public int CategoryId { get; set; }
        public decimal NewBalance { get; set; }
    }
}
