using BlazorExpenseTracker2.Data.Repositories;
using BlazorExpenseTracker2.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlazorExpenseTracker2.API.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class ExpenseController : Controller
    {
        private readonly IExpenseRepository _ExpenseRepository;

        public ExpenseController(IExpenseRepository expenseRepository)
        {
            _ExpenseRepository = expenseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            return Ok(await _ExpenseRepository.GetAllExpenses());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseDetails(int id)
        {
            return Ok( await _ExpenseRepository.GetExpenseDetails(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpenses([FromBody] Expense expense)
        {
            if (expense == null)
            {
                return BadRequest();
            }

            if (expense.Amount < 0)
            {
                ModelState.AddModelError("Name","Amount should't be empty");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _ExpenseRepository.InsertExpenseDetails(expense);

            return Created("created", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateExpense([FromBody] Expense expense)
        {
            if (expense == null)
            {
                return BadRequest();
            }

            if (expense.Amount < 0)
            {
                ModelState.AddModelError("Name", "Amount should't be empty");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           await _ExpenseRepository.UpdateExpense(expense);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            await _ExpenseRepository.DeleteExpense(id);

            return NoContent();
        }
    }
}
