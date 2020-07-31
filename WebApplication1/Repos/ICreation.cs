using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Repos
{
    public interface ICreation
    {
        public int Create(string Name);
        public int GetId(string name);
        public Task<IActionResult> CreateRecordInDB(string name);
    }
}
