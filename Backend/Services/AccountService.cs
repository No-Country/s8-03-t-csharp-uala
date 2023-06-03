using Common.DTO;
using Common.DTO.AccountDTOs;
using Contracts.Repositories;
using Contracts.Services;
using DataAccess.Models.ApplicationModels;
using Services.Extensions;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

      
        public async Task<ResponseDTO> GetAccountById(Guid id)
        {
            var account = await _accountRepository.GetAccountById(id);

            if (account == null)
            {
                return new ResponseDTO
                {
                    Success = false,
                    Result = null,
                    Message = "Account not found",
                    StatusCode = 404
                };
            }
            
            var response = new ResponseDTO
            {
                Success = true,
                Result = new AccountDTO
                {
                    Id = account.Id,
                    AccountNumber = account.AccountNumber,
                    Url_ProfilePicture = account.Url_ProfilePicture,
                    Balance = account.Balance,
                    InvestedBalance = account.InvestedBalance,
                    CVU = account.CVU,
                    Alias = account.Alias,
                    OwnerId = account.OwnerId
                },
                Message = "Account successfully found",
                StatusCode = 200
            };

            return response;
        }

        public async Task<ResponseDTO> MakeAccount(CreateAccountDTO createAccountDTO)
        {
            var account = new Account
            {
                Id = Guid.NewGuid(),
                AccountNumber = createAccountDTO.AccountNumber,
                Url_ProfilePicture= createAccountDTO.Url_ProfilePicture,
                Balance = createAccountDTO.Balance,
                InvestedBalance = createAccountDTO.InvestedBalance,
                CVU = createAccountDTO.CVU,
                Alias = createAccountDTO.Alias,
                OwnerId = createAccountDTO.OwnerId
            };

            await _accountRepository.MakeAccount(account);

            var response = new ResponseDTO
            {
                Success = true,
                Result = new AccountDTO
                {
                    Id = account.Id,
                    AccountNumber = account.AccountNumber,
                    Url_ProfilePicture = account.Url_ProfilePicture,
                    Balance = account.Balance,
                    InvestedBalance = account.InvestedBalance,
                    CVU = account.CVU,
                    Alias = account.Alias,
                    OwnerId = account.OwnerId
                },
                Message = "Account successfully created",
                StatusCode = 201
            };

            return response;
        }

        public ResponseDTO UpdateAccount(Guid id, EditAccountDTO editAccountDTO)
        {
            var account = editAccountDTO.ToEditAccountDTO(id);
            var response = _accountRepository.UpdateAccount(account);
            var dto = response.ToAccountDTO();

            var rpta = new ResponseDTO
            {
                Success = true,
                Result = dto,
                Message = "Account updated successfully",
                StatusCode = 201
            };

            return rpta;
        }

        public async Task<ResponseDTO> TransferBalanceToInvestedBalance(Guid id, decimal amount)
        {
            var account = await _accountRepository.GetAccountById(id);

            if (account == null)
            {
                return new ResponseDTO
                {
                    Success = false,
                    Result = null,
                    Message = "Account not found",
                    StatusCode = 404
                };
            }

            if (account.Balance < amount)
            {
                return new ResponseDTO
                {
                    Success = false,
                    Result = null,
                    Message = "Insufficient balance",
                    StatusCode = 400
                };
            }

            account.Balance -= amount;
            account.InvestedBalance += amount;

            await _accountRepository.UpdateAccount(account);

            var response = new ResponseDTO
            {
                Success = true,
                Result = new AccountDTO
                {
                    Id = account.Id,
                    AccountNumber = account.AccountNumber,
                    Url_ProfilePicture = account.Url_ProfilePicture,
                    Balance = account.Balance,
                    InvestedBalance = account.InvestedBalance,
                    CVU = account.CVU,
                    Alias = account.Alias,
                    OwnerId = account.OwnerId
                },
                Message = "Balance transferred to InvestedBalance successfully",
                StatusCode = 200
            };

            return response;
        }

        public async Task<ResponseDTO> WithdrawInvestedBalance(Guid id, decimal amount)
        {
            var account = await _accountRepository.GetAccountById(id);

            if (account == null)
            {
                return new ResponseDTO
                {
                    Success = false,
                    Result = null,
                    Message = "Account not found",
                    StatusCode = 404
                };
            }

            if (account.InvestedBalance < amount)
            {
                return new ResponseDTO
                {
                    Success = false,
                    Result = null,
                    Message = "Insufficient invested balance",
                    StatusCode = 400
                };
            }

            account.Balance += amount;
            account.InvestedBalance -= amount;

            await _accountRepository.UpdateAccount(account);

            var response = new ResponseDTO
            {
                Success = true,
                Result = new AccountDTO
                {
                    Id = account.Id,
                    AccountNumber = account.AccountNumber,
                    Url_ProfilePicture = account.Url_ProfilePicture,
                    Balance = account.Balance,
                    InvestedBalance = account.InvestedBalance,
                    CVU = account.CVU,
                    Alias = account.Alias,
                    OwnerId = account.OwnerId
                },
                Message = "InvestedBalance withdrawn and returned to Balance successfully",
                StatusCode = 200
            };

            return response;
        }





    }
}
