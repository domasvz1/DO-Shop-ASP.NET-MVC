using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessObjects;

namespace Presentation.Models
{
    public class CommonViewModel
    {
        public CityModel CityVM { get; set; }
        public Client ClientVM { get; set; }
    }
}