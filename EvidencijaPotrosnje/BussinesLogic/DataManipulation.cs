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
        public static List<StateInfoModel> FilterByName(string StateName, List<StateInfoModel> lista)
        {
            List<StateInfoModel> povratna = new List<StateInfoModel>();

            if (String.IsNullOrEmpty(StateName))
            {
                if (StateName == null)
                    throw new Exception("Filter string can not be null!");
                else if (StateName.Equals(String.Empty))
                    return povratna;
            }
            else
            {

                foreach (StateInfoModel model in lista)
                {
                    if (model.StateName == StateName)
                    {
                        povratna.Add(model);
                    }
                }


            }

            return povratna;
        }

        public static List<StateInfoModel> FilterByTime(DateTime DateFrom, DateTime DateTo, List<StateInfoModel> lista)
        {
            //Dictionary<DataKeys, StateInfoModel> temp = new Dictionary<DataKeys, StateInfoModel>();
            //foreach (var pair in CurrentData.Data)
            //{
            //    var state = pair.Value;

            //} 
            //CurrentData.Data = temp;
            List<StateInfoModel> povratna = new List<StateInfoModel>();

            foreach (StateInfoModel model in lista)
            {
                StateInfoModel state = new StateInfoModel();
                state.StateName = model.StateName;
                state.StateId = model.StateId;
                state.StateConsumption = FilterConsumptions(DateFrom, DateTo, model.StateConsumption);
                state.StateWeathers = FilterWeathers(DateFrom, DateTo, model.StateWeathers);
                povratna.Add(state);
            }


            return povratna;

        }

        public static List<StateConsumptionModel> FilterConsumptions(DateTime DateFrom, DateTime DateTo, List<StateConsumptionModel> lista)
        {
            List<StateConsumptionModel> potrosnje = new List<StateConsumptionModel>();

            foreach (StateConsumptionModel model in lista)
            {
                if ((DateTime.Compare(DateFrom, model.DateUTC) <= 0) && (DateTime.Compare(DateTo, model.DateUTC) >= 0))
                {
                    potrosnje.Add(model);
                }
            }
            return potrosnje;
        }

        public static List<StateWeatherModel> FilterWeathers(DateTime DateFrom, DateTime DateTo, List<StateWeatherModel> lista)
        {
            List<StateWeatherModel> vremena = new List<StateWeatherModel>();

            foreach (StateWeatherModel model in lista)
            {
                if ((DateTime.Compare(DateFrom, model.LocalTime) <= 0) && (DateTime.Compare(DateTo, model.LocalTime) >= 0))
                {
                    vremena.Add(model);
                }
            }
            return vremena;
        }
    }
}
