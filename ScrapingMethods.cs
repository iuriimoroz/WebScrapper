using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;

namespace WebScrapper
{   /// <summary>
    /// ScrapingMethods class contains methods for web pages scraping
    /// </summary>
    public class ScrapingMethods
    {
        private readonly static ScrapingBrowser _browser = new ScrapingBrowser();

        private static HtmlNode GetHtml(string url)
        {
            return _browser.NavigateToPage(new Uri(url)).Html;
        }

        public static List<string> GetMainPageLinks(string url)
        {
            var homePageLinks = new List<string>();
            var html = GetHtml(url);
            var links = html.CssSelect("a");

            foreach (var link in links)
            {
                if (link.Attributes["href"].Value.Contains("https://"))
                {
                    homePageLinks.Add(link.Attributes["href"].Value);
                }
            }

            return homePageLinks;
        }
        public static List<PageDetails> GetPageDetails(List<string> urls, string searchTerm)
        {
            var pageDetailsList = new List<PageDetails>();

            foreach (var url in urls)
            {
                var htmlNode = GetHtml(url);
                var pageDetails = new PageDetails();
                string description;

                try
                {
                    description = htmlNode.OwnerDocument.DocumentNode.SelectNodes("//meta[@name='description']")[0].Attributes["content"].Value;
                }
                catch
                {
                    description = "Unable to parse description for this page.";
                }

                pageDetails.Title = htmlNode.OwnerDocument.DocumentNode.SelectNodes("//html/head/title")[0].InnerText;
                pageDetails.Description = description;
                pageDetails.Url = url;

                var searchTermInTitle = pageDetails.Title.ToLower().Contains(searchTerm.ToLower());
                var searchTermInDescription = pageDetails.Description.ToLower().Contains(searchTerm.ToLower());

                if (searchTermInTitle || searchTermInDescription)
                {
                    pageDetailsList.Add(pageDetails);
                }
            }

            return pageDetailsList;
        }
    }
}
