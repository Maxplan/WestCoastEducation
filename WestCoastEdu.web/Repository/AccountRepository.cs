using Microsoft.EntityFrameworkCore;
using WestCoastEdu.web.Data;
using WestCoastEdu.web.Interfaces;
using WestCoastEdu.web.Models;

namespace WestCoastEdu.web.Repository;

public class AccountRepository : Repository<Account>, IAccountRepository
{
    public AccountRepository(WestCoastEduContext context) : base(context){}

    public async Task<Account?> FindByEmailAsync(string email)
    {
        return await _context.Accounts.SingleOrDefaultAsync(c => c.Email.Trim().ToLower() == email.Trim().ToLower());
    }

    public async Task<Account?> FindByUserNameAsync(string userName)
    {
        return await _context.Accounts.SingleOrDefaultAsync(c => c.UserName.Trim().ToLower() == userName.Trim().ToLower());
    }
}
