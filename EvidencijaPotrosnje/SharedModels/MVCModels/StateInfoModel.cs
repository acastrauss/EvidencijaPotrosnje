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
        private List<StateWeatherModel> stateWeathers;
		private List<StateConsumptionModel> stateConsumptions;
		private String stateName;
        #endregion

        #region Properties
        public List<StateWeatherModel> StateWeathers { get => stateWeathers; set => stateWeathers = value; }
		public List<StateConsumptionModel> StateConsumption { get => stateConsumptions; set => stateConsumptions = value; }
		public String StateName { get => stateName; set => stateName = value; }
        #endregion

        #region ConstructorsAndDestructor
        
		public StateInfoModel() 
		{
			stateWeathers = new List<StateWeatherModel>();
			stateConsumptions = new List<StateConsumptionModel>();
		}

		/// <summary>
		/// StateInfoModel is not be valid
		/// </summary>
		public static StateInfoModel NotValid() 
		{
			return new StateInfoModel()
			{
				stateName = String.Empty
			};
		}

		~StateInfoModel() {}

		/// 
		/// <param name="stateWeather"></param>
		/// <param name="stateConsumption"></param>
		public StateInfoModel(string stateName)
		{
			this.stateName = stateName;
			this.stateConsumptions = new List<StateConsumptionModel>();
			this.stateWeathers = new List<StateWeatherModel>();
		}
        #endregion
		
        #region PseudoMethods

		/// <summary>
		/// Because state can have only info for weather of consumption
		/// </summary>
		/// <returns></returns>
		public bool IsValid()
        {
            return !String.IsNullOrEmpty(stateName);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
			var temp = (StateInfoModel)obj;

			return
				this.StateName.Equals(temp.StateName);
        }

        #endregion
    }//end StateInfo
}
