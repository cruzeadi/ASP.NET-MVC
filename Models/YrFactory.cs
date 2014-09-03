using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WeatherApplication.Models
{
    public class YrFactory
    {
        public static Weather Create(XElement status, int locationID = 0)
        {

            return new Weather
            {              
               
                Temperature = status.Element("temperature").Attribute("value").Value,
                Symbol = status.Element("symbol").Attribute("number").Value,
                Time = DateTime.ParseExact(status.Attribute("from").Value, "yyyy-MM-ddTHH:mm:ss", null),
                LocationID = locationID

           };
        }

    
    }
}