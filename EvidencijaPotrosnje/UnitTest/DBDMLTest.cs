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
    /// Query Language is tested before this, so we can use it to check results for this test
    /// </summary>
    [TestFixture]
    public class DBDMLTest
    {
        private DBLogic dBLogic;

        [SetUp]
        public void SetUp()
        {
            Mock<DBLogic> mockDbLogic = new Mock<DBLogic>();
            dBLogic = mockDbLogic.Object;
        }

        /// <summary>
        /// Before this test, DB was filled with some entities
        /// </summary>
        [Test]
        public void _9DeleteAllStatesTotally()
        {
            dBLogic.RemoveAllStatesTotally();

            Assert.IsTrue(dBLogic.GetAllStates().Count() == 0);
        }

        private static readonly object[] _sourceStateInfoAddMore =
        {
            new object[] 
            {
                new StateInfoModel() 
                { 
                    StateName = "Serbia", 
                    StateConsumption = new List<StateConsumptionModel>(),
                    StateWeathers = new List<StateWeatherModel>()
                }
            }
        };

        [Test]
        [TestCaseSource("_sourceStateInfoAddMore")]
        public void _2AddStateTest(StateInfoModel expected)
        {
            dBLogic.AddState(expected);

            var state = dBLogic.GetAllStates().ToList();

            Assert.IsTrue(state[0].Equals(expected));
            
        }

        [Test]
        [TestCase(null)]
        public void _1AddStateNull(StateInfoModel toAdd)
        {
            try
            {
                dBLogic.AddState(toAdd);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("States to add can not be null."));
            }
        }

        private static readonly object[] _sourceStateConsumptionByDate =
        {
            new object[]
            {
                DateTime.MinValue, DateTime.MaxValue, "Serbia"
            }
        };

        /// <summary>
        /// Before this and next method all data is deleted
        /// After this state consumption should be empty for given state
        /// </summary>
        [Test]
        [TestCaseSource("_sourceStateConsumptionByDate")]
        public void _7RemoveStateConsumptionByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            dBLogic.RemoveStateConsumptionsByDate(startDate, endDate, stateName);

            Assert.IsTrue(dBLogic.GetStateConsumptionByName(stateName).ToList().Count == 0);
        }

        [Test]
        [TestCaseSource("_sourceStateConsumptionByDate")]
        public void _8RemoveStateWeatherByDate(DateTime startDate, DateTime endDate, String stateName)
        {
            dBLogic.RemoveStateWeathersByDate(startDate, endDate, stateName);

            Assert.IsTrue(dBLogic.GetStateWeatherByStateName(stateName).ToList().Count == 0);
        }

        /// <summary>
        /// Before next methods, RemoveStateConsumptionsByDate and RemoveStateWeathersByDate
        /// must be tested since they are used in this methods
        /// </summary>

        private static readonly object[] _sourceStateConsumptionsAdd =
        {
            new object[]
            {
                "Serbia",
                new List<StateConsumptionModel>()
                {
                    new StateConsumptionModel(
                    22, new DateTime(2021, 2, 2), new DateTime(2021, 2, 2), new DateTime(2021, 2, 2), new DateTime(2021, 2, 2),
                    "RS", 22, 22, 56)
                }
            }
        };

        [Test]
        [TestCaseSource("_sourceStateConsumptionsAdd")]
        public void _5TestAddStateConsumption(String stateName, List<StateConsumptionModel> expected)
        {
            
            dBLogic.AddStateConsumptions(expected, stateName);

            var sc = dBLogic.GetStateConsumptionByName(stateName).ToList();

            bool equals = expected.Count == sc.Count && sc.Count != 0;
        
            if(equals)
            {
                for (int i = 0; i < sc.Count; i++)
                {
                    equals &= sc[i].Equals(expected[i]);
                }
            }

            Assert.IsTrue(equals);
        }

        [Test]
        [TestCase(null, null)]
        public void _3TestAddStateConsumptionNull(String stateName, List<StateConsumptionModel> expected)
        {
            try
            {
                dBLogic.AddStateConsumptions(expected, stateName);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Invalid parameteres to add."));
            }
        }

        private static readonly object[] _sourceStateWeathersAdd =
        {
            new object[]
            {
                "Serbia",
                new List<StateWeatherModel> ()
                {
                    new StateWeatherModel(
                    22, "22", 22, 22, 22, 22, "22", "22", 22, 22, "22", 22, new DateTime(2021, 2, 2), 56)
                }
            }
        };


        [Test]
        [TestCaseSource("_sourceStateWeathersAdd")]
        public void _6TestAddStateWeather(String stateName, List<StateWeatherModel> expected)
        {
            dBLogic.AddStateWeather(expected, stateName);

            var sc = dBLogic.GetStateWeatherByStateName(stateName).ToList();

            bool equals = expected.Count == sc.Count && sc.Count != 0;

            if (equals)
            {
                for (int i = 0; i < sc.Count; i++)
                {
                    equals &= sc[i].Equals(expected[i]);
                }
            }

            Assert.IsTrue(equals);
        }

        [Test]
        [TestCase(null, null)]
        public void _4TestAddStateWeatherNull(String stateName, List<StateWeatherModel> expected)
        {
            try
            {
                dBLogic.AddStateWeather(expected, stateName);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Invalid parameteres to add."));
            }
        }

        [TearDown]
        public void TearDown()
        {

        }

    }
}
