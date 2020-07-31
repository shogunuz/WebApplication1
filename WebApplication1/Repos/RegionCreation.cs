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

    public class RegionCreation : ICreation
    {
        private readonly CountryContext _context;
        public RegionCreation(CountryContext context)
        {
            _context = context;
        }
        /*
        * CreateRegion receive checked(IsNullOrEmpty) name,
        * that's why it's not necessary to check that again.
        * Method CreateRegion should somehow verify city existion before to create
        * new one with this name. If there is a city with this name, CreateRegion will return
        * id to caller. Otherwise, it will call CreateRegionInDB in order to create new record 
        * in Region table in Database. Then, we will again need to get Id of the region, we have
        * just created in order to return result to the caller. So, in both cases I will need
        * GetRegionId method.
        */
        public int Create(string name)
        {
            int id = GetId(name);
            if (id >= 0)
                return id;
            else
            {
                CreateRecordInDB(name).Wait();
                return GetId(name);
            }
        }

        public int GetId(string name)
        {
           int getRegionId()
            {
                var region = _context.Regions.FirstOrDefault(n=>n.Name==name);
                if (region == null)
                    return -1;

                return region.Id;
            }

            return getRegionId();
        }

        public async Task<IActionResult> CreateRecordInDB(string name)
        {
            _context.Add(new Region { Name = name });
            await _context.SaveChangesAsync();
            return new OkResult();
        }

    }
}
