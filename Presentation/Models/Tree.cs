using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.WebPages;

namespace Presentation.Models
{
    public static class TreeViewHelper
    {
        /// <summary>
        /// Create an HTML tree from a recursive collection of items
        /// </summary>
        public static TreeView TreeView(this HtmlHelper html, IEnumerable<Category> categories, IEnumerable<Item> itemsWithNoCategory)
        {
            return new TreeView(html, categories, itemsWithNoCategory);
        }
    }

    /// <summary>
    /// Create an HTML tree from a resursive collection of items
    /// </summary>
    public class TreeView : IHtmlString
    {
        private readonly HtmlHelper _html;
        private readonly IEnumerable<Category> _categories = Enumerable.Empty<Category>();
        private Func<Category, string> _categoryDisplayProperty = item => item.Name.ToString();
        private Func<Item, string> _itemDisplayProperty = item => item.Name.ToString();
        private Func<Category, IEnumerable<Item>> _childrenProperty;
        private string _emptyContent = "No children";
        private IDictionary<string, object> _htmlAttributes = new Dictionary<string, object>();
        private IDictionary<string, object> _childHtmlAttributes = new Dictionary<string, object>();
        private Func<Category, HelperResult> _categoryTemplate;
        private Func<Item, HelperResult> _itemTemplate;
        private Func<string, HelperResult> _rootTemplate;
        private IEnumerable<Item> _itemsWithNoCategory;

        public TreeView(HtmlHelper html, IEnumerable<Category> categories, IEnumerable<Item> itemsWithNoCategory)
        {
            if (html == null) throw new ArgumentNullException("html");
            _html = html;
            _categories = categories;
            _itemsWithNoCategory = itemsWithNoCategory;
            // The ItemTemplate will fdeault to rendering the DisplayProperty
            _categoryTemplate = item => new HelperResult(writer => writer.Write(_categoryDisplayProperty(item)));
            _itemTemplate = item => new HelperResult(writer => writer.Write(_itemDisplayProperty(item)));
        }


        /// <summary>
        /// The template used to render each item in the tree view
        /// </summary>
        public TreeView CategoryTemplate(Func<Category, HelperResult> itemTemplate)
        {
            if (itemTemplate == null) throw new ArgumentNullException("itemTemplate");
            _categoryTemplate = itemTemplate;
            return this;
        }

        /// <summary>
        /// The template used to render each item in the tree view
        /// </summary>
        public TreeView ItemTemplate(Func<Item, HelperResult> itemTemplate)
        {
            if (itemTemplate == null) throw new ArgumentNullException("itemTemplate");
            _itemTemplate = itemTemplate;
            return this;
        }

        public TreeView RootTemplate(Func<string, HelperResult> rootTemplate)
        {
            if (rootTemplate == null) throw new ArgumentNullException("rootTemplate");
            _rootTemplate = rootTemplate;
            return this;
        }


        /// <summary>
        /// The property which returns the children items
        /// </summary>
        public TreeView Children(Func<Category, IEnumerable<Item>> selector)
        {
            //  if (selector == null) //throw new ArgumentNullException("selector");
            _childrenProperty = selector;
            return this;
        }

        /// <summary>
        /// Content displayed if the list is empty
        /// </summary>
        public TreeView EmptyContent(string emptyContent)
        {
            if (emptyContent == null) throw new ArgumentNullException("emptyContent");
            _emptyContent = emptyContent;
            return this;
        }

        /// <summary>
        /// HTML attributes appended to the root ul node
        /// </summary>
        public TreeView HtmlAttributes(object htmlAttributes)
        {
            HtmlAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return this;
        }

        /// <summary>
        /// HTML attributes appended to the root ul node
        /// </summary>
        public TreeView HtmlAttributes(IDictionary<string, object> htmlAttributes)
        {
            if (htmlAttributes == null) throw new ArgumentNullException("htmlAttributes");
            _htmlAttributes = htmlAttributes;
            return this;
        }

        /// <summary>
        /// HTML attributes appended to the children items
        /// </summary>
        public TreeView ChildrenHtmlAttributes(object htmlAttributes)
        {
            ChildrenHtmlAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            return this;
        }

