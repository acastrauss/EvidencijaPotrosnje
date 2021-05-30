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
        private int covRatio;
		private DateTime dateFrom;
		private DateTime dateShort;
		private DateTime dateTo;
		private DateTime dateUTC;
		private string stateCode;
		private double value;
		private double valueScale;

		private int stateId;
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
		public StateConsumptionModel(int covRatio, DateTime dateFrom, DateTime dateShort, DateTime dateTo, DateTime dateUTC, string stateCode, double value, double valueScale, int stateId)
		{
			this.covRatio = covRatio;
			this.dateFrom = dateFrom;
			this.dateShort = dateShort;
			this.dateTo = dateTo;
			this.dateUTC = dateUTC;
			this.stateCode = stateCode;
			this.value = value;
			this.valueScale = valueScale;
			this.stateId = stateId;
		}
        #endregion

        #region Properties
        public int CovRatio
		{
			get
			{
				return covRatio;
			}
			set
			{
				covRatio = value;
			}
		}

		public DateTime DateFrom
		{
			get
			{
				return dateFrom;
			}
			set
			{
				dateFrom = value;
			}
		}

		public DateTime DateShort
		{
			get
			{
				return dateShort;
			}
			set
			{
				dateShort = value;
			}
		}

		public DateTime DateTo
		{
			get
			{
				return dateTo;
			}
			set
			{
				dateTo = value;
			}
		}

		public DateTime DateUTC
		{
			get
			{
				return dateUTC;
			}
			set
			{
				dateUTC = value;
			}
		}

		public string StateCode
		{
			get
			{
				return stateCode;
			}
			set
			{
				stateCode = value;
			}
		}

		public double Value
		{
			get
			{
				return value;
			}
			set
			{
				this.value = value;
			}
		}

		public double ValueScale
		{
			get
			{
				return valueScale;
			}
			set
			{
				valueScale = value;
			}
		}

		public int StateId
		{
			get
			{
				return stateId;
			}
			set
			{
				stateId = value;
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

        public override bool Equals(object obj)
        {
			var temp = (StateConsumptionModel)obj;

			if (obj == null)
				return false;

			return
				this.covRatio == temp.covRatio &&
				this.dateFrom == temp.dateFrom &&
				this.dateShort == temp.dateShort &&
				this.dateTo == temp.dateTo &&
				this.dateUTC == temp.dateUTC && 
				this.stateCode == temp.stateCode &&
				this.value == temp.value &&
				this.valueScale == temp.valueScale
				;
		}

        #endregion

    }//end StateConsumption
}
