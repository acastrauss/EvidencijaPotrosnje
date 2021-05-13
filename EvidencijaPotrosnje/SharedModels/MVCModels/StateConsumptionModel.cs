using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels
{

	public class StateConsumptionModel
	{
        #region Fields
        public int covRatio;
		public DateTime dateFrom;
		public DateTime dateShort;
		public DateTime dateTo;
		public DateTime dateUTC;
		public string stateCode;
		public double value;
		public double valueScale;
        #endregion

        #region ConstructorsAndDestructor
        
		public StateConsumptionModel() { }
		/// StateConsumption not valid
		public static StateConsumptionModel NotValid() 
		{
			return new StateConsumptionModel()
			{
				covRatio = 0,
				dateFrom = DateTime.Now,
				dateShort = DateTime.Now,
				dateTo = DateTime.Now,
				dateUTC = DateTime.Now.AddYears(1000), // so it won't be valid
				stateCode = String.Empty, // also not valid
				value = -1, // not valid
				valueScale = 0
			};
		}

		~StateConsumptionModel() {}

		/// 
		/// <param name="covRatio"></param>
		/// <param name="dateFrom"></param>
		/// <param name="dateShort"></param>
		/// <param name="dateTo"></param>
		/// <param name="dateUTC"></param>
		/// <param name="stateCode"></param>
		/// <param name="value"></param>
		/// <param name="valueScale"></param>
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
        #endregion

        #region Properties
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
		#endregion

		#region PseudoMethods
		/// <summary>
		/// StateConsumptionModel is valid if:
		/// dateUTC != null && dateUTC <= DateTime.Now (cannot have value from future)
 		/// value != null
		/// </summary>
		/// <returns></returns>
		public bool IsValid()
		{	
			return dateUTC != null && dateUTC <= DateTime.Now && value >= 0 && !String.IsNullOrEmpty(stateCode);
		}

		/// <summary>
		/// String representation of MVC model
		/// </summary>
		/// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

    }//end StateConsumption
}
