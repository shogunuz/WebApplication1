using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repos
{
    public class CreateCityOrRegionPOST
    {
        public static Task<HttpResponseMessage> CreateNewLocation(string name, object obj)
        {
            string link = CreateCityOrRegion.GetLink(obj);
            string json="";
            if (obj is City city)
            {
                city = new City { Name = name };
               json = JsonConvert.SerializeObject(city);
            }
            else if(obj is Region region)
            {
                region = new Region { Name = name };
                json = JsonConvert.SerializeObject(region);
            }
            HttpClient httpClient = new HttpClient();
            HttpContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            return httpClient.PostAsync(link, stringContent);
        }
    }
}
