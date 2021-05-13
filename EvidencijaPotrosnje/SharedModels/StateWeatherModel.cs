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
        #endregion

        #region ConstructorsAndDestructor
        public StateWeatherModel() {}

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
		public StateWeatherModel(float airTemperature, string cloudCover, int devpointTemperature, int gustValue, int horizontalVisibility, int humiditiy, string presentWeather, string recentWeather, float reducedPressure, float stationPressure, string windDirection, int windSpeed)
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
		}
        #endregion

        #region Properties
        public float AirTemperature
		{
			get
			{
				return AirTemperature;
			}
			set
			{
				AirTemperature = value;
			}
		}

		public string CloudCover
		{
			get
			{
				return CloudCover;
			}
			set
			{
				CloudCover = value;
			}
		}

		public int DevpointTemperature
		{
			get
			{
				return DevpointTemperature;
			}
			set
			{
				DevpointTemperature = value;
			}
		}

		public int GustValue
		{
			get
			{
				return GustValue;
			}
			set
			{
				GustValue = value;
			}
		}

		public int HorizontalVisibility
		{
			get
			{
				return HorizontalVisibility;
			}
			set
			{
				HorizontalVisibility = value;
			}
		}

		public int Humidity
		{
			get
			{
				return Humidity;
			}
			set
			{
				Humidity = value;
			}
		}

		public string PresentWeather
		{
			get
			{
				return PresentWeather;
			}
			set
			{
				PresentWeather = value;
			}
		}

		public string RecentWeather
		{
			get
			{
				return RecentWeather;
			}
			set
			{
				RecentWeather = value;
			}
		}

		public float ReducedPressure
		{
			get
			{
				return ReducedPressure;
			}
			set
			{
				ReducedPressure = value;
			}
		}

		public float StationPressure
		{
			get
			{
				return StationPressure;
			}
			set
			{
				StationPressure = value;
			}
		}

		public string WindDirection
		{
			get
			{
				return WindDirection;
			}
			set
			{
				WindDirection = value;
			}
		}

		public int WindSpeed
		{
			get
			{
				return WindSpeed;
			}
			set
			{
				WindSpeed = value;
			}
		}

        #endregion

        #region PseudoMethods
        
		/// <summary>
		/// StateWeatherModel is valid if:
		/// airTemperature != null &&
		/// stationPressure != null && or not negative
		/// humidity != null && or not negative
		/// windSpeed != null or not negative
		/// </summary>
		/// <returns></returns>
		public bool IsValid() 
		{
			return
				stationPressure >= 0 &&
				humidity >= 0 &&
				windSpeed >= 0;
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
