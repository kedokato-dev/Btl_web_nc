using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using Btl_web_nc.Models;
using Btl_web_nc.Repositories;

namespace Btl_web_nc.Services
{
    public class RssService
    {
        private readonly ArticleRepository _articleRepository;

        public RssService(ArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        // Lưu tin tức từ RSS vào cơ sở dữ liệu
        public async Task SaveFeedItemsFromRssAsync(string rssUrl, int newsletterId)
        {
            try
            {
                using (var reader = XmlReader.Create(rssUrl))
                {
                    var feed = SyndicationFeed.Load(reader);

                    foreach (var item in feed.Items)
                    {
                        var existingArticle = await _articleRepository.GetArticleByLinkAsync(item.Links[0].Uri.ToString());

                        if (existingArticle == null)
                        {
                            // Lấy ảnh từ thẻ <enclosure> hoặc <description>
                            string? imageUrl = null;

                            // Kiểm tra thẻ <enclosure>
                            var enclosure = item.ElementExtensions
                                .FirstOrDefault(ext => ext.OuterName == "enclosure");
                            if (enclosure != null)
                            {
                                var xmlElement = enclosure.GetObject<XmlElement>();
                                imageUrl = xmlElement?.GetAttribute("url");
                            }

                            // Nếu không có <enclosure>, kiểm tra trong <description>
                            if (string.IsNullOrEmpty(imageUrl) && item.Summary != null)
                            {
                                var description = item.Summary.Text;
                                var imgTagStart = description.IndexOf("<img");
                                if (imgTagStart >= 0)
                                {
                                    var srcStart = description.IndexOf("src=\"", imgTagStart) + 5;
                                    var srcEnd = description.IndexOf("\"", srcStart);
                                    if (srcStart > 0 && srcEnd > srcStart)
                                    {
                                        imageUrl = description.Substring(srcStart, srcEnd - srcStart);
                                    }
                                }
                            }

                            var article = new Article
                            {
                                NewsletterId = newsletterId,
                                Title = item.Title.Text,
                                Link = item.Links[0].Uri.ToString(),
                                PublishedAt = item.PublishDate.DateTime,
                                Summary = item.Summary?.Text,
                                ImageUrl = imageUrl,
                                CreatedAt = DateTime.Now
                            };

                            await _articleRepository.AddArticleAsync(article);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching and saving RSS feed: {ex.Message}");
            }
        }

        // Lấy tất cả tin tức theo newsletterId với phân trang
        public async Task<(List<Article> Articles, int TotalPages)> GetPagedArticlesByNewsletterIdAsync(int newsletterId, int page, int pageSize = 20)
        {
            var allArticles = await _articleRepository.GetAllArticlesByNewsletterIdAsync(newsletterId);

            // Sắp xếp các bài viết theo ngày xuất bản giảm dần
            allArticles = allArticles.OrderByDescending(a => a.PublishedAt).ToList();

            var totalArticles = allArticles.Count;
            var totalPages = (int)Math.Ceiling(totalArticles / (double)pageSize);

            var pagedArticles = allArticles
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (pagedArticles, totalPages);
        }
    }
}