using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Btl_web_nc.Services
{
    public class RssFeedService
    {
        private const string RssUrl = "https://vnexpress.net/rss/phap-luat.rss";

        public List<string> GetRssFeed()
        {
            var newsList = new List<string>();

            using (var reader = XmlReader.Create(RssUrl))
            {
                var feed = SyndicationFeed.Load(reader);
                if (feed != null)
                {
                    foreach (var item in feed.Items)
                    {
                        var title = item.Title.Text;
                        var link = item.Links[0].Uri.ToString();
                        newsList.Add($"{title} - {link}");
                    }
                }
            }

            return newsList;
        }
    }
}