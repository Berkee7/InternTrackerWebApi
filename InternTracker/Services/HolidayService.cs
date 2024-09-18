using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices.JavaScript;

namespace InternTracker.Services
{
    public class HolidayService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "136df5cd-e1fd-4a8b-9db0-a2bdc9d21a2e";

        public HolidayService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<DateTime>> GetPublicHolidaysAsync()
        { 
            var year = 2023;
            string url = $"https://holidayapi.com/v1/holidays?country=TR&year={year}&key={ApiKey}&pretty=true";
            HttpResponseMessage response = await _httpClient.GetAsync(url);//Api çağırdım.
            response.EnsureSuccessStatusCode();

            var holidayJson=await response.Content.ReadAsStringAsync();
            Console.WriteLine(holidayJson);
            JObject holidaysObj = JObject.Parse(holidayJson);
            List<DateTime> holidayDates = holidaysObj["holidays"].Select(h => DateTime.Parse(h["date"].ToString())).ToList();
            return holidayDates;
        }


    }
}
