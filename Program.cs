using System;

namespace WebScrapper
{
    class Program
    {
        static void Main()
        {
            var mainPageLinks = ScrapingMethods.GetMainPageLinks("https://medium.com/");
            Console.WriteLine("Please enter a search term:");
            var searchTerm = Console.ReadLine();
            Console.WriteLine();
            var pageDetails = ScrapingMethods.GetPageDetails(mainPageLinks, searchTerm);
            int pageNumber = 1;

            Console.WriteLine($"Following was found using the \"{searchTerm}\" search term:");

            foreach (var pageDetail in pageDetails)
            {
                Console.WriteLine($"Page number {pageNumber++}:");
                Console.WriteLine($"Page title: {pageDetail.Title}");
                Console.WriteLine($"Page description: {pageDetail.Description}");
                Console.WriteLine($"Page URL: {pageDetail.Url}");
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
