using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels;
using SharedModels.HelperClasses;

namespace BussinesLogic
{
    /// <summary>
    /// For exporting selected (filtered) data to given file
    /// </summary>
    public class ExportData
    {
        public void SaveData(List<StateInfoModel> Countries, String fileName)
        {
            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase); //put do priject foldera \\\Debug
            string a = fileName + ".csv";
            string[] paths = { @path, a };
            string fullPath = System.IO.Path.Combine(paths); // file:\\C:------------
            fullPath = fullPath.Substring(6, fullPath.Length - 6); //Skraceno
            //var file = @"C:\OutputFile.csv";
            //CountriesDictionary countriesDictionary = new CountriesDictionary();

            using (var stream = File.CreateText(fullPath))
            {
                foreach (var Country in Countries)
                {
                    string StateName = DBLogic.GetFullStateName(Country.StateConsumption.StateCode);
                    string UCTTime = Country.StateConsumption.DateUTC.ToString();
                    string Consumption = Country.StateConsumption.Value.ToString();
                    string temperature = Country.StateWeather.AirTemperature.ToString();
                    string pressure = Country.StateWeather.StationPressure.ToString();
                    string humidity = Country.StateWeather.Humidity.ToString();
                    string windSpeed = Country.StateWeather.WindSpeed.ToString();

                    string csvRow = string.Format("{0},{1},{2},{3},{4},{5},{6}",StateName,UCTTime,Consumption,temperature,pressure,humidity,windSpeed);

                    stream.WriteLine(csvRow);
                }
            }

        }

    }
}
