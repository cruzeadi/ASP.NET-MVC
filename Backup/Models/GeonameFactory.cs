using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Globalization;

namespace WeatherApplication.Models
{
    public class GeonameFactory
    {
        public static Geoname Create(XElement status)
        {

            return new Geoname
            {
                Name = status.Element("name").Value,
                Country = status.Element("countryName").Value,
                County = status.Element("adminName1").Value,
                NextUpdate = DateTime.Now
               
             };
        }
    }
}