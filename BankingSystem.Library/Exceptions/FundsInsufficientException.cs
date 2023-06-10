using System;

namespace BankingSystem.Library.Exceptions
{
    public class FundsInsufficientException : Exception
    {
        public FundsInsufficientException() : base("Saldo insuficiente!!")
        {
        }
    }
}
