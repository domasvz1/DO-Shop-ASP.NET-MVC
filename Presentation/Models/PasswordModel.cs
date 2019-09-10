using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
// Used modules and interfaces in the project
using BusinessObjects;


namespace Presentation.Models
{
    public class PasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Senas slaptažodis")]
        public string OldPassword { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Naujas slaptažodis")]
        public string NewPassword { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords don't match")]
        [DisplayName("Pakartokite savo naują slaptažodį")]
        public string RepeatedNewPassword { get; set; }


        public bool DoPasswordsMatch()
        {
            return OldPassword != NewPassword;
        }

        public void ExcryptThePassword()
        {
            NewPassword = Encryption.EncryptPassword(NewPassword);
        }
    }
}