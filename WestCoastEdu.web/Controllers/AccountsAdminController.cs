using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WestCoastEdu.web.Data;
using WestCoastEdu.web.Models;

namespace WestCoastEdu.web.Controllers;

[Route("accounts/admin")]
public class AccountsAdminController : Controller
{
    private readonly WestCoastEduContext _context;

    public AccountsAdminController(WestCoastEduContext context)
    {
        _context = context;     
    }

    public async Task<IActionResult> Index()
    {
        var accounts = await _context.Accounts.ToListAsync();
        return View("Index", accounts);
    }

    [HttpGet("create")]
    public IActionResult Create(){
        var account = new Account();
        return View("Create", account);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(Account account){
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("edit/{accountId}")]
    public async Task<IActionResult> Edit(int accountId)
    {
        var account = await _context.Accounts.SingleOrDefaultAsync(c => c.AccountId == accountId);

        if(account is not null) return View("Edit", account);

        return Content("Error...");
    }

    [HttpPost("edit/{accountId}")]
    public async Task<IActionResult> Edit(int accountId, Account account)
    {
        var accountToUpdate = _context.Accounts.SingleOrDefault(c => c.AccountId == accountId);

        if(accountToUpdate is null) return RedirectToAction(nameof(Index));

        accountToUpdate.FullName = account.FullName;
        accountToUpdate.Email = account.Email;
        accountToUpdate.Phone = account.Phone;
        accountToUpdate.AccountType = account.AccountType;

        _context.Accounts.Update(accountToUpdate);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [Route("delete/{accountId}")]
    public async Task<IActionResult> Delete(int accountId)
    {
        var accountToDelete = await _context.Accounts.SingleOrDefaultAsync(c => c.AccountId == accountId);

        if(accountToDelete is null) return RedirectToAction(nameof(Index));

        _context.Accounts.Remove(accountToDelete);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
