using Common.DTO.AccountDTOs;
using DataAccess.Models.ApplicationModels;

namespace Services.Extensions;

public static class DtoConverters
{
    public static Account ToAccount(this EditAccountDTO accountDTO, EditAccountDTO dTO)
        => new()
        {
            //Id = id,
            AccountNumber = accountDTO.AccountNumber,
            Alias = accountDTO.Alias,
            Balance = accountDTO.Balance,
            CVU = accountDTO.CVU,
            InvestedBalance = accountDTO.InvestedBalance,
            OwnerId = accountDTO.OwnerId,
            Url_ProfilePicture = accountDTO.Url_ProfilePicture
        };

    public static AccountDTO ToAccountDTO(this Account account)
        => new()
        {
            Id = account.Id,
            AccountNumber = account.AccountNumber,
            Alias = account.Alias,
            Balance = account.Balance,
            CVU = account.CVU,
            InvestedBalance = account.InvestedBalance,
            OwnerId = account.OwnerId,
            Url_ProfilePicture = account.Url_ProfilePicture
        };

    public static Account ToEditAccountDTO(this EditAccountDTO editAccountDTO, Guid id)
        => new()
        {
            AccountNumber = editAccountDTO.AccountNumber,
            Alias = editAccountDTO.Alias,
            Balance = editAccountDTO.Balance,
            CVU = editAccountDTO.CVU,
            Id = id,
            InvestedBalance = editAccountDTO.InvestedBalance,
            OwnerId = editAccountDTO.OwnerId,
            Url_ProfilePicture = editAccountDTO.Url_ProfilePicture
        };
}
