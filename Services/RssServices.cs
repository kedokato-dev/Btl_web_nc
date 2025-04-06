using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using Btl_web_nc.Data;
using Btl_web_nc.Models;
using Microsoft.EntityFrameworkCore;

namespace Btl_web_nc.Services
{
    public class RssService
    {
        public async Task<List<Article>> GetAndSaveFeedItemsAsync(string rssUrl, int newsletterId, AppDbContext dbContext)
        {
            var articles = new List<Article>();

            try
            {
                using (var reader = XmlReader.Create(rssUrl))
                {
                    var feed = SyndicationFeed.Load(reader);

                    foreach (var item in feed.Items)
                    {
                        // Kiểm tra xem bài viết đã tồn tại chưa (dựa vào link)
                        var existingArticle = await dbContext.Articles
                            .FirstOrDefaultAsync(a => a.Link == item.Links[0].Uri.ToString());

                        if (existingArticle == null)
                        {
                            // Lấy URL hình ảnh từ thẻ <enclosure>
                            var imageUrl = item.ElementExtensions
                                .ReadElementExtensions<SyndicationLink>("enclosure", "")
                                ?.FirstOrDefault()?.Uri.ToString();

                            // Nếu không có thẻ <enclosure>, thử lấy từ <description>
                            if (string.IsNullOrEmpty(imageUrl) && item.Summary != null)
                            {
                                var description = item.Summary.Text;
                                var imgTagStart = description.IndexOf("<img src=\"");
                                if (imgTagStart >= 0)
                                {
                                    var imgTagEnd = description.IndexOf("\"", imgTagStart + 10);
                                    if (imgTagEnd > imgTagStart)
                                    {
                                        imageUrl = description.Substring(imgTagStart + 10, imgTagEnd - imgTagStart - 10);
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

                            dbContext.Articles.Add(article);
                            articles.Add(article);
                        }
                        else
                        {
                            articles.Add(existingArticle);
                        }
                    }

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching and saving RSS feed: {ex.Message}");
            }

            return articles;
        }
    }
}