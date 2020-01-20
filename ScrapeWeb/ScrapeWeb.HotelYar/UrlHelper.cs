using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScrapeWeb.HotelYar
{
    public class UrlHelper
    {
        public string RegexMatch(string content, string regex)
        {
            var result = Regex.Match(content, regex, RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[1].Value;
            return result;
        }

        public MatchCollection RegexMatches(string content, string regex)
        {
            var result = Regex.Matches(content, regex, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            return result;
        }

        public async Task FetchUrl(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var html = await client.GetStringAsync(url);

                    string tbody = RegexMatch(html, "<table class=\"ss-single-reserve-table\">.*<tbody>(.*)</tbody>.*</table>");
                    string trTag = "<tr>.*?</tr>";

                    var matches = RegexMatches(tbody, trTag);

                    foreach (Match match in matches)
                    {
                        var roomTypePattern = RegexMatch(match.Value, "<span class=\"ss-single-reserve-table-t-name\">(.*?)</span>");
                        var discountPattern = RegexMatch(match.Value, "<span class=\"ss-single-reserve-table-t-off\">(.*?)</span>");
                        var roomCapacityPattern = RegexMatch(match.Value, "<td data-title=\"ظرفیت.*?\">.*?<span>(.*?)</span>.*?</td>");
                        var extraServicePattern = RegexMatch(match.Value, "<td data-title=\"امکان سرویس اضافه.*?\"><span>(.*?)</span></td>");
                        var boardPricePattern = RegexMatch(match.Value, "<span class=\"ss-single-reserve-table-p-old\">(.*?)</span>.*</td>");
                        var finalPricePattern = RegexMatch(match.Value, "<span class=\"ss-single-reserve-table-p-new\">\\s+(.*?)\\s+</span>.*</td>");
                        var roomPointsPattern = RegexMatch(match.Value, @"<span>(\d{2})</span>");
                    }
                }
            }
            catch
            {
                Console.WriteLine("There is a problem!!!");
                Console.ReadLine();
            }

        }
    }
}
