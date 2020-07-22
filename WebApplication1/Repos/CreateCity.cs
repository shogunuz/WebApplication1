using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication1.Repos
{
    public class CreateCity
    {
        private static readonly HttpClient client = new HttpClient();

        public bool CreateNewCity(string name)
        {
            bool res = false;


            async Task InnerMethodAsync()
            {
                var values = new Dictionary<string, string>
                    {
                        { "Name", name }
                    };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync("http://localhost:44393/Cities/Create", content);

                var responseString = await response.Content.ReadAsStringAsync();

                res = true;
            }

            return res;
        }
    }
}
