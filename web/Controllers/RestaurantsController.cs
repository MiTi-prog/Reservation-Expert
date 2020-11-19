using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;

namespace web.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly ResExpertContext _context;

        public RestaurantsController(ResExpertContext context)
        {
            _context = context;
        }

        // GET: Restaurants
        public async Task<IActionResult> Index(String sortOrder, String searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["LocationSortParm"] = sortOrder == "location" ? "location_desc" : "location";
            ViewData["OpenSortParm"] = sortOrder == "open" ? "open_desc" : "open";
            ViewData["CloseSortParm"] = sortOrder == "close" ? "close_desc" : "close";
            ViewData["CurrentFilter"] = searchString;


            var restaurants = from s in _context.Restaurants
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                 restaurants = restaurants.Where(s => s.NameOfRestaurant.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    restaurants = restaurants.OrderByDescending(s => s.NameOfRestaurant);
                    break;


                case "location":
                    restaurants = restaurants.OrderBy(s => s.Location);
                    break;
                case "location_desc":
                    restaurants = restaurants.OrderByDescending(s => s.Location);
                    break;
                

                case "open":
                    restaurants = restaurants.OrderBy(s => s.Open);
                    break;
                case "open_desc":
                    restaurants = restaurants.OrderByDescending(s => s.Open);
                    break;


                case "close":
                    restaurants = restaurants.OrderBy(s => s.Close);
                    break;
                case "close_dsc":
                    restaurants = restaurants.OrderByDescending(s => s.Close);
                    break;   

                default:
                    restaurants = restaurants.OrderBy(s => s.NameOfRestaurant);
                    break;
            }
            return View(await restaurants.AsNoTracking().ToListAsync());

        }

        // GET: Restaurants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(m => m.RestaurantID == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RestaurantID,NameOfRestaurant,Location,TableCapacity,MobileNumber,Open,Close")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RestaurantID,NameOfRestaurant,Location,TableCapacity,MobileNumber,Open,Close")] Restaurant restaurant)
        {
            if (id != restaurant.RestaurantID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.RestaurantID))
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
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurants
                .FirstOrDefaultAsync(m => m.RestaurantID == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.RestaurantID == id);
        }
    }
}
