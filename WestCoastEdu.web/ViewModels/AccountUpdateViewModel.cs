using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WestCoastEdu.web.ViewModels;

public class AccountUpdateViewModel
{
    [Required(ErrorMessage = "An Account Id is Required")]
    [DisplayName("Account Id")]
    public int AccountId { get; set; }

    [Required(ErrorMessage = "A Full Name Is Required")]
    [DisplayName("Full Name")]
    public string FullName { get; set; } = "";

    [Required(ErrorMessage = "An Email Address Is Required")]
    [DisplayName("Email Address")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "A Phone number Is Required")]
    [DisplayName("Phone Number")]
    public string Phone { get; set; } = "";

    [Required(ErrorMessage = "An Account Type Is Required")]
    [DisplayName("Account Type")]    
    public string AccountType { get; set; } = "";
}
