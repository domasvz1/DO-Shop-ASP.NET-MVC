using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class Category
    {
        public Category()
        {
            Properties = new HashSet<Property>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Category Name")]
        public string Name { get; set; }
        public List<Item> Items { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}