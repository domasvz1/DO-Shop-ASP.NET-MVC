using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessObjects.KategorijosInfo;

namespace BusinessObjects.SavybesInfo
{
    public class Savybe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SavybesPavadinimas { get; set; }
        public ICollection<ItemProperty> ItemProperties { get; set; }
        public virtual ICollection<Kategorija> Kategorijos { get; set; }
    }
}