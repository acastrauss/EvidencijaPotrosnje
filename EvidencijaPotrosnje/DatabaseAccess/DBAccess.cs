﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SharedModels;
using EntityFramework.Extensions;
using SharedModels.HelperClasses;

namespace DatabaseAccess
{
    public class DBAccess
    {
        private static int stateID = 0;
        private static int stateConsumptionID = 0;
        private static int stateWeatherID = 0;

        private static void SetIDs(StatesDB db) 
        {   
            var queryStateIDs = from s in db.States
                            orderby stateID
                            select stateID;

            var queryStateWIDs = from s in db.States
                                orderby stateWeatherID
                                select stateWeatherID;

            var queryStateCIDs = from s in db.States
                                orderby stateConsumptionID
                                select stateConsumptionID;

            if (queryStateIDs.Count() == 0)
                stateID = 0;
            else
                stateID = queryStateIDs.Max() + 1;

            if (queryStateWIDs.Count() == 0)
                stateWeatherID = 0;
            else
                stateWeatherID = queryStateWIDs.Max() + 1;

            if (queryStateCIDs.Count() == 0)
                stateConsumptionID = 0;
            else 
                stateConsumptionID = queryStateCIDs.Max() + 1;
            
        }

        #region DatabaseActions
        public static void AddOrUpdateState(StateInfoModel model) 
        {
            // if given state is not valid
            if (!model.IsValid())
                throw new Exception("StateInfoModel is not valid.");


            using (var db = new StatesDB())
            {
                
                // if stateInfo doesn't exist
                if(!IfStateExistByName(model.StateName, db)) 
                {
                    // so ID's would be set everytime
                    DBAccess.SetIDs(db);

                    var newState = DBAccess.ConvertStateDBNew(model);

                    db.StateConsumptions.Add(newState.StateConsumption);
                    db.StateWeathers.Add(newState.StateWeather);
                    db.States.Add(newState);
                }
                else
                {
                    // set to existing id
                    var existingState = GetDBStateByName(model.StateName, db);
                    existingState = ConvertStateDBExisting(model, existingState.stateID, existingState.stateConsumptionID, existingState.stateWeatherID);
                    DBAccess.UpdateState(existingState, db);
                }

                db.SaveChanges();
            }
        }

        private static bool IfStateExistByName(string name, StatesDB db) 
        {
            var query = from s in db.States
                        where s.stateName == name
                        select s;

            return query.Count() != 0;
        }

        private static State GetDBStateByName(string name, StatesDB db) 
        {
            var query = from s in db.States
                        where s.stateName == name
                        select s;

            return query.FirstOrDefault();
        }

        private static void UpdateState(State state, StatesDB db)
        {
            var query = db.States.Where(s => s.stateID == state.stateID);
            State fs = query.FirstOrDefault();

            var tempConsumption = fs.StateConsumption;
            var tempWeather = fs.StateWeather;

            db.States.Remove(fs);
            db.StateConsumptions.Remove(tempConsumption);
            db.StateWeathers.Remove(tempWeather);

            db.StateConsumptions.Add(state.StateConsumption);
            db.StateWeathers.Add(state.StateWeather);
            db.States.Add(state);
        }

        public static void RemoveState(StateInfoModel model) 
        {
            // no need for setting ID's since here they are not used
            // DBAccess.SetIDs();

            using (var db = new StatesDB())
            {
                var currState = DBAccess.ConvertStateDBNew(model);

                // if there is no given state throw exception
                if (!db.States.Contains(currState) || !db.StateConsumptions.Contains(currState.StateConsumption) || !db.StateWeathers.Contains(currState.StateWeather))
                    throw new Exception("Given StateInfo (or some of it's parts) doesn't exists.");

                db.States.Remove(currState);
                db.StateConsumptions.Remove(currState.StateConsumption);
                db.StateWeathers.Remove(currState.StateWeather);

                db.SaveChanges();
            }
        }

        public static void RemoveAllStates() 
        {
            using (var db = new StatesDB())
            {
                // if there is nothing in tables, do nothing
                if(db.States.Count() != 0 && db.StateWeathers.Count() != 0 && db.StateConsumptions.Count() != 0)
                {
                    var currStates = db.States;
                    db.States.RemoveRange(currStates);

                    var currCons = db.StateConsumptions;
                    db.StateConsumptions.RemoveRange(currCons);

                    var currWeather = db.StateWeathers;
                    db.StateWeathers.RemoveRange(currWeather);

                    db.SaveChanges();
                }
            }
        }

        public static StateInfoModel GetStateByName(string name) 
        {
            StateInfoModel ret_val = null;

            using (var db = new StatesDB())
            {
                var query = from s in db.States
                            where s.stateName == name
                            select s;

                if (query.Count() == 0)
                    throw new Exception("There is no state data with that name.");

                ret_val = DBAccess.ConvertStateModel(query.FirstOrDefault());
            }

            return ret_val;
        }

        public static StateConsumptionModel GetStateConsumptionByStateName(string name) 
        {
            StateConsumptionModel ret_val = null;

            using (var db = new StatesDB())
            {
                var query = from s in db.States
                            where s.stateName == name
                            select s;

                if (query.Count() == 0)
                    throw new Exception("There is no state consumption data with that name.");

                ret_val = DBAccess.ConvertStateModel(query.FirstOrDefault()).StateConsumption;
            }

            return ret_val;
        }

        public static StateWeatherModel GetStateWeatherByStateName(string name) 
        {
            StateWeatherModel ret_val = null;

            using (var db = new StatesDB())
            {
                var query = from s in db.States
                            where s.stateName == name
                            select s;

                if (query.Count() == 0)
                    throw new Exception("There is no state weather data with that name.");

                ret_val = DBAccess.ConvertStateModel(query.FirstOrDefault()).StateWeather;
            }

            return ret_val;
        }

        public static List<StateInfoModel> GetAllStates() 
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

        public static Dictionary<DataKeys, StateInfoModel> GetStatesForDate(DateTime startDate, DateTime endDate) 
        {
            return new Dictionary<DataKeys, StateInfoModel>();
        }

        #endregion

        #region ConvertingToDatabaseModel
        private static State ConvertStateDBNew(StateInfoModel model) 
        {
            var sc = DBAccess.ConvertStateConsumptionDBNew(model.StateConsumption);
            var sw = DBAccess.ConvertStateWeatherDBNew(model.StateWeather);

            return new State()
            {
                stateName = model.StateName,
                stateID = DBAccess.stateID++,
                StateConsumption = sc,
                StateWeather = sw,
                stateConsumptionID = sc.stateConsumptionID,
                stateWeatherID = sw.stateWeatherID
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
                stateWeatherID = DBAccess.stateWeatherID++
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
                    stateConsumptionID = DBAccess.stateConsumptionID++
                };
        }
        
        private static State ConvertStateDBExisting(StateInfoModel model, int stateID, int consumptionID, int weatherID)
        {
            var sc = DBAccess.ConvertStateConsumptionDBExisting(model.StateConsumption, consumptionID);
            var sw = DBAccess.ConvertStateWeatherDBExisting(model.StateWeather, weatherID);

            return new State()
            {
                stateName = model.StateName,
                stateID = stateID,
                StateConsumption = sc,
                StateWeather = sw,
                stateConsumptionID = sc.stateConsumptionID,
                stateWeatherID = sw.stateWeatherID
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
                StateWeather = DBAccess.ConvertStateWeatherModel(dbModel.StateWeather),
                StateConsumption = DBAccess.ConvertStateConsumptionModel(dbModel.StateConsumption),
                StateName = dbModel.stateName
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


    }
}
