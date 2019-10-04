using System.Web.Mvc;
using BusinessObjects;
using System.ComponentModel.DataAnnotations;


namespace Presentation.Models
{
    public class ClientModel
    { 
        public Client ClientVM { get; set; }
        // ---------
        [Required(ErrorMessage = "You have not selected delivery city")]
        [Display(Name = "City")]
        public int? SelectedCity { get; set; }
        //---
        [Required(ErrorMessage = "You have not selected delivery city")]
        [Display(Name = "Locality")]
        public int? SelectedLocality { get; set; }
        //---
        public SelectList CityList { get; set; }
        public SelectList LocalityList { get; set; }
    }
}