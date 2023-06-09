using System.Collections.Generic;

namespace BankingSystem.Library
{
    public class AccountCRUD
    {
        private static HashSet<Account> accounts = new();

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

        public HashSet<Account> GetAll()
        {
            return new HashSet<Account>(accounts);
        }

        public void Update(Account account)
        {
            accounts.Remove(account);
            accounts.Add(account);
        }

        public void Delete(Account account)
        {
            accounts.Remove(account);
        }
    }
}
