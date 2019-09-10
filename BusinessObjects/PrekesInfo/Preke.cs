using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BusinessObjects.SavybesInfo;
using BusinessObjects.KategorijosInfo;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessObjects.Uzsakymai;

namespace BusinessObjects.PrekesInfo
{
    public class Preke
    {

        public Preke()
        {
            ItemProperties = new List<ItemProperty>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DisplayName("Prekės Pavadinimas")]
        public string Pavadinimas { get; set; }
        [Required]
        [DisplayName("Trumpas Aprašymas")]
        public string Trumpas_Aprasymas { get; set; }
        [Required]
        [DisplayName("SKU")]
        public string SKU{ get; set; }
        public int Kaina { get; set; }
        [DisplayName("Ilgasis Aprašymas")]
        public string Didysis_Aprasymas { get; set; }
        public int? KategorijosId { get; set; }

        [ForeignKey("KategorijosId")]
        public virtual Kategorija Kategorija { get; set; }

        [NotMapped]
        public HttpPostedFileBase Nuotrauka { get; set; }

        public string Nuotraukos_Adresas { get; set; }

        public virtual ICollection<ItemProperty> ItemProperties { get; set; }
    }
}