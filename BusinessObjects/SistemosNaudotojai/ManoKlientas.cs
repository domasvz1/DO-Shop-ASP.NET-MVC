using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessObjects.Uzsakymai;
using System.Collections.Generic;

namespace BusinessObjects.SistemosNaudotojai
{
    public class Klientas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3]\.)|(([\w-]+\.)+))([a-zA-Z{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Iveskite galiojanti elketronini pasto adresa")]
        public string ElPastas { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Slaptazodis { get; set; }
        [Required]
        public string Vardas { get; set; }
        [Required]
        public string Pavarde { get; set; }
        public bool Busena { get; set; }
        [DataType(DataType.Password)]
        [NotMapped]
        public string Slaptazodis2 { get; set; }

        public bool ArKlientoSlaptazodziaiSutampa()
        {
            return Slaptazodis == Slaptazodis2;
        }

        //public bool IsCorrectPassword(string unhashedPassword)
        //{
        //    return Password.ToLower() == Encryption.SHA256(unhashedPassword).ToLower();
        //}

        [UIHint("ManoKortele")]
        public ManoKortele ManoKortele { get; set; }
        public PristatymoAdresas PristatymoAdresas { get; set; }
        public ICollection<ManoUzsakymai> ManoUzsakymai { get; set; }
    }
}
