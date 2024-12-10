using System.ComponentModel.DataAnnotations;

namespace ShopTARge23.Models.Accounts
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; } 

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; } 

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } 
    }
}
