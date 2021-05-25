using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels;
using SharedModels.HelperClasses;

namespace BussinesLogic
{
    /// <summary>
    /// For manipulating data for user interface (filtering, etc.)
    /// </summary>
    public class DataManipulation
    {
        public static void FilterByName(string StateName)
        {
            if (StateName == null)
                throw new Exception("Filter string can not be null!");

            Dictionary<DataKeys, StateInfoModel> temp = new Dictionary<DataKeys, StateInfoModel>();
            foreach(StateInfoModel state in CurrentData.Data.Values)
            {
                if(state.StateName == StateName)
                {
                    DataKeys dataKeys = new DataKeys(StateName, state.StateConsumption.DateFrom, state.StateConsumption.DateTo);
                    temp.Add(dataKeys, state);
                }

            }

            CurrentData.Data = temp;
        }

        public static void FilterByTime(DateTime DateFrom, DateTime DateTo)
        {
            Dictionary<DataKeys, StateInfoModel> temp = new Dictionary<DataKeys, StateInfoModel>();
            foreach (StateInfoModel state in CurrentData.Data.Values)
            {
                if (state.StateConsumption.DateFrom >= DateFrom && state.StateConsumption.DateTo <= DateTo 
                    && state.StateWeather.LocalTime >= DateFrom && state.StateWeather.LocalTime <= DateTo)
                {
                    DataKeys dataKeys = new DataKeys(state.StateName, DateFrom, DateTo);
                    temp.Add(dataKeys, state);
                }

            }

            CurrentData.Data = temp;

        }   
    }
}
