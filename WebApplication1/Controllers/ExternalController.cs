using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Repos;
using WebApplication1.Repos.ForeignAPI;

namespace WebApplication1.Controllers
{
    public class ExternalController : Controller
    {
        // GET: externalcontroller
        public ActionResult Index()
        {
            var list =
              JsonConvert.DeserializeObject<List<MyArray>>(
                  ForeignAPI.GetData(ConstantStrings.UrlLinkToForeignAPI));
            return View(list);
        }

        // GET: externalcontroller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
