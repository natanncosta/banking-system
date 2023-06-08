using System.Collections.Generic;

namespace BankingSystem.Library
{
    public class ClientCRUD
    {
        private static HashSet<Client> clients = new();

        public void Add(Client client)
        {
            clients.Add(client);
        }

        public Client GetClient(string cpf)
        {
            foreach (var client in clients)
                if (client.CPF == cpf)
                    return client;
            return null;
        }

        public List<Client> GetAll()
        {
            return new List<Client>(clients);
        }

        public void Update(Client client)
        {
            clients.Remove(client);
            clients.Add(client);
        }

        public void Delete(Client client)
        {
            clients.Remove(client);
        }
    }
}
