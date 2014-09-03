using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherApplication.Models.Interfaces
{
    public interface IWeatherRepository
    {
        void Add(Weather Weather);
        void Add(Geoname geoname);
        void Delete(Weather Weather);
        void Delete(Geoname geoname);
        void Update(Weather Weather);
        void Update(Geoname geoname);
        List<Geoname> FindLocations();
        List<Weather> FindWeathers();
        Geoname FindLocation(int locationID);
        Weather FindWeather(int locationID);
      
        IQueryable<Geoname> QueryLocation();
        IQueryable<Weather> QueryWeather();


       
        
        void Save();
    }
}
