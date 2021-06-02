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
        bool IfStateExistByName(string name);
        void AddStates(IEnumerable<StateInfoModel> models);
        
        /// <summary>
        /// All methods that return task run asyncronously, because saving
        /// changes to DB is slow so it needs to be done with tasks
        /// </summary>
        Task AddStateWeathers(IEnumerable<StateWeatherModel> models, String stateName);
        Task AddStateConsumption(IEnumerable<StateConsumptionModel> models, String stateName);

        /// <summary>
        /// Only removes data (consumption and weather) for given state
        /// </summary>
        /// <param name="model"></param>
        Task RemoveState(String stateName);
        /// <summary>
        /// Removes the whole state
        /// </summary>
        /// <param name="model"></param>
        Task RemoveStateTotally(String stateName);
        Task RemoveAllStates();
        Task RemoveAllStatesTotally();
        Task RemoveStateWeathers(String stateName);
        Task RemoveStateConsumption(String stateName);
        Task RemoveStateWeathersByDate(DateTime startDate, DateTime endDate, String stateName);
        Task RemoveStateConsumptionsByDate(DateTime startDate, DateTime endDate, String stateName);
        /// <summary>
        /// Removes every data for given date
        /// </summary>
        Task RemoveStateByDate(DateTime startDate, DateTime endDate, String stateName);
        StateInfoModel GetStateByName(String name);
        IEnumerable<StateConsumptionModel> GetStateConsumptionByStateName(String name);
        IEnumerable<StateWeatherModel> GetStateWeatherByStateName(String name);
        IEnumerable<StateInfoModel> GetAllStates();
        StateInfoModel GetStateByDate(DateTime startDate, DateTime endDate, String stateName);
        IEnumerable<StateConsumptionModel> GetStateConsumptionsByDate(DateTime startDate, DateTime endDate, String stateName);
        IEnumerable<StateWeatherModel> GetStateWeathersByDate(DateTime startDate, DateTime endDate, String stateName);
        String GetShortStateName(String fullStateName);
        String GetFullStateName(String shortStateName);
    }
}
