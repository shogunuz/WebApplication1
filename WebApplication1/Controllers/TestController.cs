using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Message()
        {

            return View();
        }
        public ActionResult Ziba(string somesign)
        {

            return View(somesign as object);
        }

        public ActionResult Newcalc(string somevalue)
        {

            return View(somevalue as object);
        }

        public ActionResult Generatorlinks()
        {
            string url1 = Url.Action("Message"); // 
            string url2 = Url.Action("Ziba", new { id = 0 });
            string url3 = Url.Action("index", "First");
            string url4 = Url.Action("Ziba", "Test", new { somesign = "abs123"});


            string[] model = {url1,url2,url3,url4 };
            return View(model);
        }
    }
}