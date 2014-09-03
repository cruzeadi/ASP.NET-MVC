using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherApplication.Models.Interfaces;

namespace WeatherApplication.Models
{
    public class WeatherService : IWeatherService
    {

        private IWeatherRepository _repository;

        public WeatherService()
            : this(new WeatherRepository())
        {

        }

        public WeatherService(IWeatherRepository repository)
        {
            this._repository = repository;

        }

        public List<Geoname> FindLocations(string screenName)
        {

            var webservice = new GeonameWebservice();

            var locations = this._repository
            .QueryLocation()
            .Where(l => l.Name == screenName)
            .ToList();

             //Om den inte finns.
            if (!locations.Any())
            {
                
                locations = webservice.FindLocations(screenName);

                //Sparar
                locations.ForEach(l => this._repository.Add(l));
                this._repository.Save();
            }

            //Om man behöver uppdatera geonames.

            else if (locations.Any() || locations.Select(u => u.NextUpdate).First() < DateTime.Now)
            {
              
                locations.ForEach(l => l.NextUpdate =  DateTime.Now.AddMinutes(1));
                locations.ForEach(l => this._repository.Update(l));
           
            }

            return locations;
        }


        public List<Weather> FindWeathers(Geoname geoname, string screenName, int locationID)
        {

            
           // Gets weather forecasts
             var weathers = this._repository.QueryWeather().Where(t => t.LocationID == locationID).OrderBy(w => w.Time).ToList();

            // Checks if there are any weather forecasts saved in db and if datetime hasn't occured
            if (!weathers.Any())
            {
               
                  // ...aktivates web service if no post with city exists
                var webservice = new GeonameWebservice();
                weathers = webservice.FindWeathers(geoname, screenName);

                // for every object sets FK from parameter int.
                weathers.ForEach(t => t.LocationID = locationID);

                // ...update the location and save the weather in the database.
                geoname.NextUpdate = DateTime.Now.AddMinutes(10);
                weathers.ForEach(t => this._repository.Add(t));
                //weathers.ForEach(t => this._repository.Update(t));
                this._repository.Save();
            }
               else
            {
                    // ...delete the old(?) weathers (if there are any),...
                    weathers.ForEach(t => this._repository.Delete(t));

                    var webservice = new GeonameWebservice();
                    weathers = webservice.FindWeathers(geoname, screenName);

                    weathers.ForEach(t => t.LocationID = locationID);
                    // ...update the user and save the weathers in the database.
                    geoname.NextUpdate = DateTime.Now.AddMinutes(10);
                    weathers.ForEach(t => this._repository.Add(t));
                    
                    this._repository.Save();
            }
            
                return weathers;
        }
    }
}
