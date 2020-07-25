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

    public class CreateCountry
    {
        public int GetCountryId(string name)
        {
            return getCountryId(name);
        }
        private int getCountryId(string name)
        {
            int id = -1;
            WebRequest request = WebRequest.Create(ConstantStrings.UrlLinkToContries);
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
                        Country country = serializer.Deserialize<Country>(reader);
                        if (country.Name == name)
                        {
                            id = country.Id;
                            break;
                        }
                    }
                }
            }
            return id;
        }

    }
}
