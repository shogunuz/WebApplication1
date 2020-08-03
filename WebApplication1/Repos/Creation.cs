using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repos
{
    public class Creation
    {
        
        public ICreation creation;
        public Creation(ICreation creation)
        {
            this.creation = creation;
        }

        public int Create(string Name)
        {
            return creation.Create(Name);
        }
        public int GetId(string name) 
        {
            return creation.GetId(name);
        }
        public Task<IActionResult> CreateRecordInDB(string name)
        {
            return creation.CreateRecordInDB(name);
        }

    }
}
