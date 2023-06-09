using System.Collections.Generic;

namespace BankingSystem.Library
{
    public class AccountCRUD
    {
        private static List<Account> accounts = new();

        public void Add(Account account)
        {
            accounts.Add(account);
        }

        public Account GetAccount(int number)
        {
            foreach (var account in accounts)
                if (account.Number == number)
                    return account;
            return null;
        }

        public List<Account> GetAll()
        {
            return new List<Account>(accounts);
        }

        public void Update(Account oldAccount, Account updatedAccount)
        {
            accounts.Remove(oldAccount);
            accounts.Add(updatedAccount);
        }

        public void Delete(Account account)
        {
            accounts.Remove(account);
        }
    }
}
