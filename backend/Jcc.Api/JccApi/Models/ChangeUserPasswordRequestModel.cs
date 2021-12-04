using System.ComponentModel.DataAnnotations;

namespace JccApi.Models
{
    public class ChangeUserPasswordRequestModel
    {
        [Required]
        public string NewPassword { get; set; }
    }
}
