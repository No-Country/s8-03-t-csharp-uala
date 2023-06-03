using Common.DTO;
using Common.DTO.AccountDTOs;
using Contracts.Repositories;
using Contracts.Services;
using DataAccess.Models.ApplicationModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace UalaReplicaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _AccountService;
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountService AccountService, IAccountRepository accountRepository)
        {
            _AccountService = AccountService;
            _accountRepository = accountRepository;
        }

        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(Guid id)
        {
            var response = await _AccountService.GetAccountById(id);

            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response.Message);
            }

            return Ok(response);

        }

        [HttpPost("post")]
        public async Task<IActionResult> MakeAccount([FromBody] CreateAccountDTO createAccountDTO)
        {
            var response = await _AccountService.MakeAccount(createAccountDTO);

            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response.Message);
            }

            return Ok(response);
        }
        
        [HttpPut("put")]
        public ActionResult PutAccount(Guid id, EditAccountDTO editAccountDTO)
        {
            var response = _AccountService.UpdateAccount(id, editAccountDTO);
            if(!response.Success)
            {
                return StatusCode(response.StatusCode, response.Message);
            }
            return Ok(response);
        }

        [HttpGet("getMonthlyReturns")]
        public async Task<IActionResult> CalculateApproximateMonthlyReturns(Guid accountId)
        {
            var account = await _accountRepository.GetAccountById(accountId);

            if (account == null)
            {
                return NotFound();
            }

            if (account.InvestedBalance < 0)
            {
                return BadRequest();
            }

            decimal dailyReturn = 0.0006m; // 0.06%
            decimal actualBalance = account.InvestedBalance;

            for (int i = 0; i < 30; i++)
            {
                actualBalance += actualBalance * dailyReturn;
            }

            decimal monthlyReturns = actualBalance - account.InvestedBalance;
            return Ok(monthlyReturns);
        }

        [HttpPost("transferBalanceToInvestedBalance/{id}/{amount}")]
        public async Task<IActionResult> TransferBalanceToInvestedBalance(Guid id, decimal amount)
        {
            var response = await _AccountService.TransferBalanceToInvestedBalance(id, amount);

            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response.Message);
            }

            return Ok(response);
        }

        [HttpPost("withdrawInvestedBalance/{id}/{amount}")]
        public async Task<IActionResult> WithdrawInvestedBalance(Guid id, decimal amount)
        {
            var response = await _AccountService.WithdrawInvestedBalance(id, amount);

            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response.Message);
            }

            return Ok(response);
        }
    }
}
