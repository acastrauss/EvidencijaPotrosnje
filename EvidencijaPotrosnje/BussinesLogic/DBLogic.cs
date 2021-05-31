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
        private static IDBAccess dBAccess = new DBAccess();

        public static bool IfStateExists(String stateName)
        {
            bool retVal = false;

            try
            {
                retVal = dBAccess.IfStateExistByName(stateName);
            }
            catch (Exception e)
            {

                throw;
            }

            return retVal;
        }

        public static void AddMoreStates(IEnumerable<StateInfoModel> states) 
        {
            foreach (var state in states)
            {
                try
                {
                    dBAccess.AddStates(states);
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }

        public static void AddState(StateInfoModel state)
        {
            try
            {
                dBAccess.AddStates(new List<StateInfoModel>() { state});
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static void AddStateConsumptions(IEnumerable<StateConsumptionModel> stateConsumption)
        {
            try
            {
                dBAccess.AddStateConsumption(stateConsumption);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static void AddStateWeather(IEnumerable<StateWeatherModel> stateWeathers)
        {
            try
            {
                dBAccess.AddStateWeathers(stateWeathers);
            }
            catch (DbEntityValidationException e)
            {

                foreach (var item in e.EntityValidationErrors)
                {

                }
                throw;
            }
        }

        public static void RemoveState(String stateName) 
        {
            try
            {
                dBAccess.RemoveState(stateName);   
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static void RemoveStateTotally(String stateNames) 
        {
            try
            {
                dBAccess.RemoveStateTotally(stateNames);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static void RemoveAllStates() 
        {
            try
            {
                dBAccess.RemoveAllStates();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static void RemoveAllStatesTotally()
        {
            try
            {
                dBAccess.RemoveAllStatesTotally();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static void RemoveStateWeathers(String stateName)
        {
            try
            {
                dBAccess.RemoveStateWeathers(stateName);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static void RemoveStateConsumptions(String stateName)
        {
            try
            {
                dBAccess.RemoveStateConsumption(stateName);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static void RemoveStateWeathersByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            try
            {
                dBAccess.RemoveStateWeathersByDate(startDate, endDate, stateName);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static void RemoveStateConsumptionsByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            try
            {
                dBAccess.RemoveStateConsumptionsByDate(startDate, endDate, stateName);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static void RemoveStateByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            try
            {
                dBAccess.RemoveStateByDate(startDate, endDate, stateName);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static IEnumerable<StateInfoModel> GetAllStates() 
        {
            // no need for try catch since there is no exception
            return dBAccess.GetAllStates();
        }

        public static StateInfoModel GetStateByName(string name) 
        {
            StateInfoModel retVal = StateInfoModel.NotValid();

            try
            {
                retVal = dBAccess.GetStateByName(name);
            }
            catch (Exception e)
            {
                throw;
            }

            return retVal;
        }

        public static IEnumerable<StateConsumptionModel> GetStateConsumptionByName(string stateName) 
        {
            List<StateConsumptionModel> retVal = new List<StateConsumptionModel>();

            try
            {
                retVal = dBAccess.GetStateConsumptionByStateName(stateName).ToList();
            }
            catch (Exception e)
            {

                throw;
            }

            return retVal;
        }

        public IEnumerable<StateWeatherModel> GetStateWeatherByStateName(String name)
        {
            List<StateWeatherModel> retVal = new List<StateWeatherModel>();

            try
            {
                dBAccess.GetStateWeatherByStateName(name);
            }
            catch (Exception e)
            {

                throw;
            }

            return retVal;
        }

        public StateInfoModel GetStateByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            StateInfoModel retVal = new StateInfoModel();

            try
            {
                dBAccess.GetStateByDate(startDate, endDate, stateName);
            }
            catch (Exception e)
            {

                throw;
            }

            return retVal;
        }
        
        public IEnumerable<StateConsumptionModel> GetStateConsumptionsByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            List<StateConsumptionModel> retVal = new List<StateConsumptionModel>();

            try
            {
                retVal = dBAccess.GetStateConsumptionsByDate(startDate, endDate, stateName).ToList();
            }
            catch (Exception e)
            {

                throw;
            }

            return retVal;
        }
        
        public IEnumerable<StateWeatherModel> GetStateWeathersByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            List<StateWeatherModel> retVal = new List<StateWeatherModel>();

            try
            {
                retVal = dBAccess.GetStateWeathersByDate(startDate, endDate, stateName).ToList();
            }
            catch (Exception e)
            {

                throw;
            }

            return retVal;
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
