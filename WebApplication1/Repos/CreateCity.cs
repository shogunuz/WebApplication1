using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repos
{

    public class CreateCity
    {
        private readonly CountryContext _context;
        public CreateCity(CountryContext context)
        {
            _context = context;
        }
       
        public int CityCreation(string Name)
        {
            int id = GetCityId(Name);
            if (id >= 0)
                return id;
            else
            {
                CreateCityInDB(Name).Wait();
                return GetCityId(Name);
            }
        }
        private int GetCityId(string name)
        {
            int getCityId()
            {
                var city = _context.Cities
                    .FirstOrDefault(n => n.Name == name);

                if (city == null)
                    return -1;

                return city.Id;
            }

            return getCityId();
        }
        private static Task<HttpResponseMessage> CreateCityInDB(string name)
        {
            string json = JsonConvert.SerializeObject(new City { Name = name });
            HttpClient httpClient = new HttpClient();
            HttpContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            return httpClient.PostAsync(ConstantStrings.UrlLinkToCities, stringContent);
        }

    }
}
