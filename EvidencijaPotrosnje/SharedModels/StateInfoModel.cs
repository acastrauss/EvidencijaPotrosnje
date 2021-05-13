using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{
	public class StateInfoModel
	{

        #region Fields
        private StateWeatherModel stateWeather;
		private StateConsumptionModel stateConsumption;
		private String stateName;
        #endregion

        #region Properties
        public StateWeatherModel StateWeather { get => stateWeather; set => stateWeather = value; }
		public StateConsumptionModel StateConsumption { get => stateConsumption; set => stateConsumption = value; }
		public String StateName { get => stateName; set => stateName = value; }
        #endregion

        #region ConstructorsAndDestructor
        public StateInfoModel() {}

		~StateInfoModel() {}

		/// 
		/// <param name="stateWeather"></param>
		/// <param name="stateConsumption"></param>
		public StateInfoModel(StateWeatherModel stateWeather, StateConsumptionModel stateConsumption, string stateName)
		{
			this.stateWeather = stateWeather;
			this.stateConsumption = stateConsumption;
			this.stateName = stateName;
		}
        #endregion

		
        #region PseudoMethods

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool IsValid()
        {
			if (stateWeather == null || stateConsumption == null)
				return false;

			return stateWeather.IsValid() &&
				stateConsumption.IsValid() &&
				!String.IsNullOrEmpty(stateName);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion
    }//end StateInfo
}
