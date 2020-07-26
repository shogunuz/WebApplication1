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

    public class CreateRegion
    {
        private readonly CountryContext _context;
        public CreateRegion(CountryContext context)
        {
            _context = context;
        }
        public int RegionCreation(string name)
        {
            int id = GetRegionId(name);
            if (id >= 0)
                return id;
            else
            {
                CreateRegionInDB(name).Wait();
                return GetRegionId(name);
            }
        }
        private int GetRegionId(string name)
        {
           int getRegionId()
            {
                var region = _context.Regions.FirstOrDefault(n=>n.Name==name);
                if (region == null)
                    return -1;

                return region.Id;
            }

            return getRegionId();
        }

        private static Task<HttpResponseMessage> CreateRegionInDB(string name)
        {
            string json = JsonConvert.SerializeObject(new Region { Name = name });
            HttpClient httpClient = new HttpClient();
            HttpContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            return httpClient.PostAsync(ConstantStrings.UrlLinkToRegions, stringContent);
        }

    }
}
