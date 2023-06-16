using System.Collections.Generic;

namespace BankingSystem.Library
{
    public class CustomerCRUD
    {
        private static Dictionary<string, Customer> customers = new();

        public void Add(Customer customer)
        {
            customers.Add(customer.CPF, customer);
        }

        public Customer GetCustomer(string cpf)
        {
            return customers.GetValueOrDefault(cpf);
        }

        public Dictionary<string, Customer> GetAll()
        {
            return new Dictionary<string, Customer>(customers);
        }

        public void Update(Customer customer)
        {
            Delete(customer);
            Add(customer);
        }

        public void Delete(Customer customer)
        {
            customers.Remove(customer.CPF);
        }
    }
}
