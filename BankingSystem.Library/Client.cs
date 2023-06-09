using System;

namespace BankingSystem.Library
{
    public class Client
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Address { get; set; }

        public Client(string name, string cpf, string rg, string address)
        {
            Name = name;
            CPF = cpf;
            RG = rg;
            Address = address;
        }

        public Client(string cpf)
        {
            CPF = cpf;
        }

        public override bool Equals(object obj)
        {
            return obj is Client client &&
                   CPF == client.CPF;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, CPF, RG, Address);
        }
    }
}
