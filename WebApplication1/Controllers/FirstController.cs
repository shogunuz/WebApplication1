using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FirstController : Controller
    {
        // GET: First
        public ActionResult Index()
        {
            Product product = new Product();
            product.ProductIdEdit(1);
            product.ProductNameEdit("Coca-Cola");
            product.ProductPriceEdit(60); // Rubles 

            return View(product);
        }

        public ActionResult Calc(int x, int y)
        {
            int res = x + y;
            return View(res);
        }

    }
}