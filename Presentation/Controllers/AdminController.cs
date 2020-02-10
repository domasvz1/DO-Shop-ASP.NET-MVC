using System.Net;
using System.Web.Mvc;
using System.Web;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Web.Security;
// Used modules and interfaces in the project
using BusinessObjects;
using BusinessObjects.Orders;
using BusinessLogic.Interfaces;
using Presentation.Models;

namespace Presentation.Controllers
{
    public class AdminController : Controller
    {
        private readonly IItemDistributionControl _itemDistributionControl;
        private readonly IItemControl _itemControl;
        private readonly IItemCategoryControl _itemCategoryControl;
        private readonly IPropertyControl _propertyControl;
        private readonly IClientProfileControl _clientProfileControl;
        private readonly IAdminControl _adminControl;
        private readonly IOrderControl _orderControl;
       
        public AdminController(
            IItemDistributionControl itemDistributionControl,
            IItemCategoryControl itemCategoryControl,
            IItemControl itemControl,
            IPropertyControl propertyControl,
            IClientProfileControl clientProfileControl,
            IAdminControl adminControl,
            IOrderControl orderControl)
        {
            _itemDistributionControl = itemDistributionControl;
            _itemControl = itemControl;
            _itemCategoryControl = itemCategoryControl;
            _propertyControl = propertyControl;
            _clientProfileControl = clientProfileControl;
            _adminControl = adminControl;
            _orderControl = orderControl;
        }

        //Admin Connection view, locate in "DO SHOP project folder -> Views-> Admin"
        //[UserAuthorization(ConnectionPage = "~/Admin/Login", Roles = "Admin")]
        public ActionResult Index()
        {
            // Check if not there are no items, 
            var newVar = _itemDistributionControl.GetAllItems();

            if (newVar.Count() == 0)
            {
                // Do something here
                return View(_itemDistributionControl.GetAllItems());
            }
            else
            {
                return View(_itemDistributionControl.GetAllItems());
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin, string returnUrl)
        {
            // Give admin base object and lets go to the admin control class
            var adminObject = _adminControl.ConnectAdmin(admin);

            // If Admin object has been found
            if (adminObject != null)
            {
                // Find out how to use admin session more
                FormsAuthentication.SetAuthCookie("a" + adminObject.Login, false);
                Session["AccountId"] = adminObject.Id;
                Session["AccountUsername"] = adminObject.Login;
                Session["IsAdminAccount"] = true;
                if (returnUrl == null || returnUrl == string.Empty)
                {
                    returnUrl = "Index";
                }
                return Redirect(returnUrl);
            }

            // If there's none Admin object detected
            else if (adminObject == null)
            {
                ModelState.AddModelError("", "Admin object was not found in the database");
                return Redirect("Index");
            }

            else
                ModelState.AddModelError("", "Bad login, sorry");

            return View(admin);
        }

        public ActionResult LogOut()
        {
            string adminLogin = Session["AccountUsername"].ToString();
            Session["AccountId"] = null;
            Session["AccountUsername"] = null;
            Session["IsAdminAccount"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        // SystemUers - everyone who uses this system-> cleints/admins [at least for now]
        public ActionResult SystemUsers() => View();

        //UsersList has no references.This ActionResult is called when admin wants to modify the user
        public ActionResult UsersList(string searchQuarry)
        {
            List<Client> allClients = _clientProfileControl.GetClientsList()
                .Select(x => new Client
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    IsBlocked = x.IsBlocked
                }).Distinct().ToList();

            List<Client> allClientsAfterQuarry;

            // If the search quarry is empty or null show all clients in the system
            if (string.IsNullOrEmpty(searchQuarry))
            {
                allClientsAfterQuarry = allClients;
            }
            else
            {
                searchQuarry = searchQuarry.ToUpper();
                allClientsAfterQuarry = allClients.Where(x => x.FirstName.ToUpper().Contains(searchQuarry) ||
                x.LastName.ToUpper().Contains(searchQuarry) ||
                x.Email.ToUpper().Contains(searchQuarry) ||
                (x.FirstName.ToUpper() + " " + x.LastName.ToUpper()).Contains(searchQuarry))
                    .Select(x => new Client
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        IsBlocked = x.IsBlocked
                    }).Distinct().ToList();
            }

            return PartialView("../Admin/_UsersList", allClientsAfterQuarry);
        }

        //No references to this Actionresult.This is only called from _UsersList PartialView
        public ActionResult GetAllClients()
        {
            List<Client> allClients = _clientProfileControl.GetClientsList()
                .Select(x => new Client
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    IsBlocked = x.IsBlocked
                }).Distinct().ToList();

            return PartialView("../Admin/_Search", allClients);
        }

