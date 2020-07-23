using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
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
        public int DoesRegionExist(string name)
        {
           
            int id = DoesRegionExistCheck(name);
            if (id >= 0)
                return id;
            else
            {
                CreateNewRegion(name).Wait();
                return DoesRegionExistCheck(name);
            }
        }
        private int DoesRegionExistCheck(string name)
        {
            int id = -1;
            WebRequest request = WebRequest.Create(ConstantStrings.UrlLinkGetRegions);
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
                        Region region = serializer.Deserialize<Region>(reader);
                        if (region.Name == name)
                        {
                            id = region.Id;
                        }
                    }
                }
            }
            return id;
        }

        public static Task<HttpResponseMessage> CreateNewRegion(string name)
        {
            Region region = new Region { Name = name };
            HttpClient httpClient = new HttpClient();
            string json = JsonConvert.SerializeObject(region);
            HttpContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            return httpClient.PostAsync(ConstantStrings.UrlLinkPostRegions,
                stringContent);
        }


    }
}
