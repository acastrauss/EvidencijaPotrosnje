using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using SharedModels;
using SharedModels.HelperClasses;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class DataManipulationTest
    {
        [TestMethod]
        public void TestFilterByName()
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

            String stateName = "Srbija";

            BussinesLogic.DataManipulation.FilterByName(stateName);

            Assert.IsTrue(CurrentData.Data.Keys.ToList().All(x => x.Name == stateName));
            
            CurrentData.Data.Clear();
        }
        
        [TestMethod]
        public void TestFilterByNameNull() 
        {
            try
            {
                BussinesLogic.DataManipulation.FilterByName(null);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Filter string can not be null!"));
            }
        }

        [TestMethod]
        public void TestFilterByNameEmpty() 
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

            var currDataTemp = CurrentData.Data;

            BussinesLogic.DataManipulation.FilterByName(String.Empty);

            Assert.IsTrue(CurrentData.Data == currDataTemp);

            CurrentData.Data.Clear();
        }

        [TestMethod]
        public void TestFilterByNameRandom() 
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

            String radnomString = "aSSAKM..';213sa";

            BussinesLogic.DataManipulation.FilterByName(radnomString);

            Assert.IsTrue(CurrentData.Data.Count == 0);

            CurrentData.Data.Clear();
        }

        [TestMethod]
        public void TestFilterByTime()
        {
            CurrentData.Data.Clear();

            DataKeys dataKeys1 = new DataKeys("Srbija", new DateTime(2010, 4, 5), new DateTime(2015, 5, 6));
            DataKeys dataKeys2 = new DataKeys("Portugal", new DateTime(2005, 4, 5), new DateTime(2012, 5, 6));
            DataKeys dataKeys3 = new DataKeys("Spanija", new DateTime(2019, 4, 5), new DateTime(2021, 5, 6));
            DataKeys dataKeys4 = new DataKeys("Francuska", new DateTime(2019, 4, 5), new DateTime(2020, 5, 6));
            DataKeys dataKeys5 = new DataKeys("Srbija", new DateTime(2010, 4, 5), new DateTime(2020, 5, 6));

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

            // everything after 1.1.2015. and before now is valid
            var startDate = new DateTime(2015, 1, 1);
            var endDate = DateTime.Now;
            BussinesLogic.DataManipulation.FilterByTime(startDate, endDate);

            Assert.IsTrue(CurrentData.Data.Keys.ToList().All(x => x.StartDate >= startDate && x.EndDate <= endDate));

            CurrentData.Data.Clear();
        }
    }
}
