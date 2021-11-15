using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Api.Models.Transaction
{
    public class ReturnBalanceResponse
    {
        public string Message { get; set; }
        public decimal NewBalance { get; set; }
    }
}
