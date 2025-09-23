using System;
using System.Net;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
// Used modules and interfaces in the project
using BusinessObjects;
using BusinessLogic.Interfaces;

namespace Presentation.Controllers
{
    public class MainController : Controller
    {
        private readonly IItemCategoryControl _itemCategoryControl;
        private readonly IItemDistributionControl _itemDistributionControl;

        public MainController(IItemCategoryControl itemCategoryControl, IItemDistributionControl itemDistributionControl)
        {
            _itemCategoryControl = itemCategoryControl;
            _itemDistributionControl = itemDistributionControl;
        }

        // GET: The Main Index(Home) Page
        public ActionResult Index()
        {
            var categories = _itemCategoryControl.GetAllCategories();

            var items = (_itemDistributionControl.GetAllItems()).Where(i => i.CategoryId == null).ToList();
            if (items.Count != 0)
            {
                IEnumerable<Category> itemsWithoutCategory = new List<Category> { new Category { Id = 0, Items = items, Name = "Item has no category" } };
                categories = categories.Concat(itemsWithoutCategory);
            }
            return View(categories);
        }

        // Products Page
        public ActionResult Products()
        {
            var categories = _itemCategoryControl.GetAllCategories();

            var items = (_itemDistributionControl.GetAllItems()).Where(i => i.CategoryId != null).ToList();

            
           
            return View(items);
        }

        // About Page
        public ActionResult About()
        {
            return View();
        }

        // Blog Page, Needs yet to be tweaked a lot
        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult SingleBlog()
        {
            return View();
        }

        // Contact Information about the page
        public ActionResult Contact()
        {
            return View();
        }

        // Temporary soloution to a single Item display
        public ActionResult About_Item()
        {
            return View();
        }

        // Everything from her should contain the item display in the shop

        //public ActionResult About_Item(int? itemId)
        //{
        //    if (itemId == null)
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    try
        //    {
        //        var itemObject = _itemDistributionControl.GetItem(itemId.Value);
        //        return View(itemObject);
        //    }
        //    catch (ArgumentException)
        //    {
        //        return HttpNotFound();
        //    }
        //}

        // The function for searching certain items
        public ActionResult Item_Search(string searchQuarry)
        {
            var allItems = _itemDistributionControl.GetAllItems();

            // If the search quarry is not empty
            if (!string.IsNullOrEmpty(searchQuarry))
            {
                searchQuarry = searchQuarry.ToUpper();
                var searchQuarryInName = allItems.Where(x => x.Name.ToUpper().Contains(searchQuarry)).ToList();
                var searchQuarryInCategroy = allItems.Where(x => x.Category != null && x.Category.Name.ToUpper().Contains(searchQuarry)).ToList();
                // Search by Property should be added too


                // All found items unites here
                var allFoundItems = searchQuarryInName.Union(searchQuarryInCategroy);


                // Return items in a Partial view here in the same window (this might be changed
                return PartialView("_ItemsPartial", allFoundItems);
            }

            // If the search quarry is empty, we return all the items in the shop

            return PartialView("_ItemsPartial", allItems);
        }
    }
}