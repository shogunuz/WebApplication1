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
        private CountryCreation countryCreation;
        private CreationArea creationCity;
        private CreationArea creationRegion;
        public CountriesController(CountryContext context)
        {
            _context = context;
            creationRegion = new CreationArea(new RegionCreation(_context));
            creationCity = new CreationArea(new CityCreation(_context));
            countryCreation = new CountryCreation(_context);
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
                int tmpId = countryCreation.GetCountryId(name);
                if(tmpId>=0)
                {
                    return RedirectToAction(nameof(Details), new { id = tmpId });
                }
                else
                {
                    return RedirectToAction(nameof(StateNotExist));
                }
            }
            else
            {
                return RedirectToAction(nameof(WrongData));
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
                state.CityId = creationCity.Create(City);
                state.RegionId = creationRegion.Create(Region);
                int tmpId = countryCreation.GetCountryId(state.Name);
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
