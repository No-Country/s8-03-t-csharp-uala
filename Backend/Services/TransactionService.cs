using Common.DTO;
using Common.DTO.TransactionDTOs;
using Contracts.Repositories;
using Contracts.Services;
using DataAccess.Models.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<ResponseDTO> GetAllTransactionsByAccountId(Guid id)
        {
            var transactions = await _transactionRepository.GetAllTransactionsByAccountId(id);

            if (transactions == null)
            {
                return new ResponseDTO
                {
                    Success = false,
                    Result = null,
                    Message = "No transactions found for the specified account ID",
                    StatusCode = 404
                };
            }

            var transactionDTOs = new List<TransactionDTO>();

            foreach (var transaction in transactions)
            {
                var transactionDTO = new TransactionDTO
                {
                    Id = transaction.Id,
                    Date = transaction.Date,
                    Amount = transaction.Amount,
                    Motive = transaction.Motive,
                    Reference = transaction.Reference,
                    SourceAccountId = transaction.SourceAccountId,
                    DestinationAccountId = transaction.DestinationAccountId
                };

                transactionDTOs.Add(transactionDTO);
            }

            var response = new ResponseDTO
            {
                Success = true,
                Result = transactionDTOs,
                Message = "Transactions successfully retrieved",
                StatusCode = 200
            };

            return response;
        }




        public async Task<ResponseDTO> GetTransactionById(Guid id)
        {
            var transaction = await _transactionRepository.GetTransactionById(id);

            if (transaction == null)
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
                Result = new TransactionDTO
                {
                    Id = transaction.Id,
                    Date = transaction.Date,
                    Amount = transaction.Amount,
                    Motive = transaction.Motive,
                    Reference = transaction.Reference,
                    SourceAccountId = transaction.SourceAccountId,
                    DestinationAccountId = transaction.DestinationAccountId
                },
                Message = "Transaction successfully found",
                StatusCode = 200
            };

            return response;
        }

        public async Task<ResponseDTO> MakeTransaction(CreateTransactionDTO createTransactionDTO)
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Date = createTransactionDTO.Date,
                Amount = createTransactionDTO.Amount,
                Motive = createTransactionDTO.Motive,
                Reference = createTransactionDTO.Reference,
                SourceAccountId = createTransactionDTO.SourceAccountId,
                DestinationAccountId = createTransactionDTO.DestinationAccountId
            };

            await _transactionRepository.MakeTransaction(transaction);

            var response = new ResponseDTO
            {
                Success = true,
                Result = new TransactionDTO
                {
                    Id = transaction.Id,
                    Date = transaction.Date,
                    Amount = transaction.Amount,
                    Motive = transaction.Motive,
                    Reference = transaction.Reference,
                    SourceAccountId = transaction.SourceAccountId,
                    DestinationAccountId = transaction.DestinationAccountId
                },
                Message = "Transaction successfully created",
                StatusCode = 201
            };

            return response;
        }


    }
}
