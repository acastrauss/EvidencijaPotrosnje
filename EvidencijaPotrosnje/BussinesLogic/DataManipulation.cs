using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels;
using SharedModels.HelperClasses;

namespace BussinesLogic
{
    /// <summary>
    /// For manipulating data for user interface (filtering, etc.)
    /// </summary>
    public class DataManipulation
    {
        public static void FilterByName(string StateName)
        {
            if (String.IsNullOrEmpty(StateName))
            {
                if (StateName == null)
                    throw new Exception("Filter string can not be null!");
                else if (StateName.Equals(String.Empty))
                    return;
            }
            else 
            {
            }
        }

        public static void FilterByTime(DateTime DateFrom, DateTime DateTo)
        {
            Dictionary<DataKeys, StateInfoModel> temp = new Dictionary<DataKeys, StateInfoModel>();
            foreach (var pair in CurrentData.Data)
            {
                var state = pair.Value;

            } 
            CurrentData.Data = temp;
        }   
    }
}
