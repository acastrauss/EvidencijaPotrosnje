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

        private static void LoadWeather(string wf)
        {
            using (TextFieldParser csvParser = new TextFieldParser(wf))
            {             
                //csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                string state = csvParser.ReadLine();
                string[] states = state.Split(',');
                state = states[1].Substring(1);


                //csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    StateWeatherModel swm = new StateWeatherModel();
                    string[] fields = csvParser.ReadFields();
                    if (fields[0].StartsWith("#") || fields[0].Contains("Local"))
                    {
                        continue;
                    }
                    // OVDE NEDOSTAJE POLJE LOKALNO VREME IZ TABELE WEATHER,ONO CE NAM KASNIJE TREBATI ZA PRIKAZ
                    swm.LocalTime = DateTime.ParseExact(fields[0], "dd.MM.yyyy HH:mm", null);
                    float airTemp, stationPressure, reducedPressure;
                    swm.AirTemperature = float.TryParse(fields[1], out airTemp) ? airTemp : 0;
                    swm.StationPressure = float.TryParse(fields[2], out stationPressure) ? stationPressure : 0;
                    swm.ReducedPressure = float.TryParse(fields[3], out reducedPressure) ? reducedPressure : 0;
                    int humidity;
                    swm.Humidity = int.TryParse(fields[4], out humidity) ? humidity : 0;
                    swm.WindDirection = fields[5];
                    int windSpeed;
                    swm.WindSpeed = int.TryParse(fields[6], out windSpeed) ? windSpeed : 0;
                    swm.GustValue = fields[7] == "" ? 0 : int.Parse(fields[7]);
                    swm.PresentWeather = fields[8];
                    swm.RecentWeather = fields[9];
                    swm.CloudCover = fields[10];
                    int horizontalVisibility;
                    swm.HorizontalVisibility = int.TryParse(fields[11],out horizontalVisibility) ? horizontalVisibility : 0;
                    int devTemp;
                    swm.DevpointTemperature = int.TryParse(fields[12], out devTemp) ? devTemp : 0;

                    DataKeys keys = new DataKeys()
                    {
                        Name = DBLogic.GetShortStateName(state),
                        EndDate = swm.LocalTime,
                        StartDate = DateTime.MinValue
                    };

                    // if there is no data for that country at that time create new data for country
                    if(!CurrentData.Data.ContainsKey(keys)) 
                    {
                        CurrentData.Data.Add(keys, new StateInfoModel());
                    }
                    CurrentData.Data[keys].StateWeather = swm;

                }
            }

        }

        private static void LoadConsumption(string cf, string stateName, DateTime startDate, DateTime endDate)
        {
            using (TextFieldParser csvParser = new TextFieldParser(cf))
            {
                Dictionary<string, StateConsumptionModel> dictionary = new Dictionary<string, StateConsumptionModel>();


                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                csvParser.ReadLine();

                // make more efficient method for reading (binary search)
                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    StateConsumptionModel scm = new StateConsumptionModel();
                    string[] fields = csvParser.ReadFields();
                    
                    scm.DateUTC = new DateTime();
                    scm.DateShort = DateTime.ParseExact(fields[2], "M/d/yyyy", null);

                    // the next 2 is just hours
                    scm.DateFrom = DateTime.ParseExact(fields[3], "H:mm", null);
                    scm.DateTo = DateTime.ParseExact(fields[4], "H:mm", null);

                    scm.StateCode = fields[5];
                    int covRatio;
                    scm.CovRatio = int.TryParse(fields[6], out covRatio) ?covRatio : 0;
                    double value;
                    scm.Value = double.TryParse(fields[7], out value) ? value : 0;
                    double valueScale;
                    scm.ValueScale = double.TryParse(fields[8], out valueScale) ? valueScale : 0;

                    // if it is the right country and date
                    if (scm.StateCode == DBLogic.GetShortStateName(stateName) && startDate < scm.DateShort && endDate > scm.DateShort) 
                    {
                        DataKeys keys = new DataKeys()
                        {
                            Name = scm.StateCode,
                            StartDate = scm.DateFrom,
                            EndDate = scm.DateTo
                        };

                        if(!CurrentData.Data.ContainsKey(keys)) 
                        {
                            CurrentData.Data.Add(keys, new StateInfoModel());
                        }
                        CurrentData.Data[keys].StateConsumption = scm;
                    }
                }
            }
        }
        
        public static void Load(ImportParameters parameters) 
        {
            bool dataLoaded = false;

            if(!String.IsNullOrEmpty(parameters.WeatherFile)) 
            {
                ImportData.LoadWeather(parameters.WeatherFile);
                dataLoaded = true;
            }

            if(
                !String.IsNullOrEmpty(parameters.ConsumptionFile) && !String.IsNullOrEmpty(parameters.StateName) &&
                parameters.StartDate != null && parameters.EndDate != null
                )
            {
                ImportData.LoadConsumption(parameters.ConsumptionFile, parameters.StateName, (DateTime)parameters.StartDate, (DateTime)parameters.EndDate);
                dataLoaded = true;
            }

            if(dataLoaded) 
            {
                DBLogic.AddOrUpdateMoreStates(CurrentData.Data.Values);
            }
        }   
    }
}
