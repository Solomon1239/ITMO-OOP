using Banks.Observer;
using Banks.Tools;

namespace Banks.Entities.Clients
{
    public class Client : IObserver
    {
        public Client(string name, string surname, int passportId = 0, string? address = null)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ClientException("Name is empty");
            if (string.IsNullOrWhiteSpace(surname)) throw new ClientException("Surname is empty");
            if (passportId < 0) throw new ClientException("Incorrect passport id");

            Name = name;
            Surname = surname;
            PassportId = passportId;
            Address = address;
        }

        public int PassportId { get; private set; }
        public string? Address { get; private set; }
        public string Name { get; }
        public string Surname { get; }
        public string? Notification { get; private set; } = null;
        public bool Subscribed { get; private set; } = false;
        public bool IsDoubtfulClient() => PassportId == 0 || Address == null;

        public void SetPassportId(int passportId)
        {
            if (passportId <= 0) throw new ClientException("Incorrect passport id");

            PassportId = passportId;
        }

        public void SetAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address)) throw new ClientException("Incorrect address");

            Address = address;
        }

        public void ChangeSubscription()
        {
            Subscribed = !Subscribed;
        }

        public void Update(string message)
        {
            Notification = message;
        }
    }
}