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
    public class RegionsController : Controller
    {
        private readonly CountryContext _context;
        private RegionCreation createRegion;
        public RegionsController(CountryContext context)
        {
            _context = context;
            createRegion = new RegionCreation(_context);
        }
        public IActionResult PublishMsg(string str)
        {
            ViewBag.Message = str;
            return View();
        }
        // GET: Regions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Regions.ToListAsync());
        }

        // GET: Regions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _context.Regions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // GET: Regions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Regions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                try
                {
                    int tmpId = createRegion.Create(name);

                    return RedirectToAction(nameof(Details),
                        new { id = tmpId });
                }
                catch (MyException ex)
                {
                    return RedirectToAction(nameof(PublishMsg), ex.Message);
                }
                catch (Exception e)
                {
                    return RedirectToAction(nameof(PublishMsg), e.Message);
                }
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
