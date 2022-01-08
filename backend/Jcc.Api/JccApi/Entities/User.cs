using System;

namespace JccApi.Entities
{
    public class User
    {
        public User() { }

        public User(Guid id, string login, string name, string password, Enums.UserType userType)
        {
            Id = id;
            Login = login;
            Name = name;
            Password = password;
            UserType = userType;
            IsDeleted = false;
        }

        public User(string login, string name, string password, Enums.UserType userType)
            : this(Guid.NewGuid(), login, name, password, userType) { }

        public Guid Id { get; private set; }
        public string Login { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }
        public Enums.UserType UserType { get; private set; }
        public bool IsDeleted { get; private set; }

        public void ChangePassword(string newPassword)
        {
            Password = newPassword;
        }

        public void MarkAsDeleted()
        {
            IsDeleted = true;
        }
    }
}
