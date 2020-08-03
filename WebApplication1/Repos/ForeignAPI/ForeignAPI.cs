using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApplication1.Repos.ForeignAPI
{
    public class ForeignAPI
    {
        public static string GetData(string url)
        {
            string content = string.Empty;
            using (WebClient web = new WebClient())
            {
                content = web.DownloadString(url);
            }
            return content;   
        }

    }
}
