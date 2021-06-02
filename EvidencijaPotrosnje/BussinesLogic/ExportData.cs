using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels;
using SharedModels.HelperClasses;
using Microsoft.VisualBasic.FileIO;

namespace BussinesLogic
{
    /// <summary>
    /// For exporting selected (filtered) data to given file
    /// </summary>
    public class ExportData : IExportData
    {
        public void SaveData(IEnumerable<ShowingData> showingDataI)
        {
            if (showingDataI == null)
            {
                throw new Exception("List for export cant be null");

            }

            List<ShowingData> showingData = showingDataI.ToList();

            if (showingData.Count == 0)
            {
                throw new Exception("List for export cant be emtpy");
            }
            

            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase); //put do priject foldera \\\Debug
            string a = "Output_" + DateTime.Now.ToString("yyy_MM_d_HH_mm") + ".csv";// +/* DateTime.Now.ToString("yyyy/MM/d/HH/mm/ss") */ //+ ".csv";
            string[] paths = { @path, a };
            string fullPath = System.IO.Path.Combine(paths); // file:\\C:------------
            fullPath = fullPath.Substring(6, fullPath.Length - 6); //Skraceno

            using (var csvWrite = File.CreateText(fullPath))
            {
                foreach (var data in showingData)
                {
                    String stataName = data.StateName;
                    String dateUTC = data.DateUTC.ToString();
                    String consValue = data.ConsumptionValue.ToString();
                    String temp = data.Temperature.ToString();
                    String pressure = data.Pressure.ToString();
                    String humidity = data.Humidity.ToString();
                    String ws = data.WindSpeed.ToString();

                    string csvRow = string.Format("{0},{1},{2},{3},{4},{5},{6}"
                        , stataName, dateUTC, consValue, temp, pressure, humidity, ws);

                    csvWrite.WriteLine(csvRow);
                }
            }

        }
    }
}
