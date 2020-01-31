using System.Web;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class Item
    {
        public Item()
        {
            ItemProperties = new List<ItemProperty>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Item Id")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Item Name: ")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Headline: ")]
        [DataType(DataType.MultilineText)]
        public string Headline { get; set; }

        [Required]
        [DisplayName("Item SKU: ")]
        public string SKUCode { get; set; }

        //TODO maybe should be better to change from int to unsigned int?
        [Range(1, int.MaxValue, ErrorMessage = "Item price must be positive!")]
        [DisplayName("Item Price: ")]
        public int Price { get; set; }

        [DisplayName("Description: ")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DisplayName("category Id: ")]
        public int? CategoryId { get; set; }

        [DisplayName("Category: ")]
        public virtual Category Category { get; set; }

        [NotMapped]
        public HttpPostedFileBase Image { get; set; }

        [DisplayName("Nuotraukos Url")]
        public string ImageUrl { get; set; }

        public virtual ICollection<ItemProperty> ItemProperties { get; set; }
    }
}