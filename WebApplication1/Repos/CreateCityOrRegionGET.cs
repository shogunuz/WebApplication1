using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repos
{

    public class CreateCityOrRegionGET
    {
        public int CheckLocationExternal(string name, object obj)
        {
            return CheckLocationInternal(name,obj);
        }
        private int CheckLocationInternal(string name, object obj)
        {
            int id = -1;
            string link = CreateCityOrRegion.GetLink(obj);
           
            WebRequest request = WebRequest.Create(link);
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
                        switch (obj)
                        {
                            case City city:
                                city = serializer.Deserialize<City>(reader);
                                if (city.Name == name)
                                {
                                    id = city.Id;
                                }
                            break;
                            case Region region:
                                region = serializer.Deserialize<Region>(reader);
                                if (region.Name == name)
                                {
                                    id = region.Id;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return id;
        }
        
    }
}
