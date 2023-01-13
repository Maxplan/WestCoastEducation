using Microsoft.AspNetCore.Mvc;
using WestCoastEdu.web.Interfaces;
using WestCoastEdu.web.Models;
using WestCoastEdu.web.ViewModels;

namespace WestCoastEdu.web.Controllers;

[Route("accounts/admin")]
public class AccountsAdminController : Controller
{
    private readonly IAccountRepository _repo;
    public IRepository<Account> _genericRepo { get; }

    public AccountsAdminController(IRepository<Account> genericRepo, IAccountRepository repo)
    {
        _genericRepo = genericRepo;
        _repo = repo;    
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var accounts = await _genericRepo.ListAllAsync();

            var model = accounts.Select(a => new AccountListViewModel{
                AccountId = a.AccountId,
                FullName = a.FullName,
                Email = a.Email,
                Phone = a.Phone,
                AccountType = a.AccountType
            }).ToList();

            return View("Index", model);
        }
        catch (Exception ex)
        {
            
            var error = new ErrorModel{
                ErrorMessage = ex.Message
            };
            return View("_Error", error);
        }
    }

    [HttpGet("create")]
    public IActionResult Create(){
        var account = new AccountPostViewModel();
        return View("Create", account);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(AccountPostViewModel account){
        try
        {
            if (!ModelState.IsValid) return View("Create", account);

            var emailExists = await _repo.FindByEmailAsync(account.Email);

            if (emailExists is not null)
            {
                var createError = new ErrorModel
                {
                    ErrorTitle = "Something Went Wrong When Trying To Create Account",
                    ErrorMessage = $"There's already a course with title ''{account.Email}''"
                };
                return View("_Error", createError);
            }
            
            var accountToAdd = new Account
            {
                FullName = account.FullName,
                Email = account.Email,
                Phone = account.Phone,
                AccountType = account.AccountType
            };

            if(await _genericRepo.AddAsync(accountToAdd)){
                if(await _genericRepo.SaveAsync(accountToAdd)){
                    return RedirectToAction(nameof(Index));
                }
            }

            var error = new ErrorModel
            {
                ErrorTitle = "Something went wrong when trying to create a new account",
                ErrorMessage = "IS WONG"
            };
            return View("_Error", error);
        }
        catch (Exception ex)
        {
            var error = new ErrorModel
                {
                    ErrorTitle = "Something went wrong when trying to create a new account",
                    ErrorMessage = ex.Message
                };
                return View("_Error", error);
        }
  
    }

    [HttpGet("edit/{accountId}")]
    public async Task<IActionResult> Edit(int accountId)
    {
        try
        {    
            var result = await _genericRepo.FindByIdAsync(accountId);

            if(result is null){
                var error = new ErrorModel
                {
                    ErrorTitle = "Something went wrong when trying to fetch account",
                    ErrorMessage = $"There's not an account with ID: {accountId}"
                }; 
                return View("_Error", error);
            }

            var model = new AccountUpdateViewModel{
                AccountId = result.AccountId,
                FullName = result.FullName,
                Email = result.Email,
                Phone = result.Phone,
                AccountType = result.AccountType
            };

            return View("Edit", model);
        }
        catch (Exception ex)
        {
            var error = new ErrorModel{
                ErrorTitle = "Something went wrong when trying to fetch the account",
                ErrorMessage = ex.Message
            };
            return View("_Error", error);
        }
    }

    [HttpPost("edit/{accountId}")]
    public async Task<IActionResult> Edit(int accountId, AccountUpdateViewModel model)
    {
        try
        {
            if(!ModelState.IsValid) return View("Edit", model);

            var accountToUpdate = await _genericRepo.FindByIdAsync(accountId);

            if(accountToUpdate is null) return RedirectToAction(nameof(Index));

            accountToUpdate.AccountId = model.AccountId;
            accountToUpdate.FullName = model.FullName;
            accountToUpdate.Email = model.Email;
            accountToUpdate.Phone = model.Phone;
            accountToUpdate.AccountType = model.AccountType;

            if(await _genericRepo.UpdateAsync(accountToUpdate)){
                if(await _genericRepo.SaveAsync(accountToUpdate)){
                    return RedirectToAction(nameof(Index));
                }
            };
            var updateError = new ErrorModel{
                ErrorTitle = "Something went wrong",
                ErrorMessage = "Something went wrong when trying to update information"
            };
            return View("_Error", updateError);
        }
        catch (Exception ex)
        {
            var error = new ErrorModel{
                ErrorTitle = "something went wrong when trying to save your changes",
                ErrorMessage = ex.Message
            };
            return View("_Error", error);
        }
    }

    [Route("delete/{accountId}")]
    public async Task<IActionResult> Delete(int accountId)
    {
        try
        {
            var accountToDelete = await _genericRepo.FindByIdAsync(accountId);

            if(accountToDelete is null) return RedirectToAction(nameof(Index));

            if(await _genericRepo.DeleteAsync(accountToDelete)){
                if(await _genericRepo.SaveAsync(accountToDelete)){
                    return RedirectToAction(nameof(Index));
                }
            }

            var deleteError = new ErrorModel{
                ErrorTitle = "Something went wrong",
                ErrorMessage = "Something went wrong when trying to delete account"
            };

            return View("_Error", deleteError);
        }
        catch (Exception ex)
        {
            var error = new ErrorModel{
                ErrorTitle = "Something went wrong when trying to delete account",
                ErrorMessage = ex.Message
            };
            return View("_Error", error);
        }
    }
}
