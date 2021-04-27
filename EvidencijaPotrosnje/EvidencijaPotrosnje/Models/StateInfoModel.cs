using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvidencijaPotrosnje.Models
{
	public class StateInfo
	{

		public StateWeather m_StateWeather;
		public StateConsumption m_StateConsumption;

		public StateInfo()
		{

		}

		~StateInfo()
		{

		}

		/// 
		/// <param name="stateWeather"></param>
		/// <param name="stateConsumption"></param>
		public StateInfo(StateWeather stateWeather, StateConsumption stateConsumption)
		{

		}

	}//end StateInfo
}