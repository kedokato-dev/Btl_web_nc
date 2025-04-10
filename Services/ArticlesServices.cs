using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Btl_web_nc.Models;
using Btl_web_nc.Repositories;

namespace Btl_web_nc.Services
{
    public class ArticlesServices
    {
        private readonly IArticleRepository _articleRepository;

        public ArticlesServices(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<List<Article>> GetArticlesAsyncByTitle(string title)
        {
            return await Task.Run(() => _articleRepository.SearchByTitle(title).ToList());  
        }


    }
}