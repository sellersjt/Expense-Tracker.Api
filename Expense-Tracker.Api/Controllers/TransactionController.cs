using Expense_Tracker.Api.Helpers;
using Expense_Tracker.Api.Models.Transaction;
using Expense_Tracker.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Expense_Tracker.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TransactionResponse>> GetAll()
        {
            var transactions = _transactionService.GetAll(Account);
            return Ok(transactions);
        }

        [HttpGet("{id:int}")]
        public ActionResult<IEnumerable<TransactionResponse>> GetById(int id)
        {
            var transaction = _transactionService.GetById(id, Account);
            return Ok(transaction);
        }

        [HttpPost]
        public ActionResult<ReturnTransactionResponse> Create(CreateTransactionRequest model)
        {
            var category = _transactionService.Create(model, Account);
            return Ok(category);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ReturnTransactionResponse> Update(int id, UpdateTransactionRequest model)
        {
            var category = _transactionService.Update(id, model, Account);
            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ReturnBalanceResponse> Delete(int id)
        {
            var newBalance = _transactionService.Delete(id, Account);
            return Ok(newBalance);
        }
    }
}
