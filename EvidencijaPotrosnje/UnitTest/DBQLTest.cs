using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using BussinesLogic;
using SharedModels;

namespace UnitTest
{
    /// <summary>
    /// Class for testing Query Language for DB
    /// </summary>
    [TestFixture]
    public class DBQLTest
    {
        private DBLogic dBLogic;
        private List<StateInfoModel> stateInfoModels;

        [SetUp]
        public void SetUp()
        {
            Mock<DBLogic> mockDbLogic = new Mock<DBLogic>();
            dBLogic = mockDbLogic.Object;

            Mock<List<StateInfoModel>> mockList = new Mock<List<StateInfoModel>>();

            stateInfoModels = mockList.Object;
        }


        /// <summary>
        /// Added manually in DB
        /// </summary>
        [Test]
        [TestCase("Serbia")]
        public void TestIfStateExist(String stateName)
        {
            Assert.IsTrue(dBLogic.IfStateExists(stateName));
        }

        [Test]
        [TestCase("Germany")]
        public void TestIfStateNotExist(String stateName)
        {
            Assert.IsFalse(dBLogic.IfStateExists(stateName));
        }

        [Test]
        [TestCase(null)]
        public void TestIfStateExistNull(String stateName)
        {
            try
            {
                dBLogic.IfStateExists(stateName);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("State name can not be null or empty."));
            }
        }

        [Test]
        [TestCase("RS", "Serbia")]
        public void GetFullStateNameTest(String shortName, String expectedFullName)
        {
            Assert.IsTrue(
                dBLogic.GetFullStateName(shortName).Equals(expectedFullName));
        }

