using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels;
using SharedModels.HelperClasses;
using Microsoft.VisualBasic.FileIO;

namespace BussinesLogic
{
    /// <summary>
    /// For Importing data from file
    /// </summary>
    
    public class ImportData
    {
        
        public static void LoadWeather(string wf)
        {
            using (TextFieldParser csvParser = new TextFieldParser(wf))
            {
                Dictionary<string, List<StateWeatherModel>> dictionary = new Dictionary<string, List<StateWeatherModel>>();



                //csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                string state = csvParser.ReadLine();
                string[] states = state.Split(',');
                state = states[1].Substring(1);


                //csvParser.ReadLine();

                dictionary.Add(state, new List<StateWeatherModel>());

                while (!csvParser.EndOfData)
                {
                    StateWeatherModel swm = new StateWeatherModel();
                    string[] fields = csvParser.ReadFields();
                    if (fields[0].StartsWith("#"))
                    {
                        continue;
                    }
                    
                    swm.LocalTime = DateTime.Parse(fields[0]);
                    swm.AirTemperature = float.Parse(fields[1]);
                    swm.StationPressure = float.Parse(fields[2]);
                    swm.ReducedPressure = float.Parse(fields[3]);
                    swm.Humidity = int.Parse(fields[4]);
                    swm.WindDirection = fields[5];
                    swm.WindSpeed = int.Parse(fields[6]);
                    swm.GustValue = int.Parse(fields[7]);
                    swm.PresentWeather = fields[8];
                    swm.RecentWeather = fields[9];
                    swm.CloudCover = fields[10];
                    swm.HorizontalVisibility = int.Parse(fields[11]);
                    swm.DevpointTemperature = int.Parse(fields[12]);

                    dictionary[state].Add(swm);
                }

                foreach(var m in dictionary)
                {
                    CurrentData.stateInfoModel
                }
                

                
            }

        }
        public static Dictionary<string, StateConsumptionModel> LoadConsumption(string cf, string stateName,DateTime dateTimeStart , DateTime dateTimeEnd)
        {
            using (TextFieldParser csvParser = new TextFieldParser(cf))
            {
                Dictionary<string, StateConsumptionModel> dictionary = new Dictionary<string, StateConsumptionModel>();


                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    StateConsumptionModel scm = new StateConsumptionModel();
                    string[] fields = csvParser.ReadFields();

                    scm.DateUTC = DateTime.Parse(fields[1]);
                    scm.DateShort = DateTime.Parse(fields[2]);
                    scm.DateShort = DateTime.Parse(fields[3]);
                    scm.DateTo = DateTime.Parse(fields[4]);
                    scm.StateCode = fields[5];
                    scm.CovRatio = int.Parse(fields[6]);
                    scm.Value = double.Parse(fields[7]);
                    scm.ValueScale = double.Parse(fields[8]);

                    dictionary.Add(scm.StateCode, scm);

                }

                return dictionary;
            }
        }

    }

}
