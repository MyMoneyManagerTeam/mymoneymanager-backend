using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Application.Services.Transactions;
using Application.Services.Transactions.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyMoneyManagerBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        [Authorize]
        [Route("[action]")]
        public ActionResult<CustomOutputDtoQueryTransaction> Query(int number,int page, int days)
        {
            int count = _transactionService.CountTransactions(new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
            var res = _transactionService
                .Query(new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value), number, page,days);
            return Ok(new CustomOutputDtoQueryTransaction
            {
                TotalCount = count,
                UserTransactions = res
            });
        }
        public class CustomOutputDtoQueryTransaction
        {
            public int TotalCount { get; set; }
            public IEnumerable<OutputDtoQueryTransaction> UserTransactions { get; set; }
        }

        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public ActionResult<OutputDtoCreateTransaction> Create([FromBody] InputDtoCreateTransaction inputDtoCreateTransaction)
        {
            var response = _transactionService.Create(new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),inputDtoCreateTransaction);
            if (response == null)
            {
                return BadRequest(new {message = "Impossible de créer une nouvelle transaction"});
            }
            return Ok(response);
        }
    }
}