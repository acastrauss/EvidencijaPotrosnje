using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels;
using DatabaseAccess;
using SharedModels.HelperClasses;
using System.Data.Entity.Validation;

namespace BussinesLogic
{
    /// <summary>
    /// Handling database logic
    /// </summary>
    public class DBLogic
    {
        public static void AddOrUpdateMoreStates(IEnumerable<StateInfoModel> states) 
        {
            foreach (var state in states)
            {
                try
                {
                    (new DBAccess()).AddState(state);
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }

        public static void AddOrUpdateState(StateInfoModel state)
        {
            try
            {
                (new DBAccess()).AddState(state);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    
        public static void RemoveState(StateInfoModel state) 
        {
            try
            {
                (new DBAccess()).RemoveState(state);   
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static void RemoveStateByName(string name) 
        {
            try
            {
                var dbaccess = new DBAccess();
                var state = dbaccess.GetStateByName(name);
                dbaccess.RemoveState(state);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static void RemoveMoreStates(IEnumerable<StateInfoModel> states) 
        {
            foreach (var state in states)
            {
                try
                {
                    (new DBAccess()).RemoveState(state);
                }
                catch (Exception e)
                {

                    //throw;
                    continue;
                }
            }
        }

        public static void RemoveMoreStatesByName(IEnumerable<String> names)
        {
            foreach (var name in names)
            {
                try
                {
                    var dbaccess = new DBAccess();
                    var state = dbaccess.GetStateByName(name);
                    dbaccess.RemoveState(state);
                }
                catch (Exception e)
                {

                    //throw;
                    continue;
                }
            }
        }

        public static void RemoveAllStates() 
        {
            try
            {
                (new DBAccess()).RemoveAllStates();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public IEnumerable<StateInfoModel> GetAllStates() 
        {
            // no need for try catch since there is no exception
            return (new DBAccess()).GetAllStates();
        }

        public static StateInfoModel GetStateByName(string name) 
        {
            StateInfoModel ret_val = StateInfoModel.NotValid();

            try
            {
                ret_val = (new DBAccess()).GetStateByName(name);
            }
            catch (Exception e)
            {
                
                throw;
            }

            return ret_val;
        }

        public static void AddStateConsumption(StateConsumptionModel stateConsumption)
        {
            try
            {
                (new DBAccess()).AddStateConsumption(stateConsumption);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static StateConsumptionModel GetStateConsumptionByName(string name) 
        {
            StateConsumptionModel ret_val = StateConsumptionModel.NotValid();

            try
            {
                ret_val = (new DBAccess()).GetStateConsumptionByStateName(name);
            }
            catch (Exception e)
            {

                throw;
            }

            return ret_val;
        }

        public static void AddStateWeather(StateWeatherModel stateWeather)
        {
            try
            {
                (new DBAccess()).AddStateWeather(stateWeather);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static void RemoveAllStateWeatherModels()
        {
            try
            {
                (new DBAccess()).RemoveAllStateWeatherModels();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static StateWeatherModel GetWeatherModelByName(string name) 
        {
            StateWeatherModel retVal = StateWeatherModel.NotValid();

            try
            {
                retVal = (new DBAccess()).GetStateWeatherByStateName(name);
            }
            catch (Exception e)
            {

                throw;
            }

            return retVal;
        }
    
        // remove after all have db updated
        public static void AddShortStateNames() 
        {
            var cd = new CountriesDictionary();

            try
            {
                DBAccess.AddShortStateNames(cd.CountriesShort);
            }
            catch (DbEntityValidationException e)
            {
                var erors = e.EntityValidationErrors;

                foreach (var err in erors)
                {
                    var ent = err.Entry;
                    var errorrr = err.ValidationErrors;
                }

                throw;
            }
        }
    
        public static String GetFullStateName(String shortStateName) 
        {
            String retVal = String.Empty;

            try
            {
                retVal = (new DBAccess()).GetFullStateName(shortStateName);
            }
            catch (Exception e)
            {

                throw;
            }

            return retVal;
        }    

        public static String GetShortStateName(String fullStateName) 
        {
            String retVal = String.Empty;

            try
            {
                retVal = (new DBAccess()).GetShortStateName(fullStateName);
            }
            catch (Exception e)
            {

                throw;
            }

            return retVal;
        }
    }
}
