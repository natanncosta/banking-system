using System;

namespace BankingSystem.Library.Exceptions
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException() : base("Cliente não encontrado.")
        {
        }
    }
}
