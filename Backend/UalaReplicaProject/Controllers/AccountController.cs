using Common.DTO;
using Common.DTO.AccountDTOs;
using Contracts.Services;
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

        public AccountController(IAccountService AccountService)
        {
            _AccountService = AccountService;
        }

        //TODO: UpdateAccount, photo_client

        //[HttpGet("AllAccounts/{id}")]
        //public async Task<IActionResult> GetAllAccounts(Guid id)
        //{
        //    var response = await _AccountService.GetAllAccounts();

        //    if(!response.Success)
        //    {
        //        return StatusCode(response.StatusCode, response.Message);
        //    }
        //    return Ok(response);

        //}



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


        //[HttpPut("{id:guid}", Name = "UpdateAccoun")]
        [HttpPut("put")]
        public async Task<IActionResult> PutAccount(Guid id, AccountDTO accountDTO)
        {
            var response = _AccountService.UpdateAccount(id, accountDTO);
            return Ok(response);
        }
    }
}
