using Common.DTO;
using Common.DTO.AccountDTOs;

namespace Contracts.Services
{
    public interface IAccountService
    {
        Task<ResponseDTO> GetAccountById(Guid id);
        Task<ResponseDTO> UpdateAccountAsync(Guid id, EditAccountDTO accountDTO);
        Task<ResponseDTO> MakeAccount(CreateAccountDTO createAccountDTO);
        Task<ResponseDTO> TransferBalanceToInvestedBalance(Guid id, decimal amount);
        Task<ResponseDTO> WithdrawInvestedBalance(Guid id, decimal amount);
    }
}
