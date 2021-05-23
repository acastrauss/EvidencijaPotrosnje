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
                    DBAccess.AddOrUpdateState(state);
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
                DBAccess.AddOrUpdateState(state);
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
                DBAccess.RemoveState(state);   
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
                var state = DBAccess.GetStateByName(name);
                DBAccess.RemoveState(state);
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
                    DBAccess.RemoveState(state);
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
                    var state = DBAccess.GetStateByName(name);
                    DBAccess.RemoveState(state);
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
                DBAccess.RemoveAllStates();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public IEnumerable<StateInfoModel> GetAllStates() 
        {
            // no need for try catch since there is no exception
            return DBAccess.GetAllStates();
        }

        public static StateInfoModel GetStateByName(string name) 
        {
            StateInfoModel ret_val = StateInfoModel.NotValid();

            try
            {
                ret_val = DBAccess.GetStateByName(name);
            }
            catch (Exception e)
            {
                
                throw;
            }

            return ret_val;
        }

        public static StateConsumptionModel GetStateConsumptionByName(string name) 
        {
            StateConsumptionModel ret_val = StateConsumptionModel.NotValid();

            try
            {
                ret_val = DBAccess.GetStateConsumptionByStateName(name);
            }
            catch (Exception e)
            {

                throw;
            }

            return ret_val;
        }
    
        public static StateWeatherModel GetWeatherModelByName(string name) 
        {
            StateWeatherModel retVal = StateWeatherModel.NotValid();

            try
            {
                retVal = DBAccess.GetStateWeatherByStateName(name);
            }
            catch (Exception e)
            {

                throw;
            }

            return retVal;
        }
    
        // remove
        public static Dictionary<DataKeys, StateInfoModel> GetStatesForDate(DateTime startDate, DateTime endDate) 
        {
            var retVal = new Dictionary<DataKeys, StateInfoModel>();

            try
            {
                DBAccess.GetStatesForDate(startDate, endDate);
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
                retVal = DBAccess.GetFullStateName(shortStateName);
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
                retVal = DBAccess.GetShortStateName(fullStateName);
            }
            catch (Exception e)
            {

                throw;
            }

            return retVal;
        }
    }
}
