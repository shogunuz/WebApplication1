using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
using WebApplication1.Repos;

namespace WebApplication1.Repos
{
    public class CreateCity
    {
        public int DoesCityExist(string name)
        {
            int id = DoesCityExistCheck(name);
            if (id >= 0)
                return id;
            else
            {
                CreateNewCity(name).Wait();
                return DoesCityExistCheck(name);
            }
        }
        private int DoesCityExistCheck(string name)
        {
            int id = -1;
            WebRequest request = WebRequest.Create(ConstantStrings.UrlLinkGetCities);
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
                 */
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        City city = serializer.Deserialize<City>(reader);
                        if (city.Name == name)
                        {
                            id = city.Id;
                        }
                    }
                }
            }
            return id;
        }
        public static Task<HttpResponseMessage> CreateNewCity(string name)
        {
           City city = new City { Name = name };
            HttpClient httpClient = new HttpClient();
            string json = JsonConvert.SerializeObject(city);
            HttpContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            return httpClient.PostAsync(ConstantStrings.UrlLinkPostCities,
                stringContent);
        }
    }
}