        // Right now Client has only 2 Statuses, Blocked and not blocked
        /// <summary>
        /// When Admin wants to modify the status, we go to SystemUser View and it goes to UsersList Action 
        /// </summary>
        public ActionResult ModifyClientsStatus(int klientoId)
        {
            var clientObject = _clientProfileControl.GetClient(klientoId);

            if (ModelState.IsValid)
            {
                if (clientObject != null)
                    _clientProfileControl.EditStatus(clientObject);
            }
            return RedirectToAction("SystemUsers", "Admin");
        }

        public ActionResult ItemManagement()
        {
            
            return View(new ItemExportModel {
                ExportItemsToFile = GetExportedFile()
            });
        }

        [HttpPost]
        public ActionResult ItemManagement([Bind(Include = "file")] HttpPostedFileBase file)
        {
            // If there's no selected file or the file is empty
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    // The functionality is in FileControl (BUSINESS LOGIC LAYER) in LoadShopItemsFile method
                    // All is neeeded here to pass the item and image location folders in the Project folder.. presentation layer 
                    string itemsLocation = Server.MapPath("~/Uploads/Items"), imagesLocation = Server.MapPath("~/Uploads/Pictures");
                    _itemControl.StartImportingItems(itemsLocation, file, imagesLocation);

                    // After import is done there should be made another window
                    ViewBag.Message = "Items were successfully importer, go back to  main page to search for items.";
                    return View("ItemManagement");
                }

                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR: " + ex.Message.ToString();
                    return View("Index");
                }
            }
            else
            {
                string errorMsg = "You haven't chosen the file";
                ModelState.AddModelError("", errorMsg);
                ViewBag.Message = errorMsg;
                return View("ItemManagement");
            }
        }

        //Returns a list from the selected directory
        private List<FileInfo> GetExportedFile()
        {
            string path = Server.MapPath("~/Content/Items"); // Here should be a check,
            // It returns a list of files

            // Added a check if directory doesnt exist, then we return an empty list of FileInfo path
            if(!Directory.Exists(path))
            {
                // The app doesnt crash here anymore it return an empty list
                return new List<FileInfo>();
            }

            // If the .xlsx file exist there, it is returned
            return new DirectoryInfo(path).GetFiles("*.xlsx").ToList();
        }

        // This Action has no references. It it called from the ItemManagement View
        public ActionResult ExportItems()
        {
            var allItems = _itemDistributionControl.GetAllItems();
            _itemControl.ExportItemsTask(allItems, Server.MapPath("~/Content/Items"));
            return RedirectToAction("SuccessfulExport");
        }

        //[UserAuthorization(ConnectionPage = "~/Admin/Login", Roles = "Admin")]
        public ActionResult SuccessfulImport()
        {
            return View();
        }

        //[UserAuthorization(ConnectionPage = "~/Admin/Login", Roles = "Admin")]
        public ActionResult SuccessfulExport()
        {
            return View();
        }

        //GET: Admin/ItemInformation
        public ActionResult ItemInformation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _itemDistributionControl.GetItem(id.Value);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);
        }

        //GET: Admin/AddItem
        [UserAuthorization(ConnectionPage = "~/Admin/Login", Roles = "Admin")]
        public ActionResult AddItem()
        {
            ViewBag.CategoryId = new SelectList(_itemCategoryControl.GetAllCategories(), "Id", "Name");
            ViewBag.PropertyId = new SelectList(_propertyControl.GetAllProperties(), "Id", "Name");
            return View(new Item() { ItemProperties = _propertyControl.GetAllProperties().Select(x => new ItemProperty { Property = x, PropertyId = x.Id }).ToList() });
        }

        //POST: Admin/AddItem
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddItem([Bind(Include = "Id,SKUCode,Headline,Name,Price,Description,ImageUrl,Image,CategoryId,ItemProperties")] Item item)
        {
            ViewBag.CategoryId = new SelectList(_itemCategoryControl.GetAllCategories(), "Id", "Name");
            if (ModelState.IsValid)
            {
                try
                {
                    item.ItemProperties = item.ItemProperties.Where(x => x.Value != null && x.Value != "").ToList();
                    _itemControl.CreateItemWithPicture(item, Server.MapPath("~/Content/Pictures"));

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewBag.CategoryId = new SelectList(_itemCategoryControl.GetAllCategories(), "Id", "Name", item.CategoryId);
            return View(item);
        }

        // GET: Admin/ModifyItem
        public ActionResult ModifyItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item itemObject = _itemDistributionControl.GetItem(id.Value);
            if (itemObject == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(_itemCategoryControl.GetAllCategories(), "Id", "Name", itemObject.CategoryId);
            return View(itemObject);
        }

        // POST: Admin/ModifyItem
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifyItem([Bind(Include = "Id,SKUCode,Headline,Name,Price,Description,CategoryId,ItemProperties,Property")] Item item)
        {
            ViewBag.CategoryId = new SelectList(_itemCategoryControl.GetAllCategories(), "Id", "Name", item.CategoryId);
            if (ModelState.IsValid)
            {
                _itemControl.UpdateItem(item);
                return RedirectToAction("ItemInformation", new { @id = item.Id });
            }

            return View(item);
        }

        public ActionResult ChangeItemsPicture(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item keiciamaPreke = _itemDistributionControl.GetItem(id.Value);
            keiciamaPreke.ImageUrl = null;
            if (keiciamaPreke == null)
            {
                return HttpNotFound();
            }
            return View(keiciamaPreke);
        }

        [HttpPost]
        public ActionResult ChangeItemsPicture([Bind(Include = "Id, ImageUrl, Image")] Item model)
        {
            try
            {
                _itemControl.UpdateItemsPicture(model, Server.MapPath("~/Uploads/Pictures"));
                return RedirectToAction("ModifyItem", new { @id = model.Id });
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }

        //GET: Admin/RemoveItem
        public ActionResult RemoveItem(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Item preke = _itemDistributionControl.GetItem(id.Value);
            if (preke == null)
                return HttpNotFound();

            return View(preke);
        }

        //ApproveTheRemovalOfItem works and should call a pop up that shows that it was removed
        [HttpPost, ActionName("RemoveItem")]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveTheRemovalOfItem(int id)
        {
            try
            {
                _itemControl.DeleteItem(id);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult ItemProperties(FormCollection fc)
        {
            int categoryId = 0;
            List<ItemProperty> itemProperties = new List<ItemProperty>();
            try
            {
                categoryId = Convert.ToInt32(fc["CategoryId"]);
                var properties = _itemCategoryControl.GetCategory(categoryId).Properties;

                itemProperties = properties.Select(x => new ItemProperty { Property = x, PropertyId = x.Id }).ToList();
            }
            
            // If we do not have any properties
            catch (FormatException)
            {
                var properties = _propertyControl.GetAllProperties();
                if (properties != null && properties.Count > 0)
                    itemProperties = properties.Select(x => new ItemProperty { Property = x, PropertyId = x.Id }).ToList();
                else
                    return Content("<html></html>");
            }

            ViewData = new ViewDataDictionary { TemplateInfo = new TemplateInfo { HtmlFieldPrefix = "ItemProperties" } };
            return PartialView("_ItemProperties", itemProperties);
        }

        // GET: Categories
        public ActionResult Categories()
        {
            return View(_itemCategoryControl.GetAllCategories());
        }

        // GET: Admin/CategoryInformation
        public ActionResult CategoryInformation(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            try
            {
                var category = _itemCategoryControl.GetCategory(id.Value);
                return View(category);
            }
            catch (ArgumentException)
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            CategoryModel model = new CategoryModel();
            var properties = _propertyControl.GetAllProperties();
            var checkBoxListItems = new List<CheckBoxListItem>();
            properties.ForEach(x => checkBoxListItems.Add(new CheckBoxListItem()
            {
                ID = x.Id,
                Display = x.Name,
                IsChecked = false
            }));

            model.Properties = checkBoxListItems;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory(CategoryModel model)
        {

            var selectedProperties = model.Properties.Where(x => x.IsChecked).Select(x => x.ID).ToList();
            if (ModelState.IsValid)
            {
                try
                {
                    _itemCategoryControl.CreateCategory(model.Name, selectedProperties);
                    return RedirectToAction("Categories");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
            }

        // GET: Admin/ModifyCategory
        public ActionResult ModifyCategory(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            try
            {
                Category category = _itemCategoryControl.GetCategory(id.Value);
                CategoryModel model = new CategoryModel() { Name = category.Name, CategoryId = category.Id };

                var properties = _propertyControl.GetAllProperties();
                var checkBoxListItems = new List<CheckBoxListItem>();
                properties.ForEach(x => checkBoxListItems.Add(new CheckBoxListItem()
                {
                    ID = x.Id,
                    Display = x.Name,
                    IsChecked = category.Properties.Any(y => y.Id == x.Id)
                }));

                model.Properties = checkBoxListItems;
                return View(model);
            }
            catch (ArgumentException)
            {
                return HttpNotFound();
            }
        }

        // POST: Admin/ModifyCategory
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifyCategory([Bind(Include = "Id,Name,Properties")] CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var selectedProperties = category.Properties.Where(x => x.IsChecked).Select(x => x.ID).ToList();
                    _itemCategoryControl.UpdateCategory((int)category.CategoryId, category.Name, selectedProperties);

                    // After saving the updated category, we return to the categories list
                    return RedirectToAction("CategoryInformation", new { @id = category.CategoryId});
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(category);
        }

        // GET: Admin/RemoveCategory
        public ActionResult RemoveCategory(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            try
            {
                Category category = _itemCategoryControl.GetCategory(id.Value);
                return View(category);
            }
            catch (ArgumentException)
            {
                return HttpNotFound();
            }
        }

        // POST: Admin/RemoveCategory
        [HttpPost, ActionName("RemoveCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveRemoval(int id)
        {
            try
            {
                _itemCategoryControl.DeleteCategory(id);
                return RedirectToAction("Categories");
            }
            catch (ArgumentException)
            {
                return HttpNotFound();
            }
        }

        public ActionResult Properties()
        {
            return View(_propertyControl.GetAllProperties());
        }

        public ActionResult AddProperty()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProperty([Bind(Include = "Name")] Property property)
        {
            if (ModelState.IsValid)
            {
                _propertyControl.CreateProperty(property.Name);
                return RedirectToAction("Properties");
            }
            return View(property);
        }


        // GET: Admin/ModifyProperty
        public ActionResult ModifyProperty(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            try
            {
                Property category = _propertyControl.GetProperty(id.Value);
                return View(category);
            }
            catch (ArgumentException)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifyProperty([Bind(Include = "Id, Name")] Property property)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _propertyControl.UpdateProperty(property);
                    return RedirectToAction("Properties");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(property);
        }

        public ActionResult RemoveProperty(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            try
            {
                Property property = _propertyControl.GetProperty(id.Value);
                return View(property);
            }
            catch (ArgumentException)
            {
                return HttpNotFound();
            }
        }

        [HttpPost, ActionName("RemoveProperty")]
        [ValidateAntiForgeryToken]
        public ActionResult ApprovePropertyRemoval(int id)
        {
            try
            {
                _propertyControl.DeleteProperty(id);
                return RedirectToAction("Properties");
            }
            catch (ArgumentException)
            {
                return HttpNotFound();
            }
        }

        // Orders
        public ActionResult HandleOrders()
        {
            return View(_orderControl.GetAllOrders());
        }

        [UserAuthorization(ConnectionPage = "~/Admin/Login", Roles = "Admin")]
        public ActionResult ModifyOrderStatus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientOrders order = _orderControl.GetClientOrder(id.Value);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // Id Of the certain order binds here with the OrderStatus class using 'Bind'
        [UserAuthorization(ConnectionPage = "~/Admin/Login", Roles = "Admin")]
        [HttpPost]
        public ActionResult ModifyOrderStatus([Bind(Include = "Id, OrderStatus")] ClientOrders order)
        {
            _orderControl.UpdateOrderStatus(order.Id, order.OrderStatus);
            return RedirectToAction("HandleOrders");
        }

        // The function for searching certain items
        public ActionResult Item_Search(string searchQuarry)
        {
            List<Item> foundItems = new List<Item>();


            // Mainly it returs a list of all items for Partial voew
            var allItems = _itemDistributionControl.GetAllItems();

            // If the search quarry is not empty
            if (!string.IsNullOrEmpty(searchQuarry))
            {

                searchQuarry = searchQuarry.ToUpper();
                // Search by Name
                foundItems.AddRange(allItems.Where(x =>
                x.Name.ToUpper().Contains(searchQuarry)).ToList());

                // Search by SKU Code
                foundItems.AddRange(allItems.Where(x =>
                x.SKUCode.ToUpper().Contains(searchQuarry)).ToList());

                // Search by Category
                foundItems.AddRange(allItems.Where(x =>
                x.Category != null && x.Category.Name.ToUpper().Contains(searchQuarry)).ToList());

                // Search by Price
                foundItems.AddRange(allItems.Where(x =>
                x.Price.ToString().ToUpper().Contains(searchQuarry)).ToList());

                // Search by Headline
                foundItems.AddRange(allItems.Where(x =>
                x.Headline != null && x.Headline.ToUpper().Contains(searchQuarry)).ToList());

                // Description searching
                foundItems.AddRange(allItems.Where(x =>
                x.Description != null && x.Description.ToUpper().Contains(searchQuarry)).ToList());

                // Return items in a Partial view here in the same window (this might be changed
                return PartialView(foundItems);
            }

            // If the search quarry is empty, we return all the items in the shop
            return PartialView(allItems);
        }
    
    }
}