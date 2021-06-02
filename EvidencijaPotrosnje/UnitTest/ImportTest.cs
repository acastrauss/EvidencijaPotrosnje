using System;
using System.Collections.Generic;
using SharedModels;
using SharedModels.HelperClasses;
using System.Linq;
using NUnit.Framework;
using Moq;
using BussinesLogic;
using System.Web.Hosting;

namespace UnitTest
{
    [TestFixture]

    public class ImportTest
    {

        private IImportData importData;

        [SetUp]
        public void SetUp()
        {
            Mock<ImportData> mockImportData = new Mock<ImportData>();
            importData = mockImportData.Object;
        }

        [Test]
        public void WeatherTest()
        {
            //StateInfoModel state = new StateInfoModel();
            //state.StateId = 1;
            //List<StateWeatherModel> lista = importData.LoadWeather(HostingEnvironment.MapPath($"~/App_Data/TestData/ImportTest/WeatherTest.csv"), state,
            //    Convert.ToDateTime("01/01/0001"), Convert.ToDateTime("01/01/3000"));

            //bool istrue = false;


            //foreach (StateWeatherModel model in lista)
            //{
            //    if ((DateTime.Compare(model.LocalTime, Convert.ToDateTime("16/03/2021")) == 0) &&
            //        model.AirTemperature == 22 &&
            //        model.CloudCover == "22" &&
            //        model.DevpointTemperature == 22 &&
            //        model.GustValue == 22 &&
            //        model.HorizontalVisibility == 22 &&
            //        model.Humidity == 22 &&
            //        model.WindSpeed == 22 &&
            //        model.PresentWeather == "22" &&
            //        model.RecentWeather == "22" &&
            //        model.StationPressure == 22 &&
            //        model.WindDirection == "22" &&
            //        model.StateId == 1 &&
            //        model.ReducedPressure == 22)
            //    {
            //        istrue = true;
            //    }
            //}

            //Assert.IsTrue(istrue);


        }

    }
}
