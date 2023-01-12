using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WestCoastEdu.web.ViewModels;

public class AccountPostViewModel
{
    [Required(ErrorMessage = "A Full Name Is Required")]
    [DisplayName("Full Name")]
    public string FullName { get; set; } = "";

    [Required(ErrorMessage = "An Email Address Is Required")]
    [DisplayName("Email Address")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "A Phone number Is Required")]
    [DisplayName("Phone Number")]
    public string Phone { get; set; } = "";

    [Required(ErrorMessage = "A Account Role Is Required")]
    [DisplayName("Account Role")]    
    public string AccountType { get; set; } = "";
}
