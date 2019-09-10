using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// Used modules and Interfacesfrom the project
using BusinessObjects.Orders;

// Standart email regex expression was taken from
// https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format

namespace BusinessObjects
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", ErrorMessage = "Iveskite galiojanti elketronini pasto adresa")]
        [DisplayName("Enter Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Enter Password")]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Enter Last Name")]
        public string LastName { get; set; }
        public bool IsBlocked { get; set; }
        [DataType(DataType.Password)]
        [NotMapped]
        [DisplayName("Pakartokite slaptažodį")]
        public string SecondPassword { get; set; }

        // Kortele reiketu isimti is prisijungimo
        [UIHint("Card")]
        public Card Card { get; set; }

        public DeliveryAddress DeliveryAddress { get; set; }

        public ICollection<ClientOrders> ClientOrders { get; set; }


        public bool ConfirmClientPasswordsMatch()
        {
            return Password == SecondPassword;
        }

        public void InitializeClientPasswordEncryption()
        {
            Password = Encryption.EncryptPassword(Password);
        }

        public bool DoEncyptedPassowrdsMacth(string receivedPassword)
        {
            string hmm = Encryption.EncryptPassword(receivedPassword).ToLower();
            if ( hmm == Password.ToLower() )
                return true;
            else
                return false;
        }
    }
}
