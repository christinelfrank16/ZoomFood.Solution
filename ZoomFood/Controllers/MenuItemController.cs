using Microsoft.AspNetCore.Mvc;
using ZoomFood.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ZoomFood.Controllers
{
    public class MenuItemController : Controller
    {
        private readonly ZoomFoodContext _db;

        public MenuItemController(ZoomFoodContext db)
        {
            _db = db;
        }

        // public ActionResult Index()
        // {
        //     List<Restaurant> model = _db.Restaurants.ToList();
        //     return View(model);
        // }
        [HttpGet("MenuItem/Create/{restaurantId}")]
        public ActionResult Create(int restaurantId)
        {
            ViewBag.RestaurantId = restaurantId;
            return View();
        }
        [HttpPost]
        public ActionResult Create(MenuItem menuItem)
        {
            _db.MenuItems.Add(menuItem);
            _db.SaveChanges();
            int id = menuItem.RestaurantId;
            return RedirectToAction("Details", "Restaurant", id);
        }

        public ActionResult Delete(int id)
        {
            MenuItem thisMenuItem = _db.MenuItems.FirstOrDefault(menuItem => menuItem.MenuItemId == id);
            return View(thisMenuItem);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            MenuItem thisMenuItem = _db.MenuItems.FirstOrDefault(menuItem => menuItem.MenuItemId == id);
            int restaurantId = thisMenuItem.RestaurantId;
            _db.MenuItems.Remove(thisMenuItem);
            _db.SaveChanges();
            return RedirectToAction("Details", "Restaurant", restaurantId);
        }
        public ActionResult Edit(int id)
        {
            MenuItem thisMenuItem = _db.MenuItems.FirstOrDefault(menuItem => menuItem.MenuItemId == id);
            ViewBag.CuisineId = new SelectList(_db.Cuisines, "CuisineId", "Name");
            return View(thisMenuItem);
        }
        [HttpPost]
        public ActionResult Edit(MenuItem menuItem)
        {
            _db.Entry(menuItem).State = EntityState.Modified;
            _db.SaveChanges();
            int restaurantId = menuItem.RestaurantId;
            return RedirectToAction("Details", "Restaurant", restaurantId);
        }
        public ActionResult Details(int id)
        {
            MenuItem thisMenuItem = _db.MenuItems.Include(item => item.Restaurant).FirstOrDefault(menuItem => menuItem.MenuItemId == id);
            return View(thisMenuItem);
        }
    }
}