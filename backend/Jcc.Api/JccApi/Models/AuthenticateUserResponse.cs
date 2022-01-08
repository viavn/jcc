using System;

namespace JccApi.Models
{
    public class AuthenticateUserResponse
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public Enums.UserType UserType { get; set; }
    }
}
