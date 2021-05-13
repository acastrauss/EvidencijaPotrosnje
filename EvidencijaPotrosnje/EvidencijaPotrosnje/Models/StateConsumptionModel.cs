using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvidencijaPotrosnje.Models
{

	public class StateConsumptionModel
	{

		public int covRatio;
		public DateTime dateFrom;
		public DateTime dateShort;
		public DateTime dateTo;
		public DateTime dateUTC;
		public string stateCode;
		public double value;
		public double valueScale;

		public StateConsumptionModel()
		{

		}

		~StateConsumptionModel()
		{

		}

		public StateConsumptionModel(int covRatio, DateTime dateFrom, DateTime dateShort, DateTime dateTo, DateTime dateUTC, string stateCode, double value, double valueScale)
		{
			this.covRatio = covRatio;
			this.dateFrom = dateFrom;
			this.dateShort = dateShort;
			this.dateTo = dateTo;
			this.dateUTC = dateUTC;
			this.stateCode = stateCode;
			this.value = value;
			this.valueScale = valueScale;
		}

		/// 
		/// <param name="covRatio"></param>
		/// <param name="dateFrom"></param>
		/// <param name="dateShort"></param>
		/// <param name="dateTo"></param>
		/// <param name="dateUTC"></param>
		/// <param name="stateCode"></param>
		/// <param name="value"></param>
		/// <param name="valueScale"></param>


		public int CovRatio
		{
			get
			{
				return CovRatio;
			}
			set
			{
				CovRatio = value;
			}
		}

		public DateTime DateFrom
		{
			get
			{
				return DateFrom;
			}
			set
			{
				DateFrom = value;
			}
		}

		public DateTime DateShort
		{
			get
			{
				return DateShort;
			}
			set
			{
				DateShort = value;
			}
		}

		public DateTime DateTo
		{
			get
			{
				return DateTo;
			}
			set
			{
				DateTo = value;
			}
		}

		public DateTime DateUTC
		{
			get
			{
				return DateUTC;
			}
			set
			{
				DateUTC = value;
			}
		}

		public string StateCode
		{
			get
			{
				return StateCode;
			}
			set
			{
				StateCode = value;
			}
		}

		public double Value
		{
			get
			{
				return Value;
			}
			set
			{
				Value = value;
			}
		}

		public double ValueScale
		{
			get
			{
				return ValueScale;
			}
			set
			{
				ValueScale = value;
			}
		}

	}//end StateConsumption
}