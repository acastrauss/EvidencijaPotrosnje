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
        void AddOrUpdateState(StateInfoModel model);
        bool IfStateExistByName(string name, StatesDB db);
        State GetDBStateByName(string name, StatesDB db);
        void UpdateState(State state, StatesDB db);
        void RemoveState(StateInfoModel model);
        void RemoveAllStates();
        StateInfoModel GetStateByName(string name);
        StateConsumptionModel GetStateConsumptionByStateName(string name);
        StateWeatherModel GetStateWeatherByStateName(string name);
        List<StateInfoModel> GetAllStates();
        String GetShortStateName(String fullStateName);
        String GetFullStateName(String shortStateName);
    }
}
