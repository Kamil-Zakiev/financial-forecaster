using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FF.Engine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FF.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpensesController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ExpensesController> _logger;
        private readonly IAccountant _accountant;

        public ExpensesController(ILogger<ExpensesController> logger, IAccountant accountant)
        {
            _logger = logger;
            _accountant = accountant;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int lumpSum, string descr)
        {
            var expense = new Expense(lumpSum, )
            await _accountant.Expense.Add(new Expense(123, "test", new AnnualPayment(12, 1)));
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<Expense>> Get()
        {
            return await _accountant.Expense.GetAll();
        }
    }
}