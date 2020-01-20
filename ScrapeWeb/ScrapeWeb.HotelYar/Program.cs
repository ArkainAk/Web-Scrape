using ScrapeWeb.HotelYar;
using System;
using System.Threading.Tasks;

namespace ScrapeWeb
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://hotelyar.com/hotel/84/%D9%87%D8%AA%D9%84-%D8%A7%D8%B3%D8%AA%D9%82%D9%84%D8%A7%D9%84-%D8%AA%D9%87%D8%B1%D8%A7%D9%86";
            UrlHelper helper = new UrlHelper();
            await helper.FetchUrl(url);
            Console.ReadLine();
        }
    }
}
