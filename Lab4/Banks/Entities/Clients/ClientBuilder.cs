#nullable disable
using Banks.Tools;

namespace Banks.Entities.Clients
{
    public class ClientBuilder
    {
        private string _name;
        private string _surname;
        private string _address;
        private int _passportId;

        public Client Build()
        {
            if (_name is null) throw new ClientException("Client name does not exist");
            if (_surname is null) throw new ClientException("Client surname does not exist");

            return new Client(_name, _surname, _passportId, _address);
        }

        public ClientBuilder SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ClientException("Name is empty");
            _name = name;

            return this;
        }

        public ClientBuilder SetSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname)) throw new ClientException("Surname is empty");
            _surname = surname;

            return this;
        }

        public ClientBuilder SetAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address)) throw new ClientException("Address is empty");
            _address = address;

            return this;
        }

        public ClientBuilder SetPassportId(int passportId)
        {
            if (passportId < 0) throw new ClientException("Incorrect passport id");
            _passportId = passportId;

            return this;
        }
    }
}