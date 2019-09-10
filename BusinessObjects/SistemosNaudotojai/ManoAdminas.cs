using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.SistemosNaudotojai
{
    public class ManoAdminas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Prisijungimo_vardas { get; set; }
        public string Slaptazodis { get; set; }
        public bool Busena { get; set; }
        [DataType(DataType.Password)]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        public bool IsConfirmPasswordCorrect()
        {
            return Slaptazodis == ConfirmPassword;
        }
    }
}