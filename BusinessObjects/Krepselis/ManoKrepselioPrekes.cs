using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessObjects.PrekesInfo;

namespace BusinessObjects.Krepselis
{
    public class ManoKrepselioPrekes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Kiekis { get; set; }
        public int KrepselioKaina { get; set; }
        public ManoKrepselis ManoKrepselis { get; set; }
        public Preke Preke { get; set; }
    }
}