namespace WestCoastEdu.web.Interfaces;

public interface IUnitOfWork
{
    ICourseRepository CourseRepository { get; }
    IAccountRepository AccountRepository { get; }
    Task<bool> Complete();
}
