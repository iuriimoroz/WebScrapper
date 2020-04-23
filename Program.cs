using System;

namespace WebScrapper
{
    class Program
    {
        static void Main()
        {
            var mainPageLinks = ScrapingMethods.GetMainPageLinks("https://habr.com");
            Console.WriteLine("Please enter a search term:");
            var searchTerm = Console.ReadLine();
            Console.WriteLine();
            var pageDetails = ScrapingMethods.GetPageDetails(mainPageLinks, searchTerm);
            int pageNumber = 1;

            Console.WriteLine($"Following was found using the \"{searchTerm}\" search term:");

            foreach (var pageDetail in pageDetails)
            {
                Console.WriteLine($"Page number {pageNumber++}:");
                Console.WriteLine($"Page title: {pageDetail.title}");
                Console.WriteLine($"Page description: {pageDetail.description}");
                Console.WriteLine($"Page URL: {pageDetail.url}");
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
