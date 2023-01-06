namespace WestCoastEdu.web.Models;

public class Account
{
    public int AccountId { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string AccountType { get; set; } = "";
}