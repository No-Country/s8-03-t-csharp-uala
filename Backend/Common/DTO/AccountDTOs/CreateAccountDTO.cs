namespace Common.DTO.AccountDTOs
{
    public class CreateAccountDTO
    {
        public long AccountNumber { get; set; }
        //public ICollection<Transaction> Transactions { get; set; }
        public string Url_ProfilePicture { get; set; }
        public decimal Balance { get; set; }
        public decimal InvestedBalance { get; set; }
        public long CVU { get; set; }
        public string Alias { get; set; }
        public Guid OwnerId { get; set; }
    }
}
