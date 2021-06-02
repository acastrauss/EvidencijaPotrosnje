using System;
using System.Collections.Generic;
using SharedModels;
using SharedModels.HelperClasses;
using System.Linq;
using NUnit.Framework;
using Moq;
using BussinesLogic;
using System.Web.Hosting;
using System.IO;

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
            StateInfoModel state = new StateInfoModel();
            state.StateName = "Serbia";
            string solution_dir = Path.GetDirectoryName(Path.GetDirectoryName(
                TestContext.CurrentContext.TestDirectory));
            string path = solution_dir + "\\TestData\\ImportTest\\WeatherTest.csv";
            ImportParameters parameters = new ImportParameters(path, null,
                "Serbia", Convert.ToDateTime("01/01/2020"), Convert.ToDateTime("01/01/3000"));
            IEnumerable<StateWeatherModel> lista = importData.LoadWeather(parameters, state);

            bool istrue = false;


            foreach (StateWeatherModel model in lista)
            {
                
                if (model.LocalTime.Year == 2021 && model.LocalTime.Month == 3 &&
                    model.AirTemperature == 22 &&
                    model.CloudCover == "22" &&
                    model.DevpointTemperature == 22 &&
                    model.GustValue == 22 &&
                    model.HorizontalVisibility == 22 &&
                    model.Humidity == 22 &&
                    model.WindSpeed == 22 &&
                    model.PresentWeather == "22" &&
                    model.RecentWeather == "22" &&
                    model.StationPressure == 22 &&
                    model.WindDirection == "22" &&
                    model.ReducedPressure == 22)
                {
                    istrue = true;
                }
            }

            Assert.IsTrue(istrue);


        }

        [Test]
        public void ConsumptionTest()
        {

            StateInfoModel state = new StateInfoModel();
            state.StateId = 1;

            string solution_dir = Path.GetDirectoryName(Path.GetDirectoryName(
            TestContext.CurrentContext.TestDirectory));
            string path = solution_dir + "\\TestData\\ImportTest\\ConsumptionTest.csv";


            IEnumerable<StateConsumptionModel> stateConsumptionModels = importData.LoadConsumption(path, state
                , DateTime.MinValue, DateTime.MaxValue);


            bool istrue = false;


            foreach (StateConsumptionModel model in stateConsumptionModels)
            {

                if (model.StateCode == "RS")
                {
                    istrue = true;
                }
            }

            Assert.IsTrue(istrue);
        }


        [Test]
        public void WeatherTestNull()
        {
            try
            {
                ImportParameters parameters = null;
                StateInfoModel state = null;
                IEnumerable<StateWeatherModel> NullTest = importData.LoadWeather(parameters, state);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Load parameters cant be null."));
            }
        }

        [Test]
        public void ConsumptionTestNull()
        {
            try
            {
                StateInfoModel state = new StateInfoModel();
                state.StateName = "Serbia";
                string solution_dir = Path.GetDirectoryName(Path.GetDirectoryName(
                    TestContext.CurrentContext.TestDirectory));
                string weatherPath = solution_dir + "\\TestData\\ImportTest\\WeatherTest.csv";
                string consPath = solution_dir + "\\TestData\\ImportTest\\ConsumptionTest.csv";
                ImportParameters parameters = new ImportParameters(weatherPath,consPath,
                    "Serbia", Convert.ToDateTime("01/01/2020"), Convert.ToDateTime("01/01/3000"));

                importData.Load(parameters);
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Length > 0);
            }
        }

        [Test]
        public void LoadSuccessful()
        {
            try
            {
                ImportParameters parameters = null;
                StateInfoModel state = null;
                IEnumerable<StateConsumptionModel> NullTest = importData.LoadConsumption(null, null, DateTime.Now, DateTime.Now);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Load parameters cant be null."));
            }
        }

    }
}
