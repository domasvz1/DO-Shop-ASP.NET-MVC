using System;
using System.IO;
using System.Web;
using System.Linq;
using Mehdime.Entity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
//Used modules and Interfacesfrom the project
using BusinessObjects;
using DataAccess.Interfaces;
using BusinessLogic.Interfaces;

namespace BusinessLogic
{
    public class ItemControl : IItemControl
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IItemRepository _ItemRepository;
        private readonly IPropertyControl _IPropertyControl;
        private readonly IFileControl _FileControl;
        private readonly IImportControl _ImportControl;
        private readonly IItemCategoryControl _ItemCategoryControl;

        public ItemControl(IDbContextScopeFactory dbContextScopeFactory, IItemRepository itemRepository, IImportControl importControl, IFileControl fileControl, IItemCategoryControl itemCategoryControl, IPropertyControl propertyControl)
        {
            _dbContextScopeFactory = dbContextScopeFactory ?? throw new ArgumentNullException("item control broke");
            _ItemRepository = itemRepository ?? throw new ArgumentNullException("item control broke");
            _FileControl = fileControl ?? throw new ArgumentNullException("item control broke");
            _ImportControl = importControl ?? throw new ArgumentNullException("item control broke");
            _ItemCategoryControl = itemCategoryControl ?? throw new ArgumentNullException("item control broke");
            _IPropertyControl = propertyControl ?? throw new ArgumentNullException("item control broke");
        }

        public void CreateItem(Item item)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                Item rastaPreke = _ItemRepository.GetItem(item.Id);
                if (rastaPreke != null)
                    throw new Exception("Tokia preke jau yra");

