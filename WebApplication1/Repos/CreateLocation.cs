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
    public class CreateLocation
    {
        private CreateLocationGET getter;
        private CreateLocationPOST setter;
        public CreateLocation()
        {
            getter = new CreateLocationGET();
            setter = new CreateLocationPOST();
        }
        public int DoesLocationExist(string name, object obj)
        {
            int id = getter.CheckLocationExternal(name, obj);
            if (id >= 0)
                return id;
            else
            {
                setter.CreateNewLocation(name, obj).Wait();
                return getter.CheckLocationExternal(name, obj);
            }
        }

       
    }
}
