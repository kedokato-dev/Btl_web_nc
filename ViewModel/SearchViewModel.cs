using System.Collections.Generic;

namespace Btl_web_nc.Models.ViewModels
{
    public class SearchViewModel
    {
        public string Query { get; set; }
        public List<Article> Articles { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}