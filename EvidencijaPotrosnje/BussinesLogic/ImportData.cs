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
                    if (fields[0].StartsWith("#"))
                    {
                        continue;
                    }
                    // OVDE NEDOSTAJE POLJE LOKALNO VREME IZ TABELE WEATHER,ONO CE NAM KASNIJE TREBATI ZA PRIKAZ
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

                    DataKeys keys = new DataKeys()
                    {
                        Name = CountriesDictionary.CountriesShort[state],
                        DateInfo = swm.LocalTime
                    };

                    // if there is no data for that country at that time create new data for country
                    if(!CurrentData.data.ContainsKey(keys)) 
                    {
                        CurrentData.data.Add(keys, new StateInfoModel());
                    }
                    CurrentData.data[keys].StateWeather = swm;

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
                    
                    scm.DateUTC = DateTime.Parse(fields[1]);
                    scm.DateShort = DateTime.Parse(fields[2]);
                    
                    // the next 2 is just hours
                    scm.DateFrom = DateTime.Parse(fields[3]);
                    scm.DateTo = DateTime.Parse(fields[4]);

                    scm.StateCode = fields[5];
                    scm.CovRatio = int.Parse(fields[6]);
                    scm.Value = double.Parse(fields[7]);
                    scm.ValueScale = double.Parse(fields[8]);

                    // if it is the right country and date
                    if(scm.StateCode == CountriesDictionary.CountriesShort[stateName] && startDate < scm.DateShort && endDate > scm.DateShort) 
                    {
                        DataKeys keys = new DataKeys()
                        {
                            Name = scm.StateCode,
                            DateInfo = scm.DateShort
                        };

                        if(!CurrentData.data.ContainsKey(keys)) 
                        {
                            CurrentData.data.Add(keys, new StateInfoModel());
                        }
                        CurrentData.data[keys].StateConsumption = scm;
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
                DBLogic.AddOrUpdateMoreStates(CurrentData.data.Values);
            }
        }   
    }
}
