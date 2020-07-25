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
        private int GetCityId(string Name)
        {
            int id = -1;
                WebRequest request = WebRequest.Create(ConstantStrings.UrlLinkToCities);
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
                             City cityIn = serializer.Deserialize<City>(reader);
                             if (Name == cityIn.Name)
                            {
                                id = cityIn.Id;
                                break;
                            }
                        }
                    }
                }
            return id;
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
