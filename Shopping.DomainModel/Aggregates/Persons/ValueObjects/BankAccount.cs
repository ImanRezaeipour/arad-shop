using Shopping.Infrastructure.Core.Domain.Values;

namespace Shopping.DomainModel.Aggregates.Persons.ValueObjects
{
    public class BankAccount : ValueObject<BankAccount>
    {
        protected BankAccount() { }
        public BankAccount(string iban, string accountOwnerName, string accountNumber)
        {
            Iban = iban;
            AccountNumber = accountNumber;
            AccountOwnerName = accountOwnerName;
        }
        public string Iban { get; private set; }
        public string AccountOwnerName { get; private set; }
        public string AccountNumber { get; private set; }
    }
}