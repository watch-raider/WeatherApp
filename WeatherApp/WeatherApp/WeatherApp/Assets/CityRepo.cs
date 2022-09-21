using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Assets
{
    /// <summary>
    /// Repo used for CRUD methods for City DB Table 
    /// </summary>
    public  class CityRepo
    {
        public string StatusMessage { get; set; }
        public string ErrorMessage { get; private set; }
        public static SQLiteConnection conn { get; set; }
        public static CityRepo instance { get; set; }

        public CityRepo(string dbPath)
        {
            // Initialize a new SQLiteConnection
            conn = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex, true);
            // Create the CollectionData table
            try
            {
                conn.CreateTable<City>();
            }
            catch (Exception e)
            {
                conn.DropTable<City>();
                conn.CreateTable<City>();
                Debug.WriteLine(e.Message);
            }
        }

        // Static variable instialised to give access to class methods
        public static void Initialise(string filename)
        {
            instance = new CityRepo(filename);
        }

        public async Task<int> Add(City city)
        {
            int result = 0;
            ErrorMessage = string.Empty;
            try
            {
                //basic validation to ensure an id was entered
                if (city.Id == 0)
                    throw new Exception("Valid collection required");

                // insert a new collection into the CollectionData table
                result = conn.Insert(city);
                await Task.Delay(100);

                StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, city.Id);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("Failed to add {0}. Error: {1}", city.Id, ex.Message);
            }
            return result;
        }

        public async Task<int> AddAll(IEnumerable<City> cities)
        {
            int result = 0;
            ErrorMessage = string.Empty;
            try
            {
                result = conn.InsertAll(cities);
                await Task.Delay(100);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("Database Error. {0}", ex.Message);
            }
            return result;
        }

        public async Task<int> Update(City city)
        {
            int result = 0;
            ErrorMessage = string.Empty;
            try
            {
                //basic validation to ensure an id was entered
                if (city.Id == 0)
                    throw new Exception("Valid collection required");

                // insert a new collection into the CollectionData table
                result = conn.Update(city);
                await Task.Delay(100);

                StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, city.Id);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("Failed to add {0}. Error: {1}", city.Id, ex.Message);
            }
            return result;
        }

        public async Task<City> Get(long cityId)
        {
            ErrorMessage = string.Empty;
            // Retrieves a single collection from the list based on the id provided
            try
            {
                City cityData = conn.Table<City>().Single(c => c.Id == cityId);
                await Task.Delay(100);
                return cityData;
            }
            catch (Exception e)
            {
                ErrorMessage = string.Format("Database Error. {0}", e.Message);
                return null;
            }
        }

        public async Task<IEnumerable<City>> GetAll()
        {
            ErrorMessage = string.Empty;
            //return a list of collections saved from the Collection table in the database
            try
            {
                IEnumerable<City> collections = conn.Table<City>().ToList();
                await Task.Delay(100);
                return collections;
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("Database Error. {0}", ex.Message);
            }

            return Enumerable.Empty<City>();
        }

        public async Task<int> Delete(City city)
        {
            int result = 0;
            ErrorMessage = string.Empty;
            // Deletes the collection with the specified id, in the CollectionData table 
            try
            {
                result = conn.Delete(city);
                await Task.Delay(100);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("Database Error. {0}", ex.Message);
            }
            return result;
        }

        public async Task<int> DeleteAll()
        {
            int result = 0;
            ErrorMessage = string.Empty;
            //deletes all the collections saved in the CollectionData table in the database
            try
            {
                result = conn.DeleteAll(new TableMapping(typeof(City)));
                await Task.Delay(100);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("Database Error. {0}", ex.Message);
            }
            return result;
        }

        public async Task<int> DropTable()
        {
            int result = 0;
            ErrorMessage = string.Empty;
            try
            {
                result = conn.DropTable<City>();
                await Task.Delay(100);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("Database Error. {0}", ex.Message);
            }
            return result;
        }

        public async Task<CreateTableResult> CreateTable()
        {
            CreateTableResult result = CreateTableResult.Created;
            ErrorMessage = string.Empty;
            try
            {
                result = conn.CreateTable<City>();
                await Task.Delay(100);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("Database Error. {0}", ex.Message);
            }
            return result;
        }
    }
}
