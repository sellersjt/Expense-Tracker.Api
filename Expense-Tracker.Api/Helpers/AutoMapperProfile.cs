using AutoMapper;
using Expense_Tracker.Api.Entities;
using Expense_Tracker.Api.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User -> AuthenticateResponseModel
            CreateMap<User, AuthenticateResponseModel>();

            // RegisterRequestModel -> User
            CreateMap<RegisterRequestModel, User>();

            // UpdateRequestModel -> User
            CreateMap<UpdateRequestModel, User>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));

        }
    }
}
