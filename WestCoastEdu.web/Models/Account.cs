using System.ComponentModel;

namespace WestCoastEdu.web.Models;

public class Account
{
    public int AccountId { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public int AccountType { get; set; }
    public string UserName { get; set; } = "";
    public string PassWord { get; set; } = "";
}