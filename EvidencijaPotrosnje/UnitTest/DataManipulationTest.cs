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
    [TestFixture]
    public class DataManipulationTest
    {
        private IDataManipulation filterData;
        private List<ShowingData> showingDataList;

        [SetUp]
        public void SetUp()
        {
            Mock<DataManipulation> mockFilter = new Mock<DataManipulation>();
            filterData = mockFilter.Object;

            // init data
            Mock<List<ShowingData>> mockList = new Mock<List<ShowingData>>();
            ShowingData data1 = new ShowingData("srbija", Convert.ToDateTime("05/05/2016"), 22.22, (float)22.3, (float)22.3, 11, 22);
            ShowingData data2 = new ShowingData("bugarska", Convert.ToDateTime("05/05/2017"), 22.22, (float)22.3, (float)22.3, 11, 22);
            ShowingData data3 = new ShowingData("srbija", Convert.ToDateTime("05/05/2018"), 22.22, (float)22.3, (float)22.3, 11, 22);
            ShowingData data4 = new ShowingData("hrvatska", Convert.ToDateTime("05/05/2019"), 22.22, (float)22.3, (float)22.3, 11, 22);
            ShowingData data5 = new ShowingData("srbija", Convert.ToDateTime("05/05/2019"), 22.22, (float)22.3, (float)22.3, 11, 22);

            mockList.Object.Add(data1);
            mockList.Object.Add(data2);
            mockList.Object.Add(data3);
            mockList.Object.Add(data4);
            mockList.Object.Add(data5);

            showingDataList = mockList.Object;
        }

        [Test]
        [TestCase("srbija")]
        public void TestFilterByName(String stateName)
        {
            showingDataList = filterData.FilterByName(stateName, showingDataList).ToList();

            Assert.IsTrue(showingDataList.ToList().All(x => x.StateName == stateName));
            this.SetUp();
        }

        [Test]
        [TestCase(null)]
        public void TestFilterByNameNull(String stateName)
        {
            try
            {
                filterData.FilterByName(stateName, showingDataList);
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
        public void TestFilterByNameEmpty(String stateName)
        {
            try
            {
                filterData.FilterByName(stateName, showingDataList);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("Filter string can not be empty!"));
            }
            finally
            {
                this.SetUp();
            }
        }

        [Test]
        [TestCase("asd")]
        public void TestFilterByNameRandom(String stateName)
        {
            showingDataList = filterData.FilterByName(stateName, showingDataList).ToList();
            Assert.IsTrue(showingDataList.Count == 0);
            this.SetUp();
        }

        [Test]
        [TestCase("05/05/2018", "01/06/2021")]
        public void TestFilterByTime(String startDate, String endDate)
        {
            DateTime Datefrom = DateTime.Parse(startDate);
            DateTime Dateto = DateTime.Parse(endDate);

            showingDataList = filterData.FilterByTime(Datefrom, Dateto, showingDataList).ToList();

            Assert.IsTrue(showingDataList.ToList().All(x => x.DateUTC >= Datefrom && x.DateUTC <= Dateto));
            this.SetUp();

        }

        [Test]
        [TestCase("01/01/0001", "12/31/9999")]
        public void TestFilterByTimeAll(String startDate, String endDate)
        {
            var temp = showingDataList;

            DateTime Datefrom = DateTime.Parse(startDate);
            DateTime Dateto = DateTime.Parse(endDate);
            showingDataList = filterData.FilterByTime(Datefrom, Dateto, showingDataList).ToList();


            bool equals = showingDataList.Count() == temp.Count() &&
                showingDataList.Count() != 0 &&
                temp.Count() != 0;

            if (equals)
            {
                for (int i = 0; i < temp.Count(); i++)
                {
                    equals &= showingDataList[i].Equals(temp[i]);
                }
            }

            Assert.IsTrue(equals);

            this.SetUp();
        }

        [Test]
        [TestCase("12/31/9999", "01/01/0001")]
        public void TestFilterByTimeEmpty(String startDate, String endDate)
        {
            DateTime start = DateTime.Parse(startDate);
            DateTime end = DateTime.Parse(endDate);

            var tempList = filterData.FilterByTime(start, end, showingDataList).ToList();

            Assert.IsTrue(tempList.Count() == 0);

            this.SetUp();
        }

        [TearDown]
        public void TearDown()
        {
            showingDataList.Clear();
        }
    }

}
