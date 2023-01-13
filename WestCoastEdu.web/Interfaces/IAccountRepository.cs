using WestCoastEdu.web.Models;

namespace WestCoastEdu.web.Interfaces;

public interface IAccountRepository : IRepository<Account>
{
    Task<Account?> FindByUserNameAsync(string userName);
    Task<Account?> FindByEmailAsync(string email);
}
