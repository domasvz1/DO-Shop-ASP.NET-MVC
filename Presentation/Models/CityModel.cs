using BusinessObjects.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Models
{
    public class CityModel
    {
        public City City { get; set; }
        public Locality Locality { get; set; }

        public int? SelectedCity { get; set; }

        public int? SelectedLocality { get; set; }

        public SelectList CityList { get; set; }
        [NotMapped]
        public SelectList LocalityList { get; set; }
    }
}