using Microsoft.EntityFrameworkCore;
using WestCoastEdu.web.Data;
using WestCoastEdu.web.Interfaces;
using WestCoastEdu.web.Models;

namespace WestCoastEdu.web.Repository;

public class AccountRepository : IAccountRepository
{
    private readonly WestCoastEduContext _context;
    public AccountRepository(WestCoastEduContext context){
        _context = context;
    }
    public async Task<bool> AddAsync(Account account)
    {
        try
        {
            await _context.Accounts.AddAsync(account);
            return true;
        }
        catch (System.Exception)
        {
            
            return false;
        }
    }

    public Task<bool> DeleteAsync(Account account)
    {
        try
        {
            _context.Accounts.Remove(account);
            return Task.FromResult(true);
        }
        catch (System.Exception)
        {
            return Task.FromResult(false);
        }
    }

    public async Task<Account?> FindByIdAsync(int id)
    {
        return await _context.Accounts.FindAsync(id);
    }

    public async Task<Account?> FindByEmailAsync(string email)
    {
        return await _context.Accounts.SingleOrDefaultAsync(c => c.Email.Trim().ToLower() == email.Trim().ToLower());
    }

    public async Task<Account?> FindByUserNameAsync(string userName)
    {
        return await _context.Accounts.SingleOrDefaultAsync(c => c.UserName.Trim().ToLower() == userName.Trim().ToLower());
    }

    public async Task<IList<Account>> ListAllAsync()
    {
        return await _context.Accounts.ToListAsync();
    }

    public async Task<bool> SaveAsync()
    {
        try
        {
            if (await _context.SaveChangesAsync() > 0) return true;
            return false; 
        }
        catch
        {
            return false;
        }

    }

    public Task<bool> UpdateAsync(Account account)
    {
        try
        {
            _context.Accounts.Update(account);
            return Task.FromResult(true);
        }
        catch
        {  
            return Task.FromResult(false);
        }
    }
}
