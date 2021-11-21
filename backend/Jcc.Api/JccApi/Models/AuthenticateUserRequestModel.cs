namespace JccApi.Models
{
    public class AuthenticateUserRequestModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class CreateUserRequestModel
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
