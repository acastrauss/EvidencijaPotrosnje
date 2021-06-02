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
        public string SaveData(IEnumerable<ShowingData> showingDataI, string[] columns)
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
                    string csvRow = "";

                    if (columns.Contains("Drzava"))
                    {
                        String stataName = data.StateName;
                        csvRow += stataName + ","; 
                    }
                    if (columns.Contains("UTC vreme"))
                    {
                        String dateUTC = data.DateUTC.ToString();
                        csvRow += dateUTC + ",";
                    }
                    if (columns.Contains("Potrosnja"))
                    {
                        String consValue = data.ConsumptionValue.ToString();
                        csvRow += consValue + ",";
                    }
                    if (columns.Contains("Temperatura"))
                    {
                        String temp = data.Temperature.ToString();
                        csvRow += temp + ",";
                    }
                    if (columns.Contains("Pritisak"))
                    {
                        String pressure = data.Pressure.ToString();
                        csvRow += pressure + ",";
                    }
                    if (columns.Contains("Vlaznost"))
                    {
                        String humidity = data.Humidity.ToString();
                        csvRow += humidity + ",";
                    }
                    if (columns.Contains("Brzina vetra"))
                    {
                        String ws = data.WindSpeed.ToString();
                        csvRow += ws + ",";
                    }

                    csvWrite.WriteLine(csvRow);
                    csvRow = "";
                }
            }

            return fullPath;
        }
    }
}
