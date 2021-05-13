using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SharedModels;

namespace DatabaseAccess
{
    public class DBAccess
    {
        private static int stateID = 0;
        private static int stateConsumptionID = 0;
        private static int stateWeatherID = 0;

        private static void SetIDs() 
        {
            using (var db = new StatesDB())
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
        }

        #region DatabaseActions
        public static void AddState(StateInfoModel model) 
        {
            // so ID's would be set everytime
            DBAccess.SetIDs();

            using (var db = new StatesDB())
            {
                var currState = DBAccess.ConvertStateDB(model);

                db.StateConsumptions.Add(currState.StateConsumption);
                db.StateWeathers.Add(currState.StateWeather);
                db.States.Add(currState);

                db.SaveChanges();
            }
        }
        
        public static void RemoveState(StateInfoModel model) 
        {
            // no need for setting ID's since here they are not used
            // DBAccess.SetIDs();

            using (var db = new StatesDB())
            {
                var currState = DBAccess.ConvertStateDB(model);

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
                var currStates = db.States;
                db.States.RemoveRange(currStates);

                var currCons = db.StateConsumptions;
                db.StateConsumptions.RemoveRange(currCons);

                var currWeather = db.StateWeathers;
                db.StateWeathers.RemoveRange(currWeather);

                db.SaveChanges();
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

        #endregion

        #region ConvertingToDatabaseModel
        private static State ConvertStateDB(StateInfoModel model) 
        {
            var sc = DBAccess.ConvertStateConsumptionDB(model.StateConsumption);
            var sw = DBAccess.ConvertStateWeatherDB(model.StateWeather);

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

        private static StateWeather ConvertStateWeatherDB(StateWeatherModel model) 
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
                stateWeatherID = DBAccess.stateWeatherID++
            };
        }

        private static StateConsumption ConvertStateConsumptionDB(StateConsumptionModel stateConsumptionModel) 
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
                WindSpeed = (int)dbModel.windSpeed
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
