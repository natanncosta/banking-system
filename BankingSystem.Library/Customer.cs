using System;

namespace BankingSystem.Library
{
    public class Customer
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Address { get; set; }

        public Customer(string name, string cpf, string rg, string address)
        {
            Name = name;
            CPF = cpf;
            RG = rg;
            Address = address;
        }

        public Customer(string cpf)
        {
            CPF = cpf;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is Customer client &&
                   CPF == client.CPF;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, CPF, RG, Address);
        }
    }
}