        [Test]
        [TestCase(null)]
        public void GetFullStateNameNull(String stateName)
        {
            try
            {
                dBLogic.GetFullStateName(stateName);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("State name can not be null or empty."));
            }
        }

        [Test]
        [TestCase("NoCountry")]
        public void GetFullStateNameNotExist(String shortName)
        {
            try
            {
                dBLogic.GetFullStateName(shortName);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("No full name for that state."));
            }
        }

        [Test]
        [TestCase("Serbia", "RS")]
        public void GetShortStateNameTest(String fullStateName, String expectedShortName)
        {
            Assert.IsTrue(dBLogic.GetShortStateName(fullStateName).Equals(expectedShortName));
        }

        [Test]
        [TestCase("NoCountry")]
        public void GetShortStateNameNotExist(String fullStateName)
        {
            try
            {
                dBLogic.GetShortStateName(fullStateName);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("No short name for that state."));
            }
        }

        [Test]
        [TestCase(null)]
        public void GetShortStateNameNull(String fullStateName)
        {
            try
            {
                dBLogic.GetShortStateName(fullStateName);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("State name can not be null or empty."));
            }
        }

        private static readonly object[] _sourceGetAllStates =
        {
            new object[] {new List<StateInfoModel> { new StateInfoModel("Serbia") , new StateInfoModel("Germany") , new StateInfoModel("Russia") } }
        };

        [Test]
        [TestCaseSource("_sourceGetAllStates")]
        public void GetAllStates(List<StateInfoModel> expectedStates)
        {
            var states = dBLogic.GetAllStates().ToList();

            bool equals = expectedStates.Count() == states.Count() &&
                expectedStates.Count() != 0 && states.Count() != 0;

            for (int i = 0; i < states.Count(); i++)
            {
                equals &= states[i].Equals(expectedStates[i]);
            }

            Assert.IsTrue(equals);
        }

        private static readonly object[] _sourceStateByName =
        {
            new object[] { "Serbia", new StateInfoModel("Serbia") }
        };

        [Test]
        [TestCaseSource("_sourceStateByName")]
        public void GetStateByName(String stateName, StateInfoModel expectedState)
        {
            var state = dBLogic.GetStateByName(stateName);

            Assert.IsTrue(state.Equals(expectedState));
        }

        [Test]
        [TestCase(null)]
        public void GetStateByNameNull(String stateName)
        {
            try
            {
                dBLogic.GetStateByName(stateName);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("State name can not be null."));
            }
        }

        private static readonly object[] _sourceStateConsumption =
        {
            new object[] { "Serbia", new StateConsumptionModel(
                22, new DateTime(2021, 2, 2), new DateTime(2021, 2, 2), new DateTime(2021, 2, 2), new DateTime(2021, 2, 2),
                "RS", 22, 22, 56
                ) }
        };

        [Test]
        [TestCaseSource("_sourceStateConsumption")]
        public void GetStateConsumptionByName(String stateName, StateConsumptionModel expectedModel)
        {
            var state = dBLogic.GetStateConsumptionByName(stateName).ToList()[0];

            Assert.IsTrue(state.Equals(expectedModel));
        }

        [Test]
        [TestCase(null)]
        public void GetStateConsumptionByNameNull(String name)
        {
            try
            {
                dBLogic.GetStateConsumptionByName(name);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("State name can not be null or empty."));
            }
        }

        private static readonly object[] _sourceStateWeather =
        {
            new object[] { "Serbia", new StateWeatherModel(
                22, "22", 22, 22, 22, 22, "22", "22", 22, 22, "22", 22, new DateTime(2021, 2, 2), 56)
            }
        };


        [Test]
        [TestCaseSource("_sourceStateWeather")]
        public void GetStateWeatherByName(String stateName, StateWeatherModel expectedModel)
        {
            var state = dBLogic.GetStateWeatherByStateName(stateName).ToList()[0];

            Assert.IsTrue(state.Equals(expectedModel));
        }

        [Test]
        [TestCase(null)]
        public void GetStateWeatherNameNull(String stateName)
        {
            try
            {
                dBLogic.GetStateWeatherByStateName(stateName);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("State name can not be null or empty."));
            }
        }

        private static readonly object[] _sourceStateInfoDate =
        {
            new object[] { DateTime.MinValue, DateTime.MaxValue, "Serbia"
                , new StateInfoModel() {
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
        [TestCaseSource("_sourceStateInfoDate")]
        public void GetStateByDate(DateTime startDate, DateTime endDate, String stateName, StateInfoModel expectedModel)
        {
            var state = dBLogic.GetStateByDate(startDate, endDate, stateName);

            bool equals = state.StateName.Equals(expectedModel.StateName) &&
               state.StateConsumption.Count() == expectedModel.StateConsumption.Count() &&
               state.StateConsumption.Count() != 0 && expectedModel.StateConsumption.Count() != 0 &&
               state.StateWeathers.Count() == expectedModel.StateWeathers.Count() &&
               state.StateWeathers.Count() != 0 && expectedModel.StateWeathers.Count() != 0
                ;

            if (equals)
            {
                for (int i = 0; i < state.StateConsumption.Count(); i++)
                {
                    equals &= state.StateConsumption[i].Equals(expectedModel.StateConsumption[i]);
                }

                for (int i = 0; i < state.StateWeathers.Count(); i++)
                {
                    equals &= state.StateWeathers[i].Equals(expectedModel.StateWeathers[i]);
                }
            }

            Assert.IsTrue(equals);
        }

        private static readonly object[] _sourceStateInfoDateNull =
        {
            new object[] { DateTime.MinValue, DateTime.MaxValue, null}
        };


        [Test]
        [TestCaseSource("_sourceStateInfoDateNull")]
        public void TestStateByDateNull(DateTime startDate, DateTime endDate, String stateName)
        {
            try
            {
                dBLogic.GetStateByDate(startDate, endDate, stateName);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("State name can not be null or empty."));
            }
        }

        private static readonly object[] _sourceStateConsumptionDate =
        {
            new object[] { DateTime.MinValue, DateTime.MaxValue, "Serbia"
                , new List<StateConsumptionModel>()
                {   new StateConsumptionModel(
                22, new DateTime(2021, 2, 2), new DateTime(2021, 2, 2), new DateTime(2021, 2, 2), new DateTime(2021, 2, 2),
                "RS", 22, 22, 56)
                }
            }
        };

        [Test]
        [TestCaseSource("_sourceStateConsumptionDate")]
        public void TestConsumptionDate(DateTime startDate, DateTime endDate, List<StateConsumptionModel> expected)
        {

        }

        [TearDown]
        public void TearDown()
        {
            stateInfoModels.Clear();
        }

    }
}
