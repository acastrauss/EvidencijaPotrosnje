using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels;
using DatabaseAccess;
using SharedModels.HelperClasses;
using System.Data.Entity.Validation;

#pragma warning disable CS0659

namespace BussinesLogic
{
    /// <summary>
    /// Handling database logic
    /// </summary>
    public class DBLogic
    {
        private IDBAccess dBAccess = new DBAccess();

        public bool IfStateExists(String stateName)
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

        public void AddState(StateInfoModel state)
        {
            try
            {
                dBAccess.AddStates(new List<StateInfoModel>() { state });
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void AddStateConsumptions(IEnumerable<StateConsumptionModel> stateConsumption, String stateName)
        {
            bool anyNull = false;

            if(stateConsumption != null)
            {
                foreach (var modelSc in stateConsumption)
                {
                    if (modelSc == null)
                    {
                        anyNull = true;
                        break;
                    }
                }
            }

            if (stateConsumption == null || stateName == null || anyNull)
                throw new Exception("Invalid parameteres to add.");

            try
            {
                var maxDate = stateConsumption.Max(x => x.DateShort);
                var minDate = stateConsumption.Min(x => x.DateShort);

                dBAccess.RemoveStateConsumptionsByDate(minDate, maxDate, stateName).Wait();
                dBAccess.AddStateConsumption(stateConsumption, stateName).Wait();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void AddStateWeather(IEnumerable<StateWeatherModel> stateWeathers, String stateName)
        {
            bool anyNull = false;

            if (stateWeathers != null)
            {
                foreach (var modelSc in stateWeathers)
                {
                    if (modelSc == null)
                    {
                        anyNull = true;
                        break;
                    }
                }
            }

            if (stateWeathers == null || stateName == null || anyNull)
                throw new Exception("Invalid parameteres to add.");

            try
            {
                var maxDate = stateWeathers.Max(x => x.LocalTime);
                var minDate = stateWeathers.Min(x => x.LocalTime);

                dBAccess.RemoveStateWeathersByDate(minDate, maxDate, stateName).Wait();
                dBAccess.AddStateWeathers(stateWeathers, stateName).Wait();
            }
            catch (DbEntityValidationException e)
            {
                throw;
            }
        }


        public void RemoveAllStatesTotally()
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

        public void RemoveStateWeathersByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            try
            {
                dBAccess.RemoveStateWeathersByDate(startDate, endDate, stateName).Wait();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public void RemoveStateConsumptionsByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            try
            {
                dBAccess.RemoveStateConsumptionsByDate(startDate, endDate, stateName).Wait();
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public IEnumerable<StateInfoModel> GetAllStates()
        {
            // no need for try catch since there is no exception
            return dBAccess.GetAllStates();
        }

        public StateInfoModel GetStateByName(string name)
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

        public IEnumerable<StateConsumptionModel> GetStateConsumptionByName(string stateName)
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
                retVal = dBAccess.GetStateWeatherByStateName(name).ToList();
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
                retVal = dBAccess.GetStateByDate(startDate, endDate, stateName);
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

        public String GetFullStateName(String shortStateName)
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

        public String GetShortStateName(String fullStateName)
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
