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

    public class CountryCreation
    {
        private readonly CountryContext _context;
        public CountryCreation(CountryContext context)
        {
            _context = context;
        }
        public int GetCountryId(string name)
        {
            int getCountryId()
            {
                var country = _context.Countries
                    .FirstOrDefault(n => n.Name == name);

                if (country == null)
                    return -1;

                return country.Id;
            }

            return getCountryId();
        }

    }
}
