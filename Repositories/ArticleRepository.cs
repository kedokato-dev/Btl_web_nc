using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Btl_web_nc.Data;
using Btl_web_nc.Models;
using Microsoft.EntityFrameworkCore;

namespace Btl_web_nc.Repositories
{
    public class ArticleRepository: IArticleRepository
    {
        private readonly AppDbContext _context;

        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Newsletter> GetNewsletterByIdAsync(int newsletterId)
        {
            return await _context.Newsletters
                .FirstOrDefaultAsync(n => n.Id == newsletterId);
        }

        // public async Task<List<Article>> GetArticlesByNewsletterIdAsync(int newsletterId)
        // {
        //     return await _context.Articles
        //         .Where(a => a.NewsletterId == newsletterId)
        //         .ToListAsync();
        // }

        public async Task<Article> GetArticleByLinkAsync(string link)
        {
            return await _context.Articles
                .FirstOrDefaultAsync(a => a.Link == link);
        }

        public async Task AddArticleAsync(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
        }

        // public async Task<List<Article>> GetAllArticlesAsync()
        // {
        //     return await _context.Articles.ToListAsync();
        // }

        public async Task<List<Article>> GetAllArticlesByNewsletterIdAsync(int newsletterId)
        {
            return await _context.Articles
                .Where(a => a.NewsletterId == newsletterId)
                .ToListAsync();
        }


        public IEnumerable<Article> SearchByTitle(string title)
        {
            return _context.Articles
                .Where(a => a.Title.Contains(title))
                .ToList();
        }
    }
}