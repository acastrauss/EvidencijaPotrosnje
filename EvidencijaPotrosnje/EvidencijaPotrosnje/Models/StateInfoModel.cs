using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvidencijaPotrosnje.Models
{
	public class StateInfoModel
	{

        private StateWeatherModel stateWeather;
        private StateConsumptionModel stateConsumption;
        private string stateName;

        public StateWeatherModel StateWeather { get => stateWeather; set => stateWeather = value; }
        public StateConsumptionModel StateConsumption { get => stateConsumption; set => stateConsumption = value; }
        public string StateName { get => stateName; set => stateName = value; }

        public StateInfoModel()
		{

		}

		~StateInfoModel()
		{

		}

		/// 
		/// <param name="stateWeather"></param>
		/// <param name="stateConsumption"></param>
		public StateInfoModel(StateWeatherModel stateWeather, StateConsumptionModel stateConsumption, string stateName)
		{
			this.stateWeather = stateWeather;
			this.stateConsumption = stateConsumption;
			this.stateName = stateName;
		}

	}//end StateInfo
}