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
    public class ImportData : IImportData
    {
        private DBLogic dBLogic = new DBLogic();

        public List<StateWeatherModel> LoadWeather(string wf, StateInfoModel state, DateTime StartDate, DateTime EndDate)
        {
            using (TextFieldParser csvParser = new TextFieldParser(wf))
            {
                //csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                string stateInformation = csvParser.ReadLine();
                string[] states = stateInformation.Split(',');
                stateInformation = states[1].Substring(1);


                //csvParser.ReadLine();

                List<StateWeatherModel> stateWeatherModels = new List<StateWeatherModel>();

                while (!csvParser.EndOfData)
                {
                    StateWeatherModel swm = new StateWeatherModel();
                    string[] fields = csvParser.ReadFields();
                    if (fields[0].StartsWith("#") || fields[0].Contains("Local"))
                    {
                        continue;
                    }
                    swm.LocalTime = DateTime.ParseExact(fields[0], "dd.MM.yyyy HH:mm", null);

                    if (swm.LocalTime <= StartDate || swm.LocalTime >= EndDate) continue;

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
                    swm.HorizontalVisibility = int.TryParse(fields[11], out horizontalVisibility) ? horizontalVisibility : 0;
                    int devTemp;
                    swm.DevpointTemperature = int.TryParse(fields[12], out devTemp) ? devTemp : 0;
                    swm.StateId = state.StateId;

                    stateWeatherModels.Add(swm);
                }

                dBLogic.AddStateWeather(stateWeatherModels, stateInformation);
                return stateWeatherModels;
            }
        }


        public List<StateConsumptionModel> LoadConsumption(string cf, StateInfoModel state, DateTime startDate, DateTime endDate)
        {
            using (TextFieldParser csvParser = new TextFieldParser(cf))
            {
                Dictionary<string, StateConsumptionModel> dictionary = new Dictionary<string, StateConsumptionModel>();


                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                csvParser.ReadLine();

                List<StateConsumptionModel> stateConsumptionModels = new List<StateConsumptionModel>();

                String stateName = String.Empty;

                // make more efficient method for reading (binary search)
                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    StateConsumptionModel scm = new StateConsumptionModel();
                    string[] fields = csvParser.ReadFields();
                    scm.DateUTC = DateTime.UtcNow;
                    scm.DateShort = DateTime.ParseExact(fields[2], "M/d/yyyy", null);
                    if (scm.DateShort >= endDate || scm.DateShort < startDate)
                        continue;

                    // the next 2 is just hours
                    scm.DateFrom = DateTime.ParseExact(fields[3], "H:mm", null);
                    scm.DateTo = DateTime.ParseExact(fields[4], "H:mm", null);

                    scm.StateCode = fields[5];

                    if (String.IsNullOrEmpty(stateName))
                        stateName = dBLogic.GetFullStateName(scm.StateCode);

                    int covRatio;
                    scm.CovRatio = int.TryParse(fields[6], out covRatio) ? covRatio : 0;
                    double value;
                    scm.Value = double.TryParse(fields[7], out value) ? value : 0;
                    double valueScale;
                    scm.ValueScale = double.TryParse(fields[8], out valueScale) ? valueScale : 0;

                    scm.StateId = state.StateId;

                    stateConsumptionModels.Add(scm);
                }

                dBLogic.AddStateConsumptions(stateConsumptionModels, stateName);
                return stateConsumptionModels;
            }
        }

        public void Load(ImportParameters parameters)
        {

            dBLogic.RemoveAllStates();

            StateInfoModel state = dBLogic.GetStateByName(parameters.StateName);

            if (!String.IsNullOrEmpty(parameters.WeatherFile))
            {
                (new ImportData()).LoadWeather(parameters.WeatherFile, state, (DateTime)parameters.StartDate, (DateTime)parameters.EndDate);
            }

            if (
                !String.IsNullOrEmpty(parameters.ConsumptionFile) && !String.IsNullOrEmpty(parameters.StateName) &&
                parameters.StartDate != null && parameters.EndDate != null
                )
            {
                (new ImportData()).LoadConsumption(parameters.ConsumptionFile, state, (DateTime)parameters.StartDate, (DateTime)parameters.EndDate);
            }
        }
    }
}
