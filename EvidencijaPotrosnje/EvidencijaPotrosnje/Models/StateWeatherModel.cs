﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvidencijaPotrosnje.Models
{

	public class StateWeather
	{

		public float airTemperature;
		public string cloudCover;
		public int devpointTemperature;
		public int gustValue;
		public string horizontalVisibility;
		public int humidity;
		public string presentWeather;
		public string recentWeather;
		public float reducedPressure;
		public float stationPressure;
		public string windDirection;
		public int windSpeed;

		public StateWeather()
		{

		}

		~StateWeather()
		{

		}

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
		public StateWeather(float airTemperature, string cloudCover, int devpointTemperature, int gustValue, string horizontalVisibility, int humiditiy, string presentWeather, string recentWeather, float reducedPressure, float stationPressure, string windDirection, int windSpeed)
		{

		}

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

		public string HorizontalVisibility
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

	}//end StateWeather
}