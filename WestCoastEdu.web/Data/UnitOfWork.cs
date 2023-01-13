using WestCoastEdu.web.Interfaces;
using WestCoastEdu.web.Repository;

namespace WestCoastEdu.web.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly WestCoastEduContext _context;
    public UnitOfWork(WestCoastEduContext context)
    {
            _context = context;
        
    }
    public ICourseRepository CourseRepository => new CourseRepository(_context);

    public IAccountRepository AccountRepository => new AccountRepository(_context);

    public async Task<bool> Complete()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
