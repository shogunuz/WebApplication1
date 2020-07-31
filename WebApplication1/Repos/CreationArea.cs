using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Repos
{
    public class CreationArea
    {
        
        public ICreation Creation;
        public CreationArea(ICreation creation)
        {
            this.Creation = creation;
        }

        public int Create(string Name)
        {
            return Creation.Create(Name);
        }
        public int GetId(string name) 
        {
            return Creation.GetId(name);
        }
        public Task<IActionResult> CreateRecordInDB(string name)
        {
            return Creation.CreateRecordInDB(name);
        }

    }
}
