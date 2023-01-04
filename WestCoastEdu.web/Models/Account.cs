namespace WestCoastEdu.web.Models;
public class Account
{
        public int AccountId { get; set; }
        public string? FirstName { get; set;}
        public string? LastName { get; set;}
        public string? FullName => $"{FirstName} {LastName}";
        public string? AccountRole { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
}
