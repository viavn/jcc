using JccApi.Enums;

namespace JccApi.Models
{
    public class CreateUserRequestModel
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }
}
