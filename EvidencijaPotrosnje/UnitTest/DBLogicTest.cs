using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using Moq;
using NUnit.Framework;
using SharedModels;
using BussinesLogic;

namespace UnitTest
{
    [TestFixture]
    class DBLogicTest
    {
        // models that are initially in db for read test
        private List<StateInfoModel> initModels;

        [SetUp]
        public void SetUp() 
        {
            Mock<List<StateInfoModel>> mockStateInfos = new Mock<List<StateInfoModel>>();
            
            StateConsumptionModel scm = new StateConsumptionModel(
                33, new DateTime(2021, 5, 20, 11, 11, 58), new DateTime(2021, 5, 20, 11, 11, 58), new DateTime(2021, 5, 20, 11, 11, 58),
                new DateTime(2021, 5, 20, 11, 11, 58), "RS", 33, 33
                );
            StateWeatherModel swm = new StateWeatherModel(
                44, "test", 44, 44, 44, 44, "test", "test", 44, 44, "test", 44, new DateTime(2021, 5, 20, 11, 15, 58));

            StateInfoModel sim = new StateInfoModel(
                swm, scm, "Serbia"
                );

            mockStateInfos.Object.Add(
                sim
                );

            initModels = mockStateInfos.Object;
        }

        [Test]
        public void TestRead() 
        {
            var states = DBLogic.GetAllStates().ToList();

            if (states.Count != initModels.Count)
                Assert.Fail();

            for (int i = 0; i < states.Count; i++)
            {
                if (!states[i].Equals(initModels[i]))
                    Assert.Fail();
            }

            Assert.IsTrue(true);
        }

        [TearDown]
        public void TearDown()
        {
            initModels.Clear();
        }
    }
}
