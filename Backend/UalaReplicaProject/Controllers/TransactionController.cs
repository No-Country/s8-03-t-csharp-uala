using Common.DTO;
using Common.DTO.TransactionDTOs;
using Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace UalaReplicaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("AllTransactions/{id}")]
        public async Task<IActionResult> GetAllTransactions(Guid id)
        {
            var response = await _transactionService.GetAllTransactionsByAccountId(id);

            if(!response.Success)
            {
                return StatusCode(response.StatusCode, response.Message);
            }
            return Ok(response);

        }

       
        
        [HttpGet("Transaction/{id}")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            var response = await _transactionService.GetTransactionById(id);

            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response.Message);
            }

            return Ok(response);

        }

        [HttpPost]
        public async Task<IActionResult> MakeTransaction(CreateTransactionDTO createTransactionDTO)
        {
            var response = await _transactionService.MakeTransaction(createTransactionDTO);

            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response.Message);
            }

            return Ok(response);
        }


    }
}
