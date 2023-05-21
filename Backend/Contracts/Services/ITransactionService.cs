using Common.DTO;
using Common.DTO.TransactionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Services
{
    public interface ITransactionService
    {
        Task<ResponseDTO> GetAllTransactionsByAccountId(Guid id);

        Task<ResponseDTO> GetTransactionById(Guid id);

        Task<ResponseDTO> MakeTransaction(CreateTransactionDTO createTransactionDTO);
    }
}
