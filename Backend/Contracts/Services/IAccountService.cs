using Common.DTO;
using Common.DTO.AccountDTOs;

namespace Contracts.Services
{
    public interface IAccountService
    {
        Task<ResponseDTO> GetAccountById(Guid id);
        Task<ResponseDTO> UpdateAccount(Guid id, AccountDTO accountDTO);
        Task<ResponseDTO> MakeAccount(CreateAccountDTO createAccountDTO);
    }
}
