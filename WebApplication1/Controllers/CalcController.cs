using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class CalcController : Controller
    {
        // GET: Сфдс
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(int a, int b)
        {

            return View(a+b);
        }

        public ActionResult Sub(int a, int b)
        {
            return View(a - b);
        }

        public ActionResult Mul(int a, int b)
        {
            return View(a * b);
        }

        public ActionResult Div(int a, int b)
        {

            return View(a/b);
        }
      
        
    }
}