using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherApplication.Models.Interfaces
{
    interface IWeatherService
    {

        System.Collections.Generic.List<Geoname> FindLocations(string screenName);
        System.Collections.Generic.List<Weather> FindWeathers(Geoname geoname, string screenName, int locationID);
    }
}