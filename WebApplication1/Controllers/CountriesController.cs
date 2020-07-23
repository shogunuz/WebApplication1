using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Repos;
namespace WebApplication1.Controllers
{
    public class CountriesController : Controller
    {
        private readonly CountryContext _context;
        private CreateCity cc;
        private CreateRegion cr;
        private int CityId;
        private int RegionId;
        public CountriesController(CountryContext context)
        {
            _context = context;
            cc = new CreateCity();
            cr = new CreateRegion();
        }

        // GET: Countries
        public async Task<IActionResult> Index()
        {
            var countryContext = _context.Countries.Include(c => c.City);
            return View(await countryContext.ToListAsync());
        }
        public IActionResult StateNotExist()
        {
            return View();
        }

        public IActionResult NoResult()
        {
           return View();
        }
        public IActionResult PublishMsg(string str)
        {
            ViewBag.Message = str;
            return View();
        }
        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("StateNotExist");
            }

            var country = await _context.Countries
                .Include(c => c.City)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return RedirectToAction("StateNotExist");
            }

            return View(country);
        }
        
        public IActionResult Find()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Find(string name)
        {
            
            if (string.IsNullOrEmpty(name) == false)
                {
                   return innerMethod();
                }
            else
            {
                return RedirectToAction("NoResult");
            }
            IActionResult innerMethod()
            {
                var countryContext = _context.Countries.Include(c => c.City);
                foreach (var c in countryContext.ToList())
                {
                    if (c.Name == name)
                    {
                        return RedirectToAction("Details", new { id = c.Id });
                    }
                }
                return RedirectToAction(nameof(StateNotExist));
            }
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Id");
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Id");
           // ViewBag.Name = 
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(string City, string Region,string Area,
            [Bind("Name,StateCode,Population")] Country country)
        {
            if (string.IsNullOrEmpty(City) == false && string.IsNullOrEmpty(Region) == false)
            {
                double.TryParse(Area, NumberStyles.Number, CultureInfo.InvariantCulture, out double area);
                country.Area = area;
                country.CityId = cc.DoesCityExist(City);
                country.RegionId = cr.DoesRegionExist(Region);

                try
                {
                    _context.Add(country);
                    await _context.SaveChangesAsync();
                }
                catch(Exception ex)
                {
                    return RedirectToAction(nameof(PublishMsg), ex.Message);
                }
                
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Id", country.CityId);
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StateCode,Area,Population,CityId")] Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Id", country.CityId);
            return View(country);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .Include(c => c.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }
    }
}
