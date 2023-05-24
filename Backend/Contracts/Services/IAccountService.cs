﻿using Common.DTO;
using Common.DTO.AccountDTOs;

namespace Contracts.Services
{
    public interface IAccountService
    {
        Task<ResponseDTO> GetAccountById(Guid id);
        ResponseDTO UpdateAccount(Guid id, EditAccountDTO accountDTO);
        Task<ResponseDTO> MakeAccount(CreateAccountDTO createAccountDTO);
    }
}