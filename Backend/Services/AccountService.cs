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

        //public async Task<ResponseDTO> GetAllTransactionsByAccountId(Guid id)
        //{
        //    var transactions = await _transactionRepository.GetAllTransactionsByAccountId(id);

        //    if (transactions == null)
        //    {
        //        return new ResponseDTO
        //        {
        //            Success = false,
        //            Result = null,
        //            Message = "No transactions found for the specified account ID",
        //            StatusCode = 404
        //        };
        //    }

        //    var transactionDTOs = new List<TransactionDTO>();

        //    foreach (var transaction in transactions)
        //    {
        //        var transactionDTO = new TransactionDTO
        //        {
        //            Id = transaction.Id,
        //            Date = transaction.Date,
        //            Amount = transaction.Amount,
        //            Motive = transaction.Motive,
        //            Reference = transaction.Reference,
        //            SourceAccountId = transaction.SourceAccountId,
        //            DestinationAccountId = transaction.DestinationAccountId
        //        };

        //        transactionDTOs.Add(transactionDTO);
        //    }

        //    var response = new ResponseDTO
        //    {
        //        Success = true,
        //        Result = transactionDTOs,
        //        Message = "Transactions successfully retrieved",
        //        StatusCode = 200
        //    };

        //    return response;
        //}


        public async Task<ResponseDTO> GetAccountById(Guid id)
        {
            var account = await _accountRepository.GetAccountById(id);

            if (account == null)
            {
                return new ResponseDTO
                {
                    Success = false,
                    Result = null,
                    Message = "Transaction not found",
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
                    //ICollection <Transaction> Transactions=,
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
                //ICollection <Transaction> Transactions=,
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
                    //ICollection <Transaction> Transactions=,
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

            var rpta = new ResponseDTO()
            {
                Success = true,
                Result = dto,
                Message = "Account updated successfully",
                StatusCode = 201
            };

            return rpta;
        }
    }
}
