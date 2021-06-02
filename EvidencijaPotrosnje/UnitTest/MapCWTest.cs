using System;
using System.Collections.Generic;
using SharedModels;
using SharedModels.HelperClasses;
using System.Linq;
using NUnit.Framework;
using Moq;
using BussinesLogic;

namespace UnitTest
{
    public class MapCWTest
    {
        private IMapCW MapCW;
        //private List<ShowingData> showingDataList;

        [SetUp]

        public void SetUp()
        {
            Mock<MapCW> mockMapCW = new Mock<MapCW>();
            MapCW = mockMapCW.Object;
        }


        [Test]
        public void MapCWTestNull()
        {
            try
            {
                MapCW.MapData(null);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("State info model cant be null"));
            }
        }

        private static readonly object[] _dekoratror =
        {
            new object[] { 
                new StateInfoModel() {
                StateName = "Serbia",
                StateWeathers = new List<StateWeatherModel>()
                {
                    new StateWeatherModel(
                22, "22", 22, 22, 22, 22, "22", "22", 22, 22, "22", 22, new DateTime(2021, 2, 2), 56)
                },
                StateConsumption = new List<StateConsumptionModel>()
                {
                    new StateConsumptionModel(
                22, new DateTime(2021, 2, 2), new DateTime(2021, 2, 2), new DateTime(2021, 2, 2), new DateTime(2021, 2, 2),
                "RS", 22, 22, 56)
                }
            } }
        };

        [Test]
        [TestCaseSource("_dekoratror")]
        public void MapCWTestOk(StateInfoModel model)
        {

            IEnumerable<ShowingData> datas = MapCW.MapData(model);
            bool isTrue = false;
            datas.ToList();
            foreach(ShowingData data in datas)
            {
                if((data.StateName == "Serbia") && 
                    (data.ConsumptionValue == 22) && 
                    (DateTime.Compare(data.DateUTC, new DateTime(2021, 2, 2) ) == 0 ) &&
                    (data.Temperature == 22) &&
                    (data.WindSpeed ==22) &&
                    (data.Pressure == 22) &&
                    (data.Humidity == 22)

                    )
                {
                    isTrue = true;
                }
            }

            Assert.IsTrue(isTrue);

        }

        private static readonly object[] _dekoratrorNoWeather =
        {
            new object[] {
                new StateInfoModel() {
                StateName = "Serbia",
                StateWeathers = new List<StateWeatherModel>(),
                
                StateConsumption = new List<StateConsumptionModel>()
                {
                    new StateConsumptionModel(
                22, new DateTime(2021, 2, 2), new DateTime(2021, 2, 2), new DateTime(2021, 2, 2), new DateTime(2021, 2, 2),
                "RS", 22, 22, 56)
                }
            } }
        };

        [Test]
        [TestCaseSource("_dekoratrorNoWeather")]
        public void MapCWTestNoWeather(StateInfoModel model)
        {

            IEnumerable<ShowingData> datas = MapCW.MapData(model);
            bool isTrue = false;
            datas.ToList();
            foreach (ShowingData data in datas)
            {
                if ((data.StateName == "Serbia") &&
                    (data.ConsumptionValue == 22) &&
                    (DateTime.Compare(data.DateUTC, new DateTime(2021, 2, 2)) == 0))
                {
                    isTrue = true;
                }
            }

            Assert.IsTrue(isTrue);

        }


        private static readonly object[] _dekoratrorNoConsumption =
        {
            new object[] {
                new StateInfoModel() {
                StateName = "Serbia",
                StateWeathers = new List<StateWeatherModel>()
                {
                    new StateWeatherModel(
                22, "22", 22, 22, 22, 22, "22", "22", 22, 22, "22", 22, new DateTime(2021, 2, 2), 56)
                },
                StateConsumption = new List<StateConsumptionModel>()
                
            } }
        };

        [Test]
        [TestCaseSource("_dekoratrorNoConsumption")]
        public void MapCWTestNoConsumption(StateInfoModel model)
        {

            IEnumerable<ShowingData> datas = MapCW.MapData(model);
            bool isTrue = false;
            datas.ToList();
            foreach (ShowingData data in datas)
            {
                if ((data.StateName == "Serbia") &&
                    //(data.ConsumptionValue == 22) &&
                    (DateTime.Compare(data.DateUTC, new DateTime(2021, 2, 2)) == 0) &&
                    (data.Temperature == 22) &&
                    (data.WindSpeed == 22) &&
                    (data.Pressure == 22) &&
                    (data.Humidity == 22)
                    )
                {
                    isTrue = true;
                }
            }

            Assert.IsTrue(isTrue);

        }

    }
}
