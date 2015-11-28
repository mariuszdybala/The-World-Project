using Microsoft.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace TheWorld.Services
{
    public class CoordService
    {
        private ILogger<CoordService> _logger;

        public CoordService(ILogger<CoordService> logger)
        {
            _logger = logger;
        }

        public async Task<CoordServiceResult> Lookup(string Location)
        {
            var result = new CoordServiceResult()
            {
                Success = false,
                Message = "Undetermined failture while looking up coordinates"
            };
            //look up coordinates
            string encodeName = WebUtility.UrlDecode(Location);

            //czytam bing Key z zmiennych środowiskowych żeby nie udosępniać w repozytorium :p

            string bingMapsKey = Startup.Configuration["AppSettings:BingKey"];
            var url = $"http://dev.virtualearth.net/REST/v1/Locations?q={encodeName}&key={bingMapsKey}";

            var client = new HttpClient();

            var json = await client.GetStringAsync(url);

            var results = JObject.Parse(json);
            var resources = results["resourceSets"][0]["resources"];

            var coord = resources[0]["geocodePoints"][0]["coordinates"];

            result.Long = (double)coord[1];
            result.Lat = (double)coord[0];
            result.Success = true;
            result.Message = "Success";
            return result;
        }
    }


}
