using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.HelperClasses
{
    /// <summary>
    /// Class that contains parameters for import
    /// </summary>
    public class ImportParameters
    {
        public string WeatherFile { get; set; }
        public string ConsumptionFile { get; set; }
        
        // only for consumption
        public string StateName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public ImportParameters(string weatherFile = "", string consumptionFile = "", string stateName = "", DateTime? startDate = null, DateTime? endDate = null) 
        {
            this.WeatherFile = weatherFile;
            this.ConsumptionFile = consumptionFile;
            this.StateName = stateName;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
    }
}
