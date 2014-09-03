using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherApplication.Models.Interfaces;

namespace WeatherApplication.Models.AbstractClasses
{
    public abstract class WeatherRepositoryBase : IWeatherRepository
    {

        #region  IWeatherRepository Members

        public abstract void Add(Weather Weather);
        public abstract void Add(Geoname geoname);
        public abstract void Delete(Weather Weather);
        public abstract void Delete(Geoname geoname);
        public abstract void Update(Weather Weather);
        public abstract void Update(Geoname geoname);

        public List<Geoname> FindLocations()
        {
            return this.QueryLocation().ToList();
        }
        public List<Weather> FindWeathers()
        {
            return this.QueryWeather().ToList();
        }
       
        
        public Geoname FindLocation(int locationID)
        {
            return this.QueryLocation().SingleOrDefault(l => l.LocationID == locationID);
        }
        public Weather FindWeather(int weatherID)
        {
            return this.QueryWeather().SingleOrDefault(w => w.WeatherID == weatherID);
        }
     
        public abstract IQueryable<Geoname> QueryLocation();

        public abstract IQueryable<Weather> QueryWeather();


        public abstract void Save();


        #endregion
    }
}