using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class ItemProperty
    {
        [Key, Column(Order = 0)]
        public int ItemId { get; set; }
        [Key, Column(Order = 1)]
        public int PropertyId { get; set; }
        public string Value { get; set; }
        public virtual Item Item { get; set; }
        public virtual Property Property { get; set; }
    }
}