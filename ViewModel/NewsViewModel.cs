namespace Btl_web_nc.Models
{
    public class NewsViewModel
    {
        public Newsletter Newsletter { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}