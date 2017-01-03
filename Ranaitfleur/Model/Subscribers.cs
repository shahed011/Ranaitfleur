using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ranaitfleur.Model
{
    public class Subscribers
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [DisplayName("Email Address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
