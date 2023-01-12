using WestCoastEdu.web.Models;

namespace WestCoastEdu.web.Interfaces;

public interface IAccountRepository
{
    Task<IList<Account>> ListAllAsync();
    Task<Account?> FindByIdAsync(int id);
    Task<Account?> FindByUserNameAsync(string userName);
    Task<Account?> FindByEmailAsync(string email);
    Task<bool> AddAsync(Account account);
    Task<bool> UpdateAsync(Account account);
    Task<bool> DeleteAsync(Account account);
    Task<bool> SaveAsync();
}
