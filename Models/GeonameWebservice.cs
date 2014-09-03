using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Xml.Linq;

namespace WeatherApplication.Models
{
    public class GeonameWebservice
    {
        public List<Geoname> FindLocations(string screenName)
        {
           

            var requestUrlstring = String.Format("http://api.geonames.org/search?name_equals={0}&username=ulfdags&featureClass=P&style=full", screenName);

            var document = LoadDocument(requestUrlstring);

            return document.Descendants("geoname")
                .Take(1)
                .Select(status => GeonameFactory.Create(status))
                .ToList();
        }

        public List<Weather> FindWeathers(Geoname geoname, string screenName)
        {
           

            var requestUrlstring = String.Format("http://www.yr.no/place/{0}/{1}/{2}/forecast.xml", geoname.Country, geoname.County, geoname.Name);

            var document = LoadDocument(requestUrlstring);

           return (from time in document.Descendants("time") // Väljer ut alla time noder.
                    where Int32.Parse(time.Attribute("period").Value) >= 2 // Där periodens värde är större eller lika med 2.
                    group time by DateTime.Parse(time.Attribute("from").Value).Date into g // Gruppera perioderna per dag. Välj sedan ut den första i varje par.
                    select (from t in g select t).First()).Take(5).Select(forecast => YrFactory.Create(forecast, geoname.LocationID)).ToList();
        }
        
        private XDocument LoadDocument(string requestUriString)
        {
            // Initialize a new WebRequest instance for the GeoNames Search Webservice.
            var request = WebRequest.Create(requestUriString);

            // Get the response from the web service.
            using (var response = request.GetResponse())
            {
                // Parse the response.
                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    // Load the response into an XML document.
                    return XDocument.Load(stream);
                }
            }
        }
    }

}