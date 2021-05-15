using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//DATA THAT WILL BE IN THE VIEW AND EXPORTED TO CSV FILE
namespace SharedModels.HelperClasses
{
    /// <summary>
    /// Currently used data:
    /// Keys are: name of the country, date when data is measured
    /// </summary>
    public static class CurrentData
    {

        public static Dictionary<DataKeys, StateInfoModel> data = new Dictionary<DataKeys, StateInfoModel>();

    }
}
