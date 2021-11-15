using AutoMapper;
using Expense_Tracker.Api.Entities;
using Expense_Tracker.Api.Models.Accounts;
using Expense_Tracker.Api.Models.Category;
using Expense_Tracker.Api.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Api.Helpers
{
    public class AutoMapperProfile : Profile
    {
        // mappings between model and entity objects
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountResponse>();

            CreateMap<Account, AuthenticateResponse>();

            CreateMap<RegisterRequest, Account>();

            CreateMap<CreateRequest, Account>();

            CreateMap<UpdateRequest, Account>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        // ignore null role
                        if (x.DestinationMember.Name == "Role" && src.Role == null) return false;

                        return true;
                    }
                ));

            CreateMap<Category, CategoryResponse>();

            CreateMap<CreateCategoryRequest, Category>();

            CreateMap<UpdateCategoryRequest, Category>();

            CreateMap<Transaction, TransactionResponse>();

            CreateMap<CreateTransactionRequest, Transaction>();

            //CreateMap<Transaction, ReturnTransactionResponse>().ForMember(dest => dest.NewBalance, opt => opt.Ignore());

            CreateMap<UpdateTransactionRequest, Transaction>();
        }
    }
}
