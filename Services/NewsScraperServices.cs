using HtmlAgilityPack;
using System.Collections.Generic;

namespace Btl_web_nc.Services
{
    public class NewsScraperService
    {
        private const string VnExpressUrl = "https://vnexpress.net/";

        public List<string> GetLatestNews()
        {
            var newsList = new List<string>();
            var web = new HtmlWeb();
            var doc = web.Load(VnExpressUrl);

            // Lấy các tiêu đề tin tức từ trang chủ
            var nodes = doc.DocumentNode.SelectNodes("//h3[@class='title-news']/a");
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var title = node.InnerText.Trim();
                    var link = node.GetAttributeValue("href", string.Empty);
                    newsList.Add($"{title} - {link}");
                }
            }

            return newsList;
        }
    }
}