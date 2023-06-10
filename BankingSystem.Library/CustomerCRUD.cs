using System.Collections.Generic;
using BankingSystem.Library.Exceptions;

namespace BankingSystem.Library
{
    public class CustomerCRUD
    {
        private static HashSet<Customer> customers = new();

        public void Add(Customer customer)
        {
            customers.Add(customer);
        }

        public Customer GetCustomer(string cpf)
        {
            foreach (var customer in customers)
                if (customer.CPF == cpf)
                    return customer;
            throw new CustomerNotFoundException();
        }

        public HashSet<Customer> GetAll()
        {
            return new HashSet<Customer>(customers);
        }

        public void Update(Customer oldCustomer, Customer newCustomer)
        {
            customers.Remove(oldCustomer);
            customers.Add(newCustomer);
        }

        public void Delete(Customer customer)
        {
            customers.Remove(customer);
        }
    }
}
