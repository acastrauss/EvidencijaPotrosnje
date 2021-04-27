using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvidencijaPotrosnje.Models
{
	public class StateInfoModel
	{

		public StateWeatherModel m_StateWeather;
		public StateConsumptionModel m_StateConsumption;

		public StateInfoModel()
		{

		}

		~StateInfoModel()
		{

		}

		/// 
		/// <param name="stateWeather"></param>
		/// <param name="stateConsumption"></param>
		public StateInfoModel(StateWeatherModel stateWeather, StateConsumptionModel stateConsumption)
		{

		}

	}//end StateInfo
}