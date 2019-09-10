using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessObjects.SavybesInfo;
using BusinessObjects.PrekesInfo;


namespace BusinessObjects.KategorijosInfo
{
    public class Kategorija
    {
        public Kategorija()
        {
            Savybess = new HashSet<Savybe>(); // Kategorija turi savybe
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string KategorijosPavadinimas { get; set; }
        public List<Preke> Prekess { get; set; }
        public virtual ICollection<Savybe> Savybess { get; set; }
    }
}