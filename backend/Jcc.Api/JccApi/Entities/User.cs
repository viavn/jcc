using System;

namespace JccApi.Entities
{
    public class User
    {
        public User() { }

        public User(Guid id, string login, string name, string password)
        {
            Id = id;
            Login = login;
            Name = name;
            Password = password;
        }

        public User(string login, string name, string password)
            : this(Guid.NewGuid(), login, name, password) { }

        public Guid Id { get; private set; }
        public string Login { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }
    }
}
