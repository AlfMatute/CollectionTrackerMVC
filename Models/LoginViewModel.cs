using System.ComponentModel.DataAnnotations;

namespace CollectionTrackerMVC.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }

        public CollectionUser CollectionUser { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool Register { get; set; }
    }
}