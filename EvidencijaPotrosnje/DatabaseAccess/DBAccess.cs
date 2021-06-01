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
using System.Collections.ObjectModel;

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

        public bool IfStateExistByName(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new Exception("State name can not be null or empty.");

            bool retVal = false;

            using (var db = new StatesDB())
            {
                retVal = db.States.Where(x => x.stateName == name).Count() != 0;
            }

            return retVal;
        }

        public void AddStates(IEnumerable<StateInfoModel> models)
        {
            using (var db = new StatesDB())
            {
                foreach (var m in models)
                {
                    // ako vec postoji data drzava ili je nevalidna
                    if (!m.IsValid() || db.States.Where(x => x.stateName == m.StateName).Count() != 0)
                    {
                        continue;
                    }
                    else
                    {
                        var state = DBAccess.ConvertStateDBNew(m);
                        db.States.Add(state);
                    }
                }

                db.SaveChanges();
            }
        }

        private int GetStateID(String stateNme)
        {
            if (String.IsNullOrEmpty(stateNme))
                throw new Exception("State name can not be null or empty.");

            int retVal = 0;

            using (var db = new StatesDB())
            {
                var state = db.States.Where(x => x.stateName == stateNme);

                if (state.Count() == 0)
                    throw new Exception("There is no ID for given state name.");

                retVal = state.FirstOrDefault().stateID;
            }

            return retVal;
        }

        private State GetStateForID(int id)
        {
            State retVal = new State();

            using (var db = new StatesDB())
            {
                var state = db.States.Where(x => x.stateID == id);

                if (state.Count() == 0)
                    throw new Exception("There is no state for given ID.");

                retVal = state.FirstOrDefault();
            }

            return retVal;
        }

        private async Task<int> AddWeathersToDBThread(IEnumerable<StateWeatherModel> stateWeatherModels, StatesDB db, int id)
        {
            foreach (var swm in stateWeatherModels)
            {
                var dbmodel = DBAccess.ConvertStateWeatherDBNew(swm);
                dbmodel.stateID = id;
                //dbmodel.State = db.States.Where(x => x.stateID == id).FirstOrDefault();
                db.StateWeathers.Add(dbmodel);

            }

            return await db.SaveChangesAsync();
        }

        public void AddStateWeathers(IEnumerable<StateWeatherModel> models, String stateName)
        {
            int stateId = this.GetStateID(stateName);

            List<StateWeatherModel> modelsList = models.ToList();
            // delete this
            modelsList.Reverse();

            int listSize = 5000;
            List<Task<int>> dbTasks = new List<Task<int>>();

            int numT = models.Count() / listSize;

            using (var db = new StatesDB())
            {
                db.Configuration.AutoDetectChangesEnabled = false;

                for (int i = 0; i < numT; i++)
                {
                    var sublist = modelsList.GetRange(i * listSize, listSize);

                    dbTasks.Add(AddWeathersToDBThread(sublist, db, stateId));

                }

            }
        }

        private async Task<int> AddConsumptionToDBThread(IEnumerable<StateConsumptionModel> stateConsumptioModels, StatesDB db, int id)
        {
            foreach (var scm in stateConsumptioModels)
            {
                var dbmodel = DBAccess.ConvertStateConsumptionDBNew(scm);
                dbmodel.stateID = id;
                //dbmodel.State = db.States.Where(x => x.stateID == id).FirstOrDefault();
                db.StateConsumptions.Add(dbmodel);
            }

            return await db.SaveChangesAsync();
        }

        public void AddStateConsumption(IEnumerable<StateConsumptionModel> models, String stateName)
        {
            int stateId = this.GetStateID(stateName);

            List<StateConsumptionModel> modelsList = models.ToList();

            int listSize = 5000;
            List<Task<int>> dbTasks = new List<Task<int>>();

            int numT = models.Count() / listSize;

            using (var db = new StatesDB())
            {

                db.Configuration.AutoDetectChangesEnabled = false;

                for (int i = 0; i < numT; i++)
                {
                    var sublist = modelsList.GetRange(i * listSize, listSize);

                    dbTasks.Add(AddConsumptionToDBThread(sublist, db, stateId));
                }
            }
        }

        public void RemoveState(String stateName)
        {
            using (var db = new StatesDB())
            {
                var currStateQ = db.States.Where(x => x.stateName == stateName);

                if (currStateQ.Count() == 0)
                    throw new Exception("There is no state with that name.");

                var currState = currStateQ.FirstOrDefault();

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
                var currStateQ = db.States.Where(x => x.stateName == stateName);

                if (currStateQ.Count() == 0)
                    throw new Exception("There is no state with that name.");

                var currState = currStateQ.FirstOrDefault();

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
                var statesWQ = db.StateWeathers.Where(x => x.State.stateName == stateName);

                if (statesWQ.Count() == 0)
                    throw new Exception("There is no state with that name.");

                db.StateWeathers.RemoveRange(statesWQ);

                db.SaveChanges();
            }
        }

        public void RemoveStateConsumption(String stateName)
        {
            using (var db = new StatesDB())
            {
                var statesCQ = db.StateConsumptions.Where(x => x.State.stateName == stateName);

                if (statesCQ.Count() == 0)
                    throw new Exception("There is no state with that name.");

                db.StateConsumptions.RemoveRange(statesCQ);

                db.SaveChanges();
            }
        }

        public void RemoveStateWeathersByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            using (var db = new StatesDB())
            {
                var statesWQ = db.StateWeathers.Where(x => (x.localTime >= startDate && x.localTime <= endDate && x.State.stateName == stateName));

                if (statesWQ.Count() == 0)
                    throw new Exception("There is no state that this criteria applies to.");

                db.StateWeathers.RemoveRange(statesWQ);

                db.SaveChanges();
            }
        }
        public void RemoveStateConsumptionsByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            using (var db = new StatesDB())
            {
                db.StateConsumptions.RemoveRange(
                    db.StateConsumptions.Where(x => (x.dateFrom >= startDate && x.dateTo <= endDate && x.State.stateName == stateName))
                    );

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
                    db.StateConsumptions.Where(x => x.State.stateName == stateName && x.dateFrom >= startDate && x.dateTo <= endDate));

                db.StateWeathers.RemoveRange(
                    db.StateWeathers.Where(x => x.State.stateName == stateName && x.localTime >= startDate && x.localTime <= endDate));

                db.SaveChanges();
            }
        }

        public StateInfoModel GetStateByName(String name)
        {
            if (String.IsNullOrEmpty(name))
                throw new Exception("State name can not be null.");

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
            if (String.IsNullOrEmpty(name))
                throw new Exception("State name can not be null or empty.");

            List<StateConsumptionModel> retVal = new List<StateConsumptionModel>();

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
            if (String.IsNullOrEmpty(name))
                throw new Exception("State name can not be null or empty.");

            List<StateWeatherModel> retVal = new List<StateWeatherModel>();

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
                    var state = ConvertStateModel(dbS);

                    foreach (var sc in dbS.StateConsumptions)
                    {
                        state.StateConsumption.Add(DBAccess.ConvertStateConsumptionModel(sc));
                    }

                    foreach (var sw in dbS.StateWeathers)
                    {
                        state.StateWeathers.Add(DBAccess.ConvertStateWeatherModel(sw));
                    }

                    retVal.Add(state);
                }
            }
            return retVal;
        }

        public StateInfoModel GetStateByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            if (String.IsNullOrEmpty(stateName))
                throw new Exception("State name can not be null or empty.");

            StateInfoModel retVal = new StateInfoModel();

            using (var db = new StatesDB())
            {
                var statesQ = db.States.Where(x => x.stateName == stateName);

                if (statesQ.Count() == 0)
                    throw new Exception("There is no state with that name.");

                var stateByDate = statesQ.FirstOrDefault();
                stateByDate.StateConsumptions.Clear();
                stateByDate.StateWeathers.Clear();

                stateByDate.StateConsumptions = db.StateConsumptions.Where(x => x.dateFrom >= startDate && x.dateTo <= endDate).ToList();
                stateByDate.StateWeathers = db.StateWeathers.Where(x => x.localTime >= startDate && x.localTime <= endDate).ToList();

                retVal = DBAccess.ConvertStateModel(stateByDate);
            }
            return retVal;
        }
        public IEnumerable<StateConsumptionModel> GetStateConsumptionsByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            //if(String.IsNullOrEmpty(stateName))


            List<StateConsumptionModel> retVal = new List<StateConsumptionModel>();

            using (var db = new StatesDB())
            {
                var listC = db.StateConsumptions.Where(x => x.State.stateName == stateName && x.dateFrom >= startDate && x.dateTo <= endDate).ToList();

                foreach (var c in listC)
                {
                    retVal.Add(DBAccess.ConvertStateConsumptionModel(c));
                }
            }

            return retVal;
        }
        public IEnumerable<StateWeatherModel> GetStateWeathersByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            List<StateWeatherModel> retVal = new List<StateWeatherModel>();

            using (var db = new StatesDB())
            {
                var listW = db.StateWeathers.Where(x => x.State.stateName == stateName && x.localTime >= startDate && x.localTime <= endDate).ToList();

                foreach (var w in listW)
                {
                    retVal.Add(DBAccess.ConvertStateWeatherModel(w));
                }
            }

            return retVal;
        }
        #endregion

        #region ConvertingToDatabaseModel
        private static State ConvertStateDBNew(StateInfoModel model)
        {
            State state = new State();
            state.stateName = model.StateName;

            foreach (var c in model.StateConsumption)
            {
                state.StateConsumptions.Add(DBAccess.ConvertStateConsumptionDBNew(c));
            }

            foreach (var w in model.StateWeathers)
            {
                state.StateWeathers.Add(DBAccess.ConvertStateWeatherDBNew(w));
            }

            return state;
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
            StateInfoModel retVal = new StateInfoModel()
            {
                StateName = dbModel.stateName.Trim(),
                StateId = dbModel.stateID
            };

            retVal.StateConsumption.Clear();
            retVal.StateWeathers.Clear();

            foreach (var sc in dbModel.StateConsumptions)
            {
                retVal.StateConsumption.Add(DBAccess.ConvertStateConsumptionModel(sc));
            }

            foreach (var sw in dbModel.StateWeathers)
            {
                retVal.StateWeathers.Add(DBAccess.ConvertStateWeatherModel(sw));
            }

            return retVal;
        }
        private static StateWeatherModel ConvertStateWeatherModel(StateWeather dbModel)
        {
            return new StateWeatherModel()
            {
                AirTemperature = (float)dbModel.airTemperature,
                CloudCover = dbModel.cloudCover.Trim(),
                DevpointTemperature = (int)dbModel.devpointTemperature,
                GustValue = (int)dbModel.gustValue,
                HorizontalVisibility = (int)dbModel.horizontalVisibility,
                Humidity = (int)dbModel.humidity,
                PresentWeather = (string)dbModel.presentWeather.Trim(),
                RecentWeather = (string)dbModel.recentWeather.Trim(),
                ReducedPressure = (float)dbModel.reducedPressure,
                StationPressure = (float)dbModel.stationPressure,
                WindDirection = (string)dbModel.windDirection.Trim(),
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
                StateCode = (string)dbModel.stateCode.Trim(),
                Value = (double)dbModel.value,
                ValueScale = (double)dbModel.valueScale
            };
        }
        #endregion

        #region ShortNamesForStates

        public String GetShortStateName(String fullStateName)
        {
            if (String.IsNullOrEmpty(fullStateName))
                throw new Exception("State name can not be null or empty.");

            String retVal = String.Empty;

            using (var db = new StatesDB())
            {
                var state = db.shortStateNames.Where(x => x.fullName == fullStateName);

                if (state.Count() == 0)
                    throw new Exception("No short name for that state.");

                retVal = state.First().shortName;
            }

            return retVal.Trim();
        }

        public String GetFullStateName(String shortStateName)
        {
            if (String.IsNullOrEmpty(shortStateName))
                throw new Exception("State name can not be null or empty.");

            String retVal = String.Empty;

            using (var db = new StatesDB())
            {
                var state = db.shortStateNames.Where(x => x.shortName == shortStateName);

                if (state.Count() == 0)
                    throw new Exception("No full name for that state.");

                retVal = state.First().fullName;
            }

            return retVal.Trim();

        }

        #endregion
    }

}
