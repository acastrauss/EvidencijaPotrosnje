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
    /// Maps consumption data and weather data for showing
    /// </summary>
    public class MapCW : IMapCW
    {
        public IEnumerable<ShowingData> MapData(StateInfoModel stateInfoModel)
        {
            if(stateInfoModel == null)
            {
                throw new Exception("State info model cant be null");
            }

            List<ShowingData> showingDatas = new List<ShowingData>();

            foreach (var cons in stateInfoModel.StateConsumption)
            {
                var dateAdded = cons.DateShort.AddHours(cons.DateTo.Hour).AddMinutes(cons.DateTo.Minute).AddSeconds(cons.DateTo.Second);

                var weather = stateInfoModel.StateWeathers.Find(x => (x.LocalTime - dateAdded).Days == 0);

                if (weather != null)
                {
                    ShowingData showingData = new ShowingData()
                    {
                        ConsumptionValue = cons.Value,
                        DateUTC = dateAdded,
                        Humidity = weather.Humidity,
                        Pressure = weather.StationPressure,
                        StateName = stateInfoModel.StateName,
                        Temperature = weather.AirTemperature,
                        WindSpeed = weather.WindSpeed,
                        HasC = true,
                        HasW = true
                    };

                    stateInfoModel.StateWeathers.Remove(weather);

                    showingDatas.Add(showingData);
                }
                else
                {
                    ShowingData showingData = new ShowingData()
                    {
                        ConsumptionValue = cons.Value,
                        DateUTC = dateAdded,
                        StateName = stateInfoModel.StateName,
                        HasC = true,
                        HasW = false
                    };

                    showingDatas.Add(showingData);
                }
            }

            foreach (var w in stateInfoModel.StateWeathers)
            {
                ShowingData showingData = new ShowingData()
                {
                    HasC = false,
                    HasW = true,
                    DateUTC = w.LocalTime,
                    Humidity = w.Humidity,
                    Pressure = w.StationPressure,
                    StateName = stateInfoModel.StateName,
                    Temperature = w.AirTemperature,
                    WindSpeed = w.WindSpeed
                };

                showingDatas.Add(showingData);
            }

            showingDatas.OrderBy(x => x.DateUTC);

            return showingDatas;
        }

    }
}
