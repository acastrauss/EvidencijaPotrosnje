using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using SharedModels;
using BussinesLogic;
using Microsoft.VisualBasic.FileIO;

namespace UnitTest
{
    [TestFixture]
    class ExportTest
    {
        private ExportData exportData;
        private List<StateInfoModel> stateList;

        [SetUp]
        public void SetUp ()
        {
            
            Mock<ExportData> mockExport = new Mock<ExportData>();
            exportData = mockExport.Object;

            Mock<List<StateInfoModel>> mockStateList = new Mock<List<StateInfoModel>>();
            mockStateList.Object.Add(
                new StateInfoModel(
                    new StateWeatherModel(
                        55, "cloudCover", 44, 66, 77, 88, "presentWeather", "recentWeather", 33, 22, "wd", 11, DateTime.Now),
                    new StateConsumptionModel(33, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, "RS", 44, 55),
                    "Serbia"
                    )
                );
            
            stateList = mockStateList.Object;
        }

        private List<StateInfoModel> ReadTestFile(String fileName) 
        {
            List<StateInfoModel> list = new List<StateInfoModel>();

            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase); //put do priject foldera \\\Debug
            string a = fileName + ".csv";
            string[] paths = { @path, a };
            string fullPath = System.IO.Path.Combine(paths); // file:\\C:------------
            fullPath = fullPath.Substring(6, fullPath.Length - 6); //Skraceno

            using (TextFieldParser csvParser = new TextFieldParser(fullPath))
            {
                csvParser.SetDelimiters(new string[] { "," });

                while (!csvParser.EndOfData)
                {
                    var fields = csvParser.ReadFields();

                    String stateName = fields[0];
                    DateTime utc = DateTime.Parse(fields[1]);
                    double cons = double.Parse(fields[2]);
                    float airTemp = float.Parse(fields[3]);
                    float pressuer = float.Parse(fields[4]);
                    int hum = int.Parse(fields[5]);
                    int ws = int.Parse(fields[6]);

                    StateWeatherModel swm = new StateWeatherModel();
                    StateConsumptionModel scm = new StateConsumptionModel();
                    scm.DateUTC = utc;
                    scm.Value = cons;
                    swm.AirTemperature = airTemp;
                    swm.StationPressure = pressuer;
                    swm.Humidity = hum;
                    swm.WindSpeed = ws;

                    StateInfoModel sim = new StateInfoModel();
                    sim.StateName = stateName;
                    sim.StateConsumption = scm;
                    sim.StateWeather = swm;

                    list.Add(sim);
                }
            }

            return list;
        }

        [Test]
        [TestCase("user1")]
        public void TestExport1(String filename) 
        {
            exportData.SaveData(stateList, filename);

            var readData = this.ReadTestFile(filename);


            bool areSame = true;

            if(readData.Count != stateList.Count) 
            {
                areSame = false;
            }
            else
            {
                for (int i = 0; i < stateList.Count; i++)
                {
                    areSame &= stateList[i].StateConsumption.DateUTC.ToString() == readData[i].StateConsumption.DateUTC.ToString();
                    areSame &= stateList[i].StateConsumption.Value == readData[i].StateConsumption.Value;
                    areSame &= stateList[i].StateWeather.AirTemperature == readData[i].StateWeather.AirTemperature;
                    areSame &= stateList[i].StateWeather.StationPressure == readData[i].StateWeather.StationPressure;
                    areSame &= stateList[i].StateWeather.Humidity == readData[i].StateWeather.Humidity;
                    areSame &= stateList[i].StateWeather.WindSpeed == readData[i].StateWeather.WindSpeed;
                    areSame &= stateList[i].StateName == readData[i].StateName;
                }
            }

            Assert.IsTrue(areSame);

        }

        [TearDown]
        public void TearDown() 
        {

        }
    }
}
