using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repos
{
    public class CreateCityOrRegion
    {
        private CreateCityOrRegionGET getter;
        private CreateCityOrRegionPOST setter;
        public CreateCityOrRegion()
        {
            getter = new CreateCityOrRegionGET();
            setter = new CreateCityOrRegionPOST();
        }
        public int DoesLocationExist(string name, object obj)
        {
            int id = getter.CheckLocationExternal(name, obj);
            if (id >= 0)
                return id;
            else
            {
                CreateCityOrRegionPOST.CreateNewLocation(name, obj).Wait();
                return getter.CheckLocationExternal(name, obj);
            }
        }

        public static string GetLink(object obj) => getLink(obj);

        private static string getLink(object obj)
        {
            if (obj is City _)
            {
                return ConstantStrings.UrlLinkGetCities;
            }
            else if (obj is Region _)
            {
                return ConstantStrings.UrlLinkGetRegions;
            }
            else
            {
                //wrong object data
                throw new Exception();
            }
        }
    }
}
