using System;
using System.Collections.Generic;
using SharedModels;
using SharedModels.HelperClasses;
using System.Linq;
using NUnit.Framework;
using Moq;

namespace UnitTest
{
    [TestFixture]
    public class DataManipulationTest
    {
        private Dictionary<DataKeys, StateInfoModel> localDict;

        [SetUp]
        public void SetUp() 
        {
            CurrentData.Data.Clear();

            DataKeys dataKeys1 = new DataKeys("Srbija", new DateTime(2010, 4, 5), new DateTime(2015, 5, 6));
            DataKeys dataKeys2 = new DataKeys("Portugal", new DateTime(2010, 4, 5), new DateTime(2015, 5, 6));
            DataKeys dataKeys3 = new DataKeys("Spanija", new DateTime(2010, 4, 5), new DateTime(2015, 5, 6));
            DataKeys dataKeys4 = new DataKeys("Francuska", new DateTime(2010, 4, 5), new DateTime(2015, 5, 6));
            DataKeys dataKeys5 = new DataKeys("Srbija", new DateTime(2010, 4, 5), new DateTime(2015, 5, 6));

            StateInfoModel stateInfoModel1 = new StateInfoModel(new StateWeatherModel(), new StateConsumptionModel(), "Srbija");
            StateInfoModel stateInfoModel2 = new StateInfoModel(new StateWeatherModel(), new StateConsumptionModel(), "Portugal");
            StateInfoModel stateInfoModel3 = new StateInfoModel(new StateWeatherModel(), new StateConsumptionModel(), "Spanija");
            StateInfoModel stateInfoModel4 = new StateInfoModel(new StateWeatherModel(), new StateConsumptionModel(), "Francuska");
            StateInfoModel stateInfoModel5 = new StateInfoModel(new StateWeatherModel(), new StateConsumptionModel(), "Srbija");

            CurrentData.Data.Add(dataKeys1, stateInfoModel1);
            CurrentData.Data.Add(dataKeys2, stateInfoModel2);
            CurrentData.Data.Add(dataKeys3, stateInfoModel3);
            CurrentData.Data.Add(dataKeys4, stateInfoModel4);
            CurrentData.Data.Add(dataKeys5, stateInfoModel5);

            Mock<Dictionary<DataKeys, StateInfoModel>> mockCurrentData = new Mock<Dictionary<DataKeys, StateInfoModel>>(CurrentData.Data);
            localDict = mockCurrentData.Object;
        }

        [Test]
        [TestCase("Srbija")]
        public void TestFilterByName(String filterParam)
        {
            BussinesLogic.DataManipulation.FilterByName(filterParam);

            Assert.IsTrue(CurrentData.Data.Keys.ToList().All(x => x.Name == filterParam));

            this.SetUp();
        }
        
        [Test]
        [TestCase(null)]
        public void TestFilterByNameNull(String filterParam) 
        {
            try
            {
                BussinesLogic.DataManipulation.FilterByName(filterParam);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Filter string can not be null!"));
            }
            finally 
            {
                this.SetUp();
            }
        }

        [Test]
        [TestCase("")]
        public void TestFilterByNameEmpty(String filterParam) 
        {
            localDict = CurrentData.Data;

            BussinesLogic.DataManipulation.FilterByName(filterParam);
            Assert.IsTrue(CurrentData.Data == localDict);

            this.SetUp();
        }

        [Test]
        [TestCase("aSSAKM..';213sa")]
        public void TestFilterByNameRandom(String filterParam) 
        {
            BussinesLogic.DataManipulation.FilterByName(filterParam);

            Assert.IsTrue(CurrentData.Data.Count == 0);

            this.SetUp();
        }

        [Test]
        [TestCase("01/01/2015", "05/25/2021")]
        public void TestFilterByTime(String startDate, String endDate)
        {

            // everything after 1.1.2015. and before now is valid
            var start = DateTime.Parse(startDate);
            var end = DateTime.Parse(endDate);
            BussinesLogic.DataManipulation.FilterByTime(start, end);

            Assert.IsTrue(CurrentData.Data.Keys.ToList().All(x => x.StartDate >= start && x.EndDate <= end));

            this.SetUp();
        }

        [Test]
        [TestCase("01/01/0001","12/31/9999")]
        public void TestFilterByTimeAll(String start, String end) 
        {
            localDict = CurrentData.Data;

            // everything should be here again
            var startDate = DateTime.Parse(start);
            var endDate = DateTime.Parse(end);
            BussinesLogic.DataManipulation.FilterByTime(startDate, endDate);

            Assert.IsTrue(CurrentData.Data == localDict);

            this.SetUp();
        }
    
        [Test]
        [TestCase("12/31/9999", "01/01/0001")]
        public void TestFilterByTimeEmpty(String start, String end)
        {
            
            // everything should be empty
            var startDate = DateTime.Parse(start); 
            var endDate = DateTime.Parse(end);
            BussinesLogic.DataManipulation.FilterByTime(startDate, endDate);

            Assert.IsTrue(CurrentData.Data.Count == 0);

            this.SetUp();
        }

        [TearDown]
        public void TearDown()
        {
            localDict.Clear();
            CurrentData.Data.Clear();
        }

    }
}
