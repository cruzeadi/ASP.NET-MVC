using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherApplication.Models.AbstractClasses;
using WeatherApplication.Models;
using System.Data;
using System.Data.Objects;

namespace WeatherApplication.Models
{
    public class WeatherRepository: WeatherRepositoryBase
    {
        private WP11_cu222ai_WeatherEntities2 _entities = new WP11_cu222ai_WeatherEntities2();

        public override void Add(Weather yr)
        {
            this._entities.Weathers.AddObject(yr);
        }

        public override void Add(Geoname geoname)
        {
            this._entities.Geonames.AddObject(geoname);
        }

        public override void Delete(Weather yr)
        {
            if (yr.EntityState != EntityState.Deleted)
            {
                this._entities.Weathers.Attach(yr);
            }
            this._entities.Weathers.DeleteObject(yr);
        }

        public override void Delete(Geoname geoname)
        {

          
            if (geoname.EntityState == EntityState.Deleted)
            {
                this._entities.Geonames.Attach(geoname);
            }
          this._entities.Geonames.DeleteObject(geoname);
        }

        public override void Update(Weather yr)
        {
            ObjectStateEntry entry;
            if (!this._entities.ObjectStateManager.TryGetObjectStateEntry(yr, out entry) ||
                entry.State == EntityState.Detached)
            {
                this._entities.Weathers.Attach(yr);
            }

            if (yr.EntityState != EntityState.Modified)
            {
                this._entities.ObjectStateManager.ChangeObjectState(yr, EntityState.Modified);
            }
           
        }

        public override void Update(Geoname geoname)
        {
            ObjectStateEntry entry;
            if (!this._entities.ObjectStateManager.TryGetObjectStateEntry(geoname, out entry) ||
                entry.State == EntityState.Detached)
            {
                this._entities.Geonames.Attach(geoname);
            }

            if (geoname.EntityState != EntityState.Modified)
            {
                this._entities.ObjectStateManager.ChangeObjectState(geoname, EntityState.Modified);
            }
        }

        public override void Save()
        {
            this._entities.SaveChanges();
        }

       public override IQueryable<Geoname> QueryLocation()
        {
            return this._entities.Geonames;
        }

       public override IQueryable<Weather> QueryWeather()
       {
           return this._entities.Weathers;
       }
    }
}