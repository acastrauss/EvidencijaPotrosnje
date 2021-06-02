using SharedModels;
using SharedModels.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic
{
    public interface IImportData
    {
        IEnumerable<StateWeatherModel> LoadWeather(ImportParameters importParameters, StateInfoModel state);
        IEnumerable<StateConsumptionModel> LoadConsumption(string cf, StateInfoModel state, DateTime startDate, DateTime endDate);
        void Load(ImportParameters parameters);
    }
}
