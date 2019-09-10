using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Your login")]
        public string Login { get; set; }
        [DisplayName("Your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsCorrectPassword(string enteredPassword)
        {
            // admin password is not encrypted, needs to change logic temporary
            //if (Encryption.EncryptPassword(ivestasSlaptazodis).ToLower() == Password.ToLower())

            if (enteredPassword == Password)
                return true;
            else
                return false;
        }
    }
}