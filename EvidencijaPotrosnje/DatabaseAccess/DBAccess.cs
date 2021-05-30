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
using System.Linq.Dynamic;

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

        public bool IfStateExistByName(string name, StatesDB db)
        {
            return db.States.Where(x => x.stateName == name).Count() != 0;
        }

        public void AddState(StateInfoModel model) 
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
                    throw new Exception("State with that name already exists.");
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

        public void AddStateConsumption(StateConsumptionModel model)
        {
            using (var db = new StatesDB())
            {
                var newStateConsumption = DBAccess.ConvertStateConsumptionDBNew(model);

                db.StateConsumptions.Add(newStateConsumption);

                db.SaveChanges();
            }
        }

        public void RemoveState(String stateName)
        {
            using (var db = new StatesDB())
            {
                var currState = db.States.Where(x => x.stateName == stateName).FirstOrDefault();

                db.States.Remove(currState);

                currState.StateConsumptions.Clear();
                currState.StateWeathers.Clear();

                db.States.Add(currState);

                db.SaveChanges();
            }
        }

        public void RemoveStateTotally(String stateName)
        {
            using (var db = new StatesDB())
            {
                var currState = db.States.Where(x => x.stateName == stateName).FirstOrDefault();

                db.States.Remove(currState);

                db.SaveChanges();
            }
        }
        
        public void RemoveAllStates()
        {
            using (var db = new StatesDB())
            {
                var currCons = db.StateConsumptions;
                db.StateConsumptions.RemoveRange(currCons);

                var currWeather = db.StateWeathers;
                db.StateWeathers.RemoveRange(currWeather);

                db.SaveChanges();
            }
        }

        public void RemoveAllStatesTotally()
        {
            using (var db = new StatesDB())
            {   
                var currCons = db.StateConsumptions;
                db.StateConsumptions.RemoveRange(currCons);

                var currWeather = db.StateWeathers;
                db.StateWeathers.RemoveRange(currWeather);

                var currStates = db.States;
                db.States.RemoveRange(currStates);

                db.SaveChanges();
            }
        }

        public void RemoveStateWeathers(String stateName) 
        {
            using (var db = new StatesDB())
            {
                db.StateWeathers.RemoveRange(
                    db.StateWeathers.Where(x => x.State.stateName == stateName)
                    );

                db.SaveChanges();
            }
        }

        public void RemoveStateConsumption(String stateName)
        {
            using (var db = new StatesDB())
            {
                db.StateConsumptions.RemoveRange(
                    db.StateConsumptions.Where(x => x.State.stateName == stateName)
                    );

                db.SaveChanges();
            }
        }

        public void RemoveStateWeathersByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            using (var db = new StatesDB())
            {
                db.StateWeathers.RemoveRange(
                    db.StateWeathers.Where(x => (x.localTime >= startDate && x.localTime <= endDate && x.State.stateName == stateName))
                    );

                db.SaveChanges();
            }
        }
        public void RemoveStateConsumptionsByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            using (var db = new StatesDB())
            {
                db.StateConsumptions.RemoveRange(
                    db.StateConsumptions.Where(x => (x.dateFrom >= startDate && x.dateTo <= endDate && x.State.stateName == stateName))
                    ) ;

                db.SaveChanges();
            }

        }
        /// <summary>
        /// Removes every data for given date
        /// </summary>
        public void RemoveStateByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            using (var db = new StatesDB())
            {
                db.StateConsumptions.RemoveRange(
                    db.StateConsumptions.Where(x => x.State.stateName == stateName));

                db.StateWeathers.RemoveRange(
                    db.StateWeathers.Where(x => x.State.stateName == stateName));

                db.SaveChanges();
            }
        }

        public StateInfoModel GetStateByName(String name) 
        {
            StateInfoModel retVal = null;

            using (var db = new StatesDB())
            {
                var state = db.States.Where(x => x.stateName == name);

                if (state.Count() == 0)
                    throw new Exception("There is no state data with that name.");

                retVal = DBAccess.ConvertStateModel(state.First());
            }

            return retVal;
        }

        public IEnumerable<StateConsumptionModel> GetStateConsumptionByStateName(String name) 
        {
            List<StateConsumptionModel> retVal = null;

            using (var db = new StatesDB())
            {
                var state = db.States.Where(x => x.stateName == name);

                if (state.Count() == 0)
                    throw new Exception("There is no state weather data with that name.");

                var listStateC = state.First().StateConsumptions;

                foreach (var sc in listStateC)
                {
                    retVal.Add(DBAccess.ConvertStateConsumptionModel(sc));
                }
            }
            return retVal;
        }

        public IEnumerable<StateWeatherModel> GetStateWeatherByStateName(String name) 
        {
            List<StateWeatherModel> retVal = null;

            using (var db = new StatesDB())
            {
                var state = db.States.Where(x => x.stateName == name);

                if (state.Count() == 0)
                    throw new Exception("There is no state weather data with that name.");

                var listStateW = state.First().StateWeathers;

                foreach (var sw in listStateW)
                {
                    retVal.Add(DBAccess.ConvertStateWeatherModel(sw));
                }
            }
            return retVal;
        }

        public IEnumerable<StateInfoModel> GetAllStates() 
        {
            List<StateInfoModel> retVal = new List<StateInfoModel>();

            using (var db = new StatesDB())
            {
                foreach (var dbS in db.States)
                {
                    retVal.Add(ConvertStateModel(dbS));
                }
            }
            return retVal;
        }

        public StateInfoModel GetStateByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            StateInfoModel retVal = new StateInfoModel();

            using (var db = new StatesDB())
            {
                var stateByDate = db.States.Where(x => x.stateName == stateName).FirstOrDefault();
                stateByDate.StateConsumptions.Clear();
                stateByDate.StateWeathers.Clear();

                stateByDate.StateConsumptions = db.StateConsumptions.Where(x => x.State.stateName == stateByDate.stateName && x.dateFrom >= startDate && x.dateTo <= endDate).ToList();
                stateByDate.StateWeathers = db.StateWeathers.Where(x => x.State.stateName == stateByDate.stateName && x.localTime >= startDate && x.localTime <= endDate).ToList();

                retVal = DBAccess.ConvertStateModel(stateByDate);
            }
            return retVal;
        }
        public IEnumerable<StateConsumption> GetStateConsumptionsByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            List<StateConsumption> retVal = new List<StateConsumption>();

            using (var db = new StatesDB())
            {
                retVal = db.StateConsumptions.Where(x => x.State.stateName == stateName && x.dateFrom >= startDate && x.dateTo <= endDate).ToList();
            }

            return retVal;
        }
        public IEnumerable<StateWeather> GetStateWeathersByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            List<StateWeather> retVal = new List<StateWeather>();

            using (var db = new StatesDB())
            {
                retVal = db.StateWeathers.Where(x => x.State.stateName == stateName && x.localTime >= startDate && x.localTime <= endDate).ToList();
            }

            return retVal;
        }
        #endregion

        #region ConvertingToDatabaseModel
        private static State ConvertStateDBNew(StateInfoModel model) 
        {
            return new State()
            {
                stateName = model.StateName,
                StateConsumptions = (ICollection<StateConsumption>)model.StateConsumption,
                StateWeathers = (ICollection<StateWeather>)model.StateWeathers
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
                stateID = model.StateId
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
                    stateID = stateConsumptionModel.StateId
                    //stateConsumptionID = DBAccess.stateConsumptionID++
                };
        }
        
        private static State ConvertStateDBExisting(StateInfoModel model, int stateID)
        {

            return new State()
            {
                stateName = model.StateName,
                stateID = stateID,
                StateConsumptions = (ICollection<StateConsumption>)model.StateConsumption,
                StateWeathers = (ICollection<StateWeather>)model.StateWeathers
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

        #region ShortNamesForStates
        
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