        /// <summary>
        /// HTML attributes appended to the children items
        /// </summary>
        public TreeView ChildrenHtmlAttributes(IDictionary<string, object> htmlAttributes)
        {
            if (htmlAttributes == null) throw new ArgumentNullException("htmlAttributes");
            _childHtmlAttributes = htmlAttributes;
            return this;
        }


        /// <summary>
        /// The property which will display the text rendered for each item
        /// </summary>
        public TreeView ItemText()
        {
            return this;
        }

        public string ToHtmlString()
        {
            return ToString();
        }

        public void Render()
        {
            var writer = _html.ViewContext.Writer;
            using (var textWriter = new HtmlTextWriter(writer))
            {
                textWriter.Write(ToString());
            }
        }

        private void ValidateSettings()
        {
            if (_childrenProperty == null)
            {
                return;
            }
        }


        public override string ToString()
        {
            ValidateSettings();
            var listItems = new List<Category>();
            var noCategoryItems = new List<Item>();
            if (_categories != null)
            {
                listItems = _categories.ToList();
            }

            if (_itemsWithNoCategory != null)
            {
                noCategoryItems = _itemsWithNoCategory.ToList();
            }


            var ul = new TagBuilder("ul");
            ul.MergeAttributes(_htmlAttributes);
            var li = new TagBuilder("li")
            {
                InnerHtml = _rootTemplate(_emptyContent).ToHtmlString()
            };
            li.MergeAttribute("id", "-1");

            if (listItems.Count > 0 || noCategoryItems.Count > 0)
            {
                var innerUl = new TagBuilder("ul");
                innerUl.MergeAttributes(_childHtmlAttributes);

                foreach (var item in listItems)
                {
                    BuildNestedTag(innerUl, item, _childrenProperty);
                }

                BuildNestedTag(innerUl, new Category() { Name = "Prekės be kategorijos", Items = noCategoryItems, Id = -2 }, _childrenProperty);
                li.InnerHtml += innerUl.ToString();
            }
            ul.InnerHtml += li.ToString();

            return ul.ToString();
        }

        private void AppendChildren(TagBuilder parentTag, Category parentItem, Func<Category, IEnumerable<Item>> childrenProperty)
        {
            if (childrenProperty == null)
            {
                return;
            }
            var children = childrenProperty(parentItem).ToList();
            if (!children.Any())
            {
                return;
            }

            var innerUl = new TagBuilder("ul");
            innerUl.MergeAttributes(_childHtmlAttributes);

            foreach (var item in children)
            {
                var li = GetLi(item);
                innerUl.InnerHtml += li.ToString(TagRenderMode.StartTag);
                innerUl.InnerHtml += li.InnerHtml + li.ToString(TagRenderMode.EndTag);
            }

            parentTag.InnerHtml += innerUl.ToString();
        }

        private void BuildNestedTag(TagBuilder parentTag, Category parentItem, Func<Category, IEnumerable<Item>> childrenProperty)
        {
            var li = GetLi(parentItem);
            parentTag.InnerHtml += li.ToString(TagRenderMode.StartTag);
            AppendChildren(li, parentItem, childrenProperty);
            parentTag.InnerHtml += li.InnerHtml + li.ToString(TagRenderMode.EndTag);
        }

        private TagBuilder GetLi(Category category)
        {
            var li = new TagBuilder("li")
            {
                InnerHtml = _categoryTemplate(category).ToHtmlString()
            };
            Type myType = category.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name.ToLower() == "id")
                    li.MergeAttribute("categoryId", prop.GetValue(category, null).ToString());
                // Do something with propValue
                if (prop.Name.ToLower() == "sortorder")
                    li.MergeAttribute("priority", prop.GetValue(category, null).ToString());
            }
            return li;
        }

        private TagBuilder GetLi(Item item)
        {
            var li = new TagBuilder("li")
            {
                InnerHtml = _itemTemplate(item).ToHtmlString()
            };
            Type myType = item.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name.ToLower() == "id")
                    li.MergeAttribute("itemId", prop.GetValue(item, null).ToString());
                // Do something with propValue
                if (prop.Name.ToLower() == "sortorder")
                    li.MergeAttribute("priority", prop.GetValue(item, null).ToString());
            }
            return li;
        }
    }
}