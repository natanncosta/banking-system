using System;

namespace BankingSystem.Library
{
    public class BankingException : Exception
    {
        public BankingException(string message) : base(message)
        {
        }
    }
}
