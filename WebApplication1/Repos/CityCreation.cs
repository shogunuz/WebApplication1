using Microsoft.AspNetCore.Mvc;
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

    public class CityCreation
    {
        private readonly CountryContext _context;
        public CityCreation(CountryContext context)
        {
            _context = context;
        }

        /*
        * CreateCity receive checked(IsNullOrEmpty) Name,
        * that's why it's not necessary to check that again.
        * Method CreateCity should somehow verify city existion before to create
        * new one with this name. If there is a city with this name, CreateCity will return
        * id to caller. Otherwise, it will call CreateCityInDB in order to create new record 
        * in City table in Database. Then, we will again need to get Id of the city, we have
        * just created in order to return result to the caller. So, in both cases I will need
        * GetCityId method.
        */

        public int CreateCity(string Name)
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

       
        private int GetCityId(string name)
        {
            int getCityId()
            {
                var city = _context.Cities
                    .FirstOrDefault(n => n.Name == name);

                if (city == null)
                    return -1;

                return city.Id;
            }

            return getCityId();
        }
        private async Task<IActionResult> CreateCityInDB(string name)
        {
            _context.Add(new City { Name = name});
            await _context.SaveChangesAsync();
            return new OkResult();
        }

    }
}
