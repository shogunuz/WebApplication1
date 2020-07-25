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
            int id = -1;
            WebRequest request = WebRequest.Create(ConstantStrings.UrlLinkToRegions);
            using (WebResponse response = request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader streamReader = new StreamReader(stream))
            using (JsonTextReader reader = new JsonTextReader(streamReader))
            {
                reader.SupportMultipleContent = true;
                var serializer = new JsonSerializer();

                /* Хочу читать по кусочкам, а не целую порцию,
                 * ибо не факт, что я всегда буду получать маленькую 
                 * порцию данных.
                 * In terms of drawbacks, I make connect engaged while loop is working...
                 */
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        Region region = serializer.Deserialize<Region>(reader);
                        if (region.Name == name)
                        {
                            id = region.Id;
                            break;
                        }
                    }
                }
            }

            return id;
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
