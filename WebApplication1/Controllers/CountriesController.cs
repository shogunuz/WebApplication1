using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class CountriesController : Controller
    {
        private readonly CountryContext _context;
        private int CityId;
        private int RegionId;
        private CreateCity cc;
        private CreateRegion cr;
        private CreateCountry cs;
        public CountriesController(CountryContext context)
        {
            _context = context;
            cr = new CreateRegion(_context);
            cc = new CreateCity(_context);
            cs = new CreateCountry();
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

        public IActionResult WrongData()
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
                return RedirectToAction(nameof(StateNotExist));
            }

            var country = await _context.Countries
                .Include(c => c.City)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return RedirectToAction(nameof(StateNotExist));
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
                return RedirectToAction(nameof(WrongData));
            }
            IActionResult innerMethod()
            {
                var countryContext = _context.Countries.Include(c => c.City);
                foreach (var c in countryContext.ToList())
                {
                    if (c.Name == name)
                    {
                        return RedirectToAction(nameof(Details), new { id = c.Id });
                    }
                }
                return RedirectToAction(nameof(StateNotExist));
            }
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(string City, string Region,string Area,
            [Bind("Name,StateCode,Population")] Country state)
        {
            if (string.IsNullOrEmpty(City) == false && string.IsNullOrEmpty(Region) == false)
            {
                double.TryParse(Area, NumberStyles.Number, CultureInfo.InvariantCulture, out double area);
                state.Area = area;
                state.CityId = cc.CityCreation(City);
                state.RegionId = cr.RegionCreation(Region);
                int tmpId = cs.GetCountryId(state.Name);
                if ( tmpId < 0)
                {
                    try
                    {
                        _context.Add(state);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Details), new { id = state.Id });
                    }
                    catch (MyException ex)
                    {
                        return RedirectToAction(nameof(PublishMsg), new { str = ex.Message });
                    }
                    catch (Exception e)
                    {
                        return RedirectToAction(nameof(PublishMsg), 
                            new {str = e.Message });
                    }
                }
                else
                {
                    state.Id = tmpId;
                    try
                    {
                        _context.Update(state);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Details), new { id = state.Id });
                    }
                    catch (DbUpdateConcurrencyException ed)
                    {
                        return RedirectToAction(nameof(PublishMsg),
                          new { str = ed.Message });
                    }
                    catch (Exception ex)
                    {
                        return RedirectToAction(nameof(PublishMsg),
                          new { str = ex.Message });
                    }
                }

            }
            return RedirectToAction(nameof(Index));
        }
    }
}
