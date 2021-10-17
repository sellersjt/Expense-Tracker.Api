using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Api.Models.Accounts
{
    public class RevokeTokenRequest
    {
        public string Token { get; set; }
    }
}
