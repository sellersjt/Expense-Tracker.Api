using Expense_Tracker.Api.Entities;
using Expense_Tracker.Api.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Api.Services
{
    public interface IUserService
    {
        AuthenticateResponseModel Authenticate(AuthenticateRequestModel model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Register(RegisterRequestModel model);
        void Update(int id, UpdateRequestModel model);
        void Delete(int id);
    }
}
