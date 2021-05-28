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
        
		public StateInfoModel() { }

		/// <summary>
		/// StateInfoModel is not be valid
		/// </summary>
		public static StateInfoModel NotValid() 
		{
			return new StateInfoModel()
			{
				stateWeather = null,
				stateConsumption = null,
				stateName = String.Empty
			};
		}

		~StateInfoModel() {}

		/// 
		/// <param name="stateWeather"></param>
		/// <param name="stateConsumption"></param>
		public StateInfoModel(StateWeatherModel stateWeather, StateConsumptionModel stateConsumption, string stateName)
		{
			this.stateWeather = stateWeather;
			this.stateConsumption = stateConsumption;
			this.stateName = stateName.Trim();
		}
        #endregion
		
        #region PseudoMethods

		/// <summary>
		/// Because state can have only info for weather of consumption
		/// </summary>
		/// <returns></returns>
		public bool IsValid()
        {
            if (stateWeather == null || stateConsumption == null)
                return false;

            return (stateWeather.IsValid() ||
                stateConsumption.IsValid())
                && !String.IsNullOrEmpty(stateName);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
			var temp = (StateInfoModel)obj;

			bool eq1 = this.StateConsumption.Equals(temp.StateConsumption);
			bool eq2 = this.StateName.Equals(temp.StateName);
			bool eq3 = this.StateWeather.Equals(temp.StateWeather);
			
			return
				this.StateConsumption.Equals(temp.StateConsumption) &&
				this.StateName.Equals(temp.StateName) &&
				this.StateWeather.Equals(temp.StateWeather)
				;
        }

        #endregion
    }//end StateInfo
}