                _ItemRepository.CreateItem(item);
                dbContextScope.SaveChanges();
            }
        }

        public void CreateItemWithPicture(Item item, string pictureRepository)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                Item rastaPreke = _ItemRepository.GetItem(item.Id);
                if (rastaPreke != null)
                    throw new Exception("Preke jau egzsituoja");
                try
                {
                    if (String.IsNullOrEmpty(item.ImageUrl) && item.Image != null)
                    {
                        string[] direktorijos = pictureRepository.Split(Path.DirectorySeparatorChar);
                        string galiausiaDir = Path.DirectorySeparatorChar + Path.Combine(direktorijos[direktorijos.Length - 2], Path.Combine(direktorijos[direktorijos.Length - 1], _FileControl.LoadShopItemsFile(pictureRepository, item.Image)));
                        if (!CheckPictureExtension(galiausiaDir))
                            throw new Exception("Neteisingas nuotraukos formatas");

                        item.ImageUrl = galiausiaDir;
                    }
                    else if ((!String.IsNullOrEmpty(item.ImageUrl) && item.Image == null) ||
                        (!String.IsNullOrEmpty(item.ImageUrl) && item.Image != null))
                    {
                        if (!CheckPictureExtension(item.ImageUrl))
                            throw new Exception("Neteisingas nuotraukos formatas");

                        item.ImageUrl = item.ImageUrl;
                    }
                    else
                        item.ImageUrl = null;
                }
                catch
                {
                    throw;
                }
                _ItemRepository.CreateItem(item);
                dbContextScope.SaveChanges();
            }    
        }

        public void UpdateItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException("AtnaujinamaPreke");

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {    
                var foundItemObject = _ItemRepository.GetItem(item.Id);

                foundItemObject.SKUCode = item.SKUCode;
                foundItemObject.Name = item.Name;
                foundItemObject.Description = item.Description;
                foundItemObject.Price = item.Price;
                foundItemObject.Headline = item.Headline;
                if (item.CategoryId != foundItemObject.CategoryId)
                {
                    foundItemObject.Category = item.Category;
                    foundItemObject.CategoryId = item.CategoryId;
                }
                foundItemObject.ItemProperties = item.ItemProperties;
                _ItemRepository.EditItem(foundItemObject);
                dbContextScope.SaveChanges();
            }
        }

        public void UpdateItemsPicture(Item item, string photoRepository)
        {
            if (item == null)
                throw new ArgumentNullException("Updating the item");

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {

                var foundItemObject = _ItemRepository.GetItem(item.Id);
                if (foundItemObject == null)
                {
                    throw new Exception("No such category found");
                }
                try
                {
                    if (String.IsNullOrEmpty(item.ImageUrl) && item.Image != null)
                    {
                        string[] repositories = photoRepository.Split(Path.DirectorySeparatorChar);
                        string repository = Path.DirectorySeparatorChar + Path.Combine(repositories[repositories.Length - 2], Path.Combine(repositories[repositories.Length - 1], _FileControl.LoadShopItemsFile(photoRepository, item.Image)));
                        if (!CheckPictureExtension(repository))
                        {
                            throw new Exception("The picture extension does not fit the system criteria");
                        }
                        item.ImageUrl = repository;
                    }
                    else if ((!String.IsNullOrEmpty(item.ImageUrl) && item.Image == null) ||
                        (!String.IsNullOrEmpty(item.ImageUrl) && item.Image != null))
                    {
                        if (!CheckPictureExtension(item.ImageUrl))
                        {
                            throw new Exception("The picture extension does not fit the system criteria");
                        }
                        item.ImageUrl = item.ImageUrl;
                    }
                    else
                        item.ImageUrl = null;
                }
                catch
                {
                    throw;
                }

                foundItemObject.ImageUrl = item.ImageUrl;
                foundItemObject.Image = item.Image;
                _ItemRepository.EditItem(foundItemObject);
                dbContextScope.SaveChanges();
            }
        }

        public void DeleteItem(int id)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var foundItemObject = _ItemRepository.GetItem(id);
                if (foundItemObject == null)
                {
                    throw new Exception("The item you are trying to delete does not exist");
                }
                _ItemRepository.DeleteItem(foundItemObject);
                dbContextScope.SaveChanges();
            }
        }

        public void AddPropertyToItem(int itemId, int propertyId, string propertyInfo)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var itemObject = _ItemRepository.GetItem(itemId);
                var propertyObject = _IPropertyControl.GetProperty(propertyId);

                if (itemObject == null)
                    throw new ArgumentException($"Preke su tokiu Id nebuvo rasta");

                if (propertyObject == null)
                    throw new ArgumentException($"Preke su tokia savybe nebuvo rasta");

                var itemProperty = new ItemProperty
                {
                    ItemId = itemId,
                    PropertyId = propertyId,
                    Value = propertyInfo
                };

                itemObject.ItemProperties.Add(itemProperty);
                dbContextScope.SaveChanges();
            }
        }

        public void RemovePropertyFromItem(int itemId, int propertyId)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var itemObject = _ItemRepository.GetItem(itemId);
                var propertyObject = itemObject.ItemProperties.SingleOrDefault(x => x.PropertyId == propertyId);

                if (itemObject == null)
                    throw new ArgumentException($"Preke su tokiu Id nebuvo rasta");
                
                itemObject.ItemProperties.Remove(propertyObject);
                dbContextScope.SaveChanges();
            }
        }

        private bool CheckPictureExtension(string url)
        {
            return Regex.IsMatch(url, @"(\.png|\.jpg|\.jpeg|\.gif){1}$");
        }

        // items Location represents file location in the projects folder, images location is the same, the httpbased file is the attached excel file
        public async Task StartImportingItems(string itemsLocation, HttpPostedFileBase file, string imagesLocation)
        {
            await Task.Run(() =>
            {
                var itemsFileObject = _FileControl.LoadShopItemsFile(itemsLocation, file);
                var allItems = _ImportControl.ImportItemsFromFile(Path.Combine(itemsLocation, itemsFileObject));
                var allCategories = _ItemCategoryControl.GetAllCategories();

                for (int i = 0; i < allItems.Count; i++)
                {
                    if (allItems[i].Name == null)
                        continue;
                    if (allItems[i].Headline == null)
                        continue;
                    if (allItems[i].SKUCode == null)
                        continue;
                    if (allItems[i].Price <= 0)
                        continue;

                    var itemPropertiesSet = new HashSet<ItemProperty>();
                    var properties = allItems[i].ItemProperties.ToList();

                    Category category = null;
                    if (!(string.IsNullOrWhiteSpace(allItems[i].Category.Name) || string.IsNullOrEmpty(allItems[i].Category.Name)) &&
                        (category = allCategories.FirstOrDefault(c => c.Name == allItems[i].Category.Name)) == null)
                    {

                        var itemProperties = new List<int>();
                        foreach (var property in properties)
                        {
                            Property propertiesInDatabase;
                            if (!(string.IsNullOrEmpty(property.Property.Name) || string.IsNullOrWhiteSpace(property.Property.Name)))
                            {
                                if ((propertiesInDatabase = _IPropertyControl.GetProperty(property.Property.Name)) == null)
                                {
                                    _IPropertyControl.CreateProperty(property.Property.Name);
                                    propertiesInDatabase = _IPropertyControl.GetProperty(property.Property.Name);
                                }
                                itemProperties.Add(propertiesInDatabase.Id);
                            }
                        }

                        _ItemCategoryControl.CreateCategory(allItems[i].Category.Name, itemProperties);
                        allCategories = _ItemCategoryControl.GetAllCategories();
                        category = allCategories.FirstOrDefault(c => c.Name == allItems[i].Category.Name);
                        allItems[i].Category = null;
                    }

                    allItems[i].Category = null;
                    allItems[i].CategoryId = category.Id;

                    var categoryProperties = category.Properties;

                    foreach (var categoryProperty in categoryProperties)
                    {
                        var addedProperty = properties.FirstOrDefault(x => x.Property.Name == categoryProperty.Name);
                        if (addedProperty == null)
                        {
                            addedProperty = new ItemProperty();
                        }
                        itemPropertiesSet.Add(new ItemProperty() { PropertyId = categoryProperty.Id, Value = addedProperty.Value });
                    }

                    allItems[i].ItemProperties = itemPropertiesSet;

                    try
                    {
                        using (var dbContextScope = _dbContextScopeFactory.Create())
                        {
                            Item foundItemObject = _ItemRepository.GetItem(allItems[i].Id);
                            if (foundItemObject != null)
                                throw new Exception("This item already exisst");
                            try
                            {
                                if (String.IsNullOrEmpty(allItems[i].ImageUrl) && allItems[i].Image != null)
                                {
                                    string[] itemsFileRepository = imagesLocation.Split(Path.DirectorySeparatorChar);
                                    string repository = Path.DirectorySeparatorChar + Path.Combine(itemsFileRepository[itemsFileRepository.Length - 2], Path.Combine(itemsFileRepository[itemsFileRepository.Length - 1], _FileControl.LoadShopItemsFile(imagesLocation, allItems[i].Image)));
                                    if (!CheckPictureExtension(repository))
                                    {
                                        throw new Exception("Picture extension is not supported by the system");
                                    }
                                    allItems[i].ImageUrl = repository;
                                }
                                else if ((!String.IsNullOrEmpty(allItems[i].ImageUrl) && allItems[i].Image == null) ||
                                    (!String.IsNullOrEmpty(allItems[i].ImageUrl) && allItems[i].Image != null))
                                {
                                    if (!CheckPictureExtension(allItems[i].ImageUrl))
                                    {
                                        throw new Exception("Picture extension is not supported by the system");
                                    }
                                }
                                else
                                    allItems[i].ImageUrl = null;                      
                            }
                            catch
                            {
                                throw;
                            }
                            _ItemRepository.CreateItem(allItems[i]);
                            dbContextScope.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception("The following line describes whats wrong " + e.Message);
                    }

                }
            });
        }

        public async Task ExportItemsTask(IEnumerable<Item> items, string itemsRepository)
        {
            await Task.Run(() =>
            {
                var exportRepository = _ImportControl.ExportItemsToFile(items, itemsRepository);
            });
        }
    }
}
