using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.HelperClasses
{
    public class ShowingData
    {
        #region Constructors

        public ShowingData(string stateName, DateTime dateUTC, double consumptionValue, float temperature, float pressure, int humidity, int windSpeed)
        {
            StateName = stateName;
            DateUTC = dateUTC;
            ConsumptionValue = consumptionValue;
            Temperature = temperature;
            Pressure = pressure;
            Humidity = humidity;
            WindSpeed = windSpeed;
        }

        public ShowingData() 
        {
            StateName = String.Empty;
            DateUTC = DateTime.MinValue;
            // fleg
            ConsumptionValue = 0;
            Temperature = 0;
            Pressure = 0;
            Humidity = 0;
            WindSpeed = 0;
        }

        public ShowingData(ShowingData showingDataRef)
        {
            StateName = showingDataRef.StateName;
            DateUTC = showingDataRef.DateUTC;
            ConsumptionValue = showingDataRef.ConsumptionValue;
            Temperature = showingDataRef.Temperature;
            Pressure = showingDataRef.Pressure;
            Humidity = showingDataRef.Humidity;
            WindSpeed = showingDataRef.Humidity;
        }

        #endregion

        #region Properties

        public String StateName { get; set; }
        public DateTime DateUTC { get; set; }
        public double ConsumptionValue { get; set; }
        public float Temperature { get; set; }
        public float Pressure { get; set; }
        public int Humidity { get; set; }
        public int WindSpeed { get; set; }
        public bool HasC { get; set; }
        public bool HasW { get; set; }
        #endregion

        #region AdditionalMethods

        public override bool Equals(object obj)
        {
            ShowingData showingData = (ShowingData)obj;

            return 
                this.StateName == showingData.StateName &&
                this.DateUTC == showingData.DateUTC &&
                this.ConsumptionValue == showingData.ConsumptionValue &&
                this.Temperature == showingData.Temperature &&
                this.Pressure == showingData.Pressure &&
                this.Humidity == showingData.Humidity &&
                this.WindSpeed == showingData.WindSpeed
                ;
        }

        #endregion
    }
}
