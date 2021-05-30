using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SharedModels;
using EntityFramework.Extensions;
using SharedModels.HelperClasses;
using System.Data.Entity.Validation;

namespace DatabaseAccess
{
    public class DBAccess : IDBAccess
    {

        public DBAccess() { }

        /// <summary>
        /// Using methods as static, but they can't be static, because they need to implement interface
        /// </summary>
        /// <param name="model"></param>

        #region DatabaseActions
        public void AddOrUpdateState(StateInfoModel model) 
        {
            // if given state is not valid
            if (!model.IsValid())
                throw new Exception("StateInfoModel is not valid.");

            using (var db = new StatesDB())
            {
                
                // if stateInfo doesn't exist
                if(!IfStateExistByName(model.StateName, db)) 
                {

                    var newState = DBAccess.ConvertStateDBNew(model);

                    db.States.Add(newState);
                }
                else
                {
                    //set to existing id
                    //var existingState = GetDBStateByName(model.StateName, db);
                    //existingState = ConvertStateDBExisting(model, existingState.stateID, existingState.stateConsumptionID, existingState.stateWeatherID);
                    //this.UpdateState(existingState, db);
                }

                db.SaveChanges();
            }
        }

        public void AddStateWeather(StateWeatherModel model)
        {
            using (var db = new StatesDB())
            {

                var newStateWeather = DBAccess.ConvertStateWeatherDBNew(model);

                db.StateWeathers.Add(newStateWeather);
                
                db.SaveChanges();
            }
        }

        public void RemoveAllStateWeatherModels()
        {
            using (var db = new StatesDB())
            {
                // if there is nothing in tables, do nothing
                if (db.StateWeathers.Count() != 0)
                {
                    var currWeather = db.StateWeathers;
                    db.StateWeathers.RemoveRange(currWeather);

                    db.SaveChanges();
                }
            }
        }

        public bool IfStateExistByName(string name, StatesDB db) 
        {
            return db.States.Where(x => x.stateName == name).Count() != 0;
        }

        public State GetDBStateByName(string name, StatesDB db) 
        {
            return db.States.Where(x => x.stateName == name).FirstOrDefault();
        }

        public void UpdateState(State state, StatesDB db)
        {
            var query = db.States.Where(s => s.stateID == state.stateID);
            State fs = query.FirstOrDefault();

            db.States.Remove(fs);
            db.States.Add(state);
        }

        public void RemoveState(StateInfoModel model) 
        {

            using (var db = new StatesDB())
            {
                var currState = DBAccess.ConvertStateDBNew(model);

                db.States.Remove(currState);

                db.SaveChanges();
            }
        }

        public void RemoveAllStates() 
        {
            using (var db = new StatesDB())
            {
               
                //var currStates = db.States;
                //db.States.RemoveRange(currStates);

                var currCons = db.StateConsumptions;
                db.StateConsumptions.RemoveRange(currCons);

                var currWeather = db.StateWeathers;
                db.StateWeathers.RemoveRange(currWeather);

                db.SaveChanges();
            }
        }

        public StateInfoModel GetStateByName(string name) 
        {
            StateInfoModel ret_val = null;

            using (var db = new StatesDB())
            {

                var state = db.States.Where(x => x.stateName == name).FirstOrDefault();

                //if (state.Count() == 0)
                //    throw new Exception("There is no state data with that name.");

                ret_val = DBAccess.ConvertStateModel(state);
            }

            return ret_val;
        }

        public void AddStateConsumption(StateConsumptionModel model)
        {
            using (var db = new StatesDB())
            {

                var newStateConsumption = DBAccess.ConvertStateConsumptionDBNew(model);

                db.StateConsumptions.Add(newStateConsumption);

                db.SaveChanges();
            }
        }
        public StateConsumptionModel GetStateConsumptionByStateName(string name) 
        {
            StateConsumptionModel retVal = null;

            //using (var db = new StatesDB())
            //{
            //    var state = db.States.Where(x => x.stateName == name);

            //    if (state.Count() == 0)
            //        throw new Exception("There is no state weather data with that name.");

            //    retVal = DBAccess.ConvertStateConsumptionModel(state.First().StateConsumption);
            //}

            return retVal;
        }

        public StateWeatherModel GetStateWeatherByStateName(string name) 
        {
            StateWeatherModel retVal = null;

            //using (var db = new StatesDB())
            //{
            //    var state = db.States.Where(x => x.stateName == name);

            //    if (state.Count() == 0)
            //        throw new Exception("There is no state weather data with that name.");

            //    retVal = DBAccess.ConvertStateWeatherModel(state.First().StateWeather);
            //}

            return retVal;
        }

        public List<StateInfoModel> GetAllStates() 
        {
            List<StateInfoModel> ret_val = new List<StateInfoModel>();

            using (var db = new StatesDB())
            {
                foreach (var dbS in db.States)
                {
                    ret_val.Add(ConvertStateModel(dbS));
                }
            }

            return ret_val;
        }

        #endregion

        #region ConvertingToDatabaseModel
        private static State ConvertStateDBNew(StateInfoModel model) 
        {
            return new State()
            {
                stateName = model.StateName,
            };
        }

        private static StateWeather ConvertStateWeatherDBNew(StateWeatherModel model) 
        {
            return new StateWeather()
            {
                airTemperature = model.AirTemperature,
                cloudCover = model.CloudCover,
                devpointTemperature = model.DevpointTemperature,
                gustValue = model.GustValue,
                horizontalVisibility = model.HorizontalVisibility,
                humidity = model.Humidity,
                presentWeather = model.PresentWeather,
                recentWeather = model.RecentWeather,
                reducedPressure = model.ReducedPressure,
                stationPressure = model.StationPressure,
                windDirection = model.WindDirection,
                windSpeed = model.WindSpeed,
                localTime = model.LocalTime,
                stateID = model.StateId,
                //stateWeatherID = DBAccess.stateWeatherID++
            };
        }

