using System.Collections.Generic;

namespace BankingSystem.Library
{
    public class AccountCRUD
    {
        private static Dictionary<int, Account> accounts = new();

        public void Add(Account account)
        {
            accounts.Add(account.Number, account);
        }

        public Account GetAccount(int accountNumber)
        {
            return accounts.GetValueOrDefault(accountNumber);
        }

        public Dictionary<int, Account> GetAll()
        {
            return new Dictionary<int, Account>(accounts);
        }

        public void Update(Account account)
        {
            Delete(account);
            Add(account);
        }

        public void Delete(Account account)
        {
            accounts.Remove(account.Number);
        }
    }
}
