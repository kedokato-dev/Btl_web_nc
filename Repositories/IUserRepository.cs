using Btl_web_nc.Models;

public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersByNewsletterIdAsync(int newsletterId);
    }