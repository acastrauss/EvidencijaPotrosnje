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
    public class ExportTest
    {
        private IExportData exportData;
        private List<ShowingData> showingDataList;

        [SetUp]

        public void SetUp()
        {
            Mock<ExportData> mockExport = new Mock<ExportData>();
            exportData = mockExport.Object;

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
        public void TestExportEmpty()
        {
            try
            {
                List<ShowingData> lista = new List<ShowingData>();
                exportData.SaveData(lista);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("List for export cant be emtpy"));
            }

        }

        [Test]
        public void TestExportNull()
        {
            try
            {
                exportData.SaveData(null);
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Equals("List for export cant be null"));
            }

        }






    }
}
