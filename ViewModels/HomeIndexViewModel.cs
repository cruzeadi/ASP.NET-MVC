using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using WeatherApplication.Models;

namespace WeatherApplication.ViewModels
{
    public class HomeIndexViewModel
    {
        [DisplayName("City")]
        [Required(ErrorMessage="Du måste ange en stad")]
        [RegularExpression(@"^[a-zA-Z  ÅÄÖåäöÆØ]+$", ErrorMessage = "Ange en stad med bokstäver.")]
        [MaxLength(50)]
        [MinLength(1)]
        public string ScreenName { get; set; }

        public List<Weather> Weathers { get; set; }
        public List<Geoname> Geoname { get; set; }

        public string Name
        {
            get { return Weathers != null && Weathers.Any() ? Weathers[0].Geoname.Name : ScreenName; }
        }

    }
}