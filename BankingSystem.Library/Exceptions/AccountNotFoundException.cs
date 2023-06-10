using System;

namespace BankingSystem.Library.Exceptions
{
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException() : base("Conta não encontrada")
        {
        }
    }
}
