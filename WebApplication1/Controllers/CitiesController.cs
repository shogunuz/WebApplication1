using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Exceptions;
using WebApplication1.Models;
using WebApplication1.Repos;

namespace WebApplication1.Controllers
{
    public class CitiesController : Controller
    {
        private readonly CountryContext _context;
        private CityCreation cityCreation;
        public CitiesController(CountryContext context)
        {
            _context = context;
            cityCreation = new CityCreation(_context);
        }
        public IActionResult PublishMsg(string str)
        {
            ViewBag.Message = str;
            return View();
        }
        // GET: Cities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cities.ToListAsync());
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // GET: Cities/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (string.IsNullOrEmpty(name) == false)
            {
                try
                {
                    int tmpId = cityCreation.CreateCity(name);
                    return RedirectToAction(nameof(Details), new { id = tmpId });
                }
                catch (MyException ex)
                {
                    return RedirectToAction(nameof(PublishMsg), new { str = ex.Message });
                }
                catch (Exception e)
                {
                    return RedirectToAction(nameof(PublishMsg),
                        new { str = e.Message });
                }
            }
            return RedirectToAction(nameof(Index));
        }


    
    }
}
