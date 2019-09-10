using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessObjects.PrekesInfo;

namespace BusinessObjects.SavybesInfo
{
    public class ItemProperty
    {
        [Key, Column(Order = 0)] // pagal preke
        public int PrekesId { get; set; }
        [Key, Column(Order = 1)] // pagal savybe
        public int SavybesId { get; set; }
        public string SavybesInformacija { get; set; }
        public virtual Preke Preke { get; set; } // Susietos prekes ir Savybes lenteles
        public virtual Savybe Savybe { get; set; }
    }
}