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
    public class CreateLocationPOST
    {
        public Task<HttpResponseMessage> CreateNewLocation(string name, object obj)
        {
            string link = CreateLocation.GetLink(obj);
            string json = GetJsonLink(name, obj);
            
            HttpClient httpClient = new HttpClient();
            HttpContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            return httpClient.PostAsync(link, stringContent);
        }

        private string  GetJsonLink(string name, object obj)
        {
            //I'm trying to use DRY, that's why I've created object obs
            object obs = null;
            switch (obj)
            {
                case City _:
                    obs = new City { Name = name };
                    break;
                case Region _:
                    obs = new Region { Name = name };
                    break;
                case Country _:
                    obs = new Country { Name = name };
                    break;
                default:
                    throw new Exception("Trouble in CreateCityOrRegionPOST");
            }
            return JsonConvert.SerializeObject(obs);
        }
    }
}
