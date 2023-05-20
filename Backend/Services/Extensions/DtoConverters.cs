using Common.DTO.AccountDTOs;
using DataAccess.Models.ApplicationModels;

namespace Services.Extensions;

public static class DtoConverters
{
    public static Account ToAccount(this AccountDTO accountDTO)
        => new()
        {
            Id = accountDTO.Id,
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
}
