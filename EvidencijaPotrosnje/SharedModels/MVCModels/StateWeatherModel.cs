using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{

	public class StateWeatherModel
	{
        #region Fields
        private float airTemperature;
		private string cloudCover;
		private int devpointTemperature;
		private int gustValue;
		private int horizontalVisibility;
		private int humidity;
		private string presentWeather;
		private string recentWeather;
		private float reducedPressure;
		private float stationPressure;
		private string windDirection;
		private int windSpeed;
		private DateTime localTime;
        #endregion

        #region ConstructorsAndDestructor
        public StateWeatherModel() {}

		/// <summary>
		/// Not valid value for StateWeatherModel
		/// </summary>
		/// <returns></returns>
		public static StateWeatherModel NotValid() 
		{
			return new StateWeatherModel()
			{
				airTemperature = -float.MaxValue,
				stationPressure = -1,
				humidity = -1,
				windSpeed = -1,
				localTime = DateTime.Now.AddYears(100)
			};
		}

		~StateWeatherModel() {}

		/// 
		/// <param name="airTemperature"></param>
		/// <param name="cloudCover"></param>
		/// <param name="devpointTemperature"></param>
		/// <param name="gustValue"></param>
		/// <param name="horizontalVisibility"></param>
		/// <param name="humiditiy"></param>
		/// <param name="presentWeather"></param>
		/// <param name="recentWeather"></param>
		/// <param name="reducedPressure"></param>
		/// <param name="stationPressure"></param>
		/// <param name="windDirection"></param>
		/// <param name="windSpeed"></param>
		/// <param name="localTime"></param>
		public StateWeatherModel(float airTemperature, string cloudCover, int devpointTemperature, int gustValue, int horizontalVisibility, int humiditiy, string presentWeather, string recentWeather, float reducedPressure, float stationPressure, string windDirection, int windSpeed, DateTime localTime)
		{
			this.airTemperature = airTemperature;
			this.cloudCover = cloudCover;
			this.devpointTemperature = devpointTemperature;
			this.gustValue = gustValue;
			this.horizontalVisibility = horizontalVisibility;
			this.humidity = humiditiy;
			this.presentWeather = presentWeather;
			this.recentWeather = recentWeather;
			this.reducedPressure = reducedPressure;
			this.stationPressure = stationPressure;
			this.windDirection = windDirection;
			this.windSpeed = windSpeed;
			this.localTime = localTime;
		}
        #endregion

        #region Properties
        public float AirTemperature
		{
			get
			{
				return airTemperature;
			}
			set
			{
				airTemperature = value;
			}
		}

		public string CloudCover
		{
			get
			{
				return cloudCover;
			}
			set
			{
				cloudCover = value;
			}
		}

		public int DevpointTemperature
		{
			get
			{
				return devpointTemperature;
			}
			set
			{
				devpointTemperature = value;
			}
		}

		public int GustValue
		{
			get
			{
				return gustValue;
			}
			set
			{
				gustValue = value;
			}
		}

		public int HorizontalVisibility
		{
			get
			{
				return horizontalVisibility;
			}
			set
			{
				horizontalVisibility = value;
			}
		}

		public int Humidity
		{
			get
			{
				return humidity;
			}
			set
			{
				humidity = value;
			}
		}

		public string PresentWeather
		{
			get
			{
				return presentWeather;
			}
			set
			{
				presentWeather = value;
			}
		}

		public string RecentWeather
		{
			get
			{
				return recentWeather;
			}
			set
			{
				recentWeather = value;
			}
		}

		public float ReducedPressure
		{
			get
			{
				return reducedPressure;
			}
			set
			{
				reducedPressure = value;
			}
		}

		public float StationPressure
		{
			get
			{
				return stationPressure;
			}
			set
			{
				stationPressure = value;
			}
		}

		public string WindDirection
		{
			get
			{
				return windDirection;
			}
			set
			{
				windDirection = value;
			}
		}

		public int WindSpeed
		{
			get
			{
				return windSpeed;
			}
			set
			{
				windSpeed = value;
			}
		}

		public DateTime LocalTime { get => localTime; set => localTime = value; }


		#endregion

		#region PseudoMethods

		/// <summary>
		/// StateWeatherModel is valid if:
		/// airTemperature != null && airTemperature != -float.MaxValue
		/// stationPressure != null && or not negative 
		/// humidity != null && or not negative
		/// windSpeed != null or not negative
		/// </summary>
		/// <returns></returns>
		public bool IsValid() 
		{
			return
				airTemperature != -float.MaxValue &&
				stationPressure >= 0 &&
				humidity >= 0 &&
				windSpeed >= 0 &&
				localTime <= DateTime.Now;
		}

		/// <summary>
		/// String representation of StateWeatherModel
		/// </summary>
		/// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }

        #endregion
    }//end StateWeather
}
