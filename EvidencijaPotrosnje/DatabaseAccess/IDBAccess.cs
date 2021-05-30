using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess
{
    public interface IDBAccess
    {
        void AddState(StateInfoModel model);
        bool IfStateExistByName(string name, StatesDB db);
        /// <summary>
        /// Only removes data (consumption and weather) for given state
        /// </summary>
        /// <param name="model"></param>
        void RemoveState(String stateName);
        /// <summary>
        /// Removes the whole state
        /// </summary>
        /// <param name="model"></param>
        void RemoveStateTotally(String stateName);
        void RemoveAllStates();
        void RemoveAllStatesTotally();
        void RemoveStateWeathers(String stateName);
        void RemoveStateConsumption(String stateName);
        void RemoveStateWeathersByDate(DateTime startDate, DateTime endDate, String stateName);
        void RemoveStateConsumptionsByDate(DateTime startDate, DateTime endDate, String stateName);
        /// <summary>
        /// Removes every data for given date
        /// </summary>
        void RemoveStateByDate(DateTime startDate, DateTime endDate, String stateName);
        StateInfoModel GetStateByName(String name);
        IEnumerable<StateConsumptionModel> GetStateConsumptionByStateName(String name);
        IEnumerable<StateWeatherModel> GetStateWeatherByStateName(String name);
        IEnumerable<StateInfoModel> GetAllStates();
        StateInfoModel GetStateByDate(DateTime startDate, DateTime endDate, String stateName);
        IEnumerable<StateConsumption> GetStateConsumptionsByDate(DateTime startDate, DateTime endDate, String stateName);
        IEnumerable<StateWeather> GetStateWeathersByDate(DateTime startDate, DateTime endDate, String stateName);
        String GetShortStateName(String fullStateName);
        String GetFullStateName(String shortStateName);
    }
}
