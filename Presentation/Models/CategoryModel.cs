using System;
using System.Collections.Generic;

namespace Presentation.Models
{
    public class CategoryModel
    {
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public List<CheckBoxListItem> Properties { get; set; }

        public CategoryModel()
        {
            Properties = new List<CheckBoxListItem>();
        }
    }
}