using System.Web.Mvc;
using BusinessObjects;
using System.ComponentModel.DataAnnotations;


namespace Presentation.Models
{
    public class ClientModel
    { 
        public Client ClientVM { get; set; }
        // ---------
        [Display(Name = "Country")]
        public int? SelectedCity { get; set; }
        //---
        [Display(Name = "City")]
        public int? SelectedLocality { get; set; }
        //---
        public SelectList CityList { get; set; }
        public SelectList LocalityList { get; set; }
    }
}