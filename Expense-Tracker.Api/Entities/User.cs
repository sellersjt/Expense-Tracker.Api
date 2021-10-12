using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Expense_Tracker.Api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public bool CanMessage { get; set; } = true;
        [JsonIgnore]
        public string Role { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
