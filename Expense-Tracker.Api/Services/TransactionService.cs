using AutoMapper;
using Expense_Tracker.Api.Entities;
using Expense_Tracker.Api.Helpers;
using Expense_Tracker.Api.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Api.Services
{
    public interface ITransactionService
    {
        IEnumerable<TransactionResponse> GetAll(Account account);
        TransactionResponse GetById(int id, Account account);
        ReturnTransactionResponse Create(CreateTransactionRequest request, Account account);
        ReturnTransactionResponse Update(int id, UpdateTransactionRequest model, Account account);
        ReturnBalanceResponse Delete(int id, Account account);
    }

    public class TransactionService : ITransactionService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TransactionService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<TransactionResponse> GetAll(Account account)
        {
            if (account.Role == Role.Admin)
            {
                var transactions = _context.Transactions;
                return _mapper.Map<IList<TransactionResponse>>(transactions);
            }
            else
            {
                var transactions = _context.Transactions.Where(x => x.CreaterId == account.Id);
                return _mapper.Map<IList<TransactionResponse>>(transactions);
            }
        }

        public TransactionResponse GetById(int id, Account account)
        {
            var transaction = getTransaction(id);

            // users can get their own transaction and admins can get any transaction
            if (transaction.CreaterId != account.Id && account.Role != Role.Admin)
                throw new AppException("Unauthorized");

            return _mapper.Map<TransactionResponse>(transaction);
        }

        public ReturnTransactionResponse Create(CreateTransactionRequest model, Account account)
        {
            var transaction = _mapper.Map<Transaction>(model);

            transaction.CreaterId = account.Id;

            account.Balance += transaction.Amount;

            _context.Transactions.Add(transaction);
            _context.Accounts.Attach(account);
            _context.Entry(account).Property(x => x.Balance).IsModified = true;
            _context.SaveChanges();

            var output = new ReturnTransactionResponse
            {
                Id = transaction.Id,
                Name = transaction.Name,
                Amount = transaction.Amount,
                Date = transaction.Date,
                CreaterId = transaction.CreaterId,
                CategoryId = transaction.CategoryId,
                NewBalance = account.Balance
            };

            //var output = _mapper.Map<ReturnTransactionResponse>(transaction);
            //output.NewBalance = account.Balance;

            return output;
        }

        public ReturnTransactionResponse Update(int id, UpdateTransactionRequest model, Account account)
        {
            var transaction = getTransaction(id);

            // users can update their own transaction and admins can update any transaction
            if (transaction.CreaterId != account.Id && account.Role != Role.Admin)
                throw new AppException("Unauthorized");

            // check if updating admin account, if not get user account to update
            Account accountToUpdate = account.Id == transaction.CreaterId ? account : getAccount(transaction.CreaterId);

            // back out old amount
            accountToUpdate.Balance -= transaction.Amount;

            // copy model to transaction
            _mapper.Map(model, transaction);

            // add new amount
            accountToUpdate.Balance += transaction.Amount;

            _context.Transactions.Update(transaction);
            _context.Accounts.Attach(accountToUpdate);
            _context.Entry(accountToUpdate).Property(x => x.Balance).IsModified = true;
            _context.SaveChanges();

            var output = new ReturnTransactionResponse
            {
                Id = transaction.Id,
                Name = transaction.Name,
                Amount = transaction.Amount,
                Date = transaction.Date,
                CreaterId = transaction.CreaterId,
                CategoryId = transaction.CategoryId,
                NewBalance = accountToUpdate.Balance
            };

            //var output = _mapper.Map<ReturnTransactionResponse>(transaction);
            //output.NewBalance = account.Balance;

            return output;
        }

        public ReturnBalanceResponse Delete(int id, Account account)
        {
            var transaction = getTransaction(id);

            // users can delete their own transaction and admins can delete any transaction
            if (transaction.CreaterId != account.Id && account.Role != Role.Admin)
                throw new AppException("Unauthorized");

            // check if updating admin account, if not get user account to update
            Account accountToUpdate = account.Id == transaction.CreaterId ? account : getAccount(transaction.CreaterId);

            // back out transaction amount
            accountToUpdate.Balance -= transaction.Amount;

            _context.Transactions.Remove(transaction);
            _context.Accounts.Attach(accountToUpdate);
            _context.Entry(accountToUpdate).Property(x => x.Balance).IsModified = true;
            _context.SaveChanges();

            var output = new ReturnBalanceResponse
            {
                Message = "Transaction deleted successfully",
                NewBalance = accountToUpdate.Balance
            };

            return output;
        }

        // helper methods
        private Transaction getTransaction(int id)
        {
            var transaction = _context.Transactions.Find(id);
            if (transaction == null) throw new KeyNotFoundException("Transaction not found");
            return transaction;
        }

        private Account getAccount(int id)
        {
            var account = _context.Accounts.Find(id);
            return account;
        }
    }
}