        private static StateConsumption ConvertStateConsumptionDBNew(StateConsumptionModel stateConsumptionModel) 
        {
            return
                new StateConsumption()
                {
                    covRation = stateConsumptionModel.CovRatio,
                    dateFrom = stateConsumptionModel.DateFrom,
                    dateShort = stateConsumptionModel.DateShort,
                    dateTo = stateConsumptionModel.DateTo,
                    dateUTC = stateConsumptionModel.DateUTC,
                    stateCode = stateConsumptionModel.StateCode,
                    value = stateConsumptionModel.Value,
                    valueScale = stateConsumptionModel.ValueScale,
                    stateID = stateConsumptionModel.StateId,
                    //stateConsumptionID = DBAccess.stateConsumptionID++
                };
        }
        
        private static State ConvertStateDBExisting(StateInfoModel model, int stateID)
        {

            return new State()
            {
                stateName = model.StateName,
                stateID = stateID,
            };
        }

        private static StateWeather ConvertStateWeatherDBExisting(StateWeatherModel model, int id)
        {
            return new StateWeather()
            {
                airTemperature = model.AirTemperature,
                cloudCover = model.CloudCover,
                devpointTemperature = model.DevpointTemperature,
                gustValue = model.GustValue,
                horizontalVisibility = model.HorizontalVisibility,
                humidity = model.Humidity,
                presentWeather = model.PresentWeather,
                recentWeather = model.RecentWeather,
                reducedPressure = model.ReducedPressure,
                stationPressure = model.StationPressure,
                windDirection = model.WindDirection,
                windSpeed = model.WindSpeed,
                localTime = model.LocalTime,
                stateWeatherID = id
            };
        }

        private static StateConsumption ConvertStateConsumptionDBExisting(StateConsumptionModel stateConsumptionModel, int id) 
        {
            return
            new StateConsumption()
            {
                covRation = stateConsumptionModel.CovRatio,
                dateFrom = stateConsumptionModel.DateFrom,
                dateShort = stateConsumptionModel.DateShort,
                dateTo = stateConsumptionModel.DateTo,
                dateUTC = stateConsumptionModel.DateUTC,
                stateCode = stateConsumptionModel.StateCode,
                value = stateConsumptionModel.Value,
                valueScale = stateConsumptionModel.ValueScale,
                stateConsumptionID = id
            };
        }

        #endregion

        #region ConvertingToMVCModel
        private static StateInfoModel ConvertStateModel(State dbModel)
        {
            return new StateInfoModel()
            {
                StateName = dbModel.stateName,
                StateId = dbModel.stateID,
                
            };
        }

        private static StateWeatherModel ConvertStateWeatherModel(StateWeather dbModel) 
        {
            return new StateWeatherModel()
            {
                AirTemperature = (float)dbModel.airTemperature,
                CloudCover = dbModel.cloudCover,
                DevpointTemperature = (int)dbModel.devpointTemperature,
                GustValue = (int)dbModel.gustValue,
                HorizontalVisibility = (int)dbModel.horizontalVisibility,
                Humidity = (int)dbModel.humidity,
                PresentWeather = (string)dbModel.presentWeather,
                RecentWeather = (string)dbModel.recentWeather,
                ReducedPressure = (float)dbModel.reducedPressure,
                StationPressure = (float)dbModel.stationPressure,
                WindDirection = (string)dbModel.windDirection,
                WindSpeed = (int)dbModel.windSpeed,
                LocalTime = (DateTime)dbModel.localTime
            };
        }

        private static StateConsumptionModel ConvertStateConsumptionModel(StateConsumption dbModel) 
        {
            return new StateConsumptionModel()
            {
                CovRatio = (int)dbModel.covRation,
                DateFrom = (DateTime)dbModel.dateFrom,
                DateShort = (DateTime)dbModel.dateShort,
                DateTo = (DateTime)dbModel.dateTo,
                DateUTC = (DateTime)dbModel.dateUTC,
                StateCode = (string)dbModel.stateCode,
                Value = (double)dbModel.value,
                ValueScale = (double)dbModel.valueScale
            };
        }
        #endregion

        #region ShortNamesForStates
        // remove after all have db updated
        public static void AddShortStateNames(Dictionary<string, string> shortNames) 
        {
            using (var db = new StatesDB())
            {
                foreach (var sn in shortNames)
                {
                    try
                    {
                        var ssn = new shortStateName();
                        ssn.shortName = sn.Key;
                        ssn.fullName = sn.Value;

                        if (ssn.shortName.Length > 2) continue;

                        db.shortStateNames.Add(ssn);
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }

                db.SaveChanges();
            }
        }

        public String GetShortStateName(String fullStateName) 
        {
            String retVal = String.Empty;

            using (var db = new StatesDB())
            {
                var state = db.shortStateNames.Where(x => x.fullName == fullStateName);

                if (state.Count() == 0)
                    throw new Exception("No short name for that state.");

                retVal = state.First().shortName;
            }

            return retVal;
        }

        public String GetFullStateName(String shortStateName) 
        {
            String retVal = String.Empty;

            using (var db = new StatesDB())
            {
                var state = db.shortStateNames.Where(x => x.shortName == shortStateName);

                if (state.Count() == 0)
                    throw new Exception("No full name for that state.");

                retVal = state.First().fullName;
            }

            return retVal;

        }

        #endregion
    }
}
