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
        public static List<ShowingData> FilterByName(string StateName, List<ShowingData> lista)
        {
            List<ShowingData> povratna = new List<ShowingData>();

            if (String.IsNullOrEmpty(StateName))
            {
                if (StateName == null)
                    throw new Exception("Filter string can not be null!");
                else if (StateName.Equals(String.Empty))
                    throw new Exception("Filter string can not be empty!");
            }
            else
            {
                foreach (ShowingData model in lista)
                {
                    if (model.StateName == StateName)
                    {
                        povratna.Add(model);
                    }
                }
            }

            return povratna;
        }

        public static List<ShowingData> FilterByTime(DateTime DateFrom, DateTime DateTo, List<ShowingData> lista)
        {
            if (DateFrom == null)
                throw new Exception("Filter date can not be null!");
            if (DateTo == null)
                throw new Exception("Filter date can not be null!");

            List<ShowingData> povratna = new List<ShowingData>();

            foreach (ShowingData model in lista)
            {
                if ((DateTime.Compare(DateFrom, model.DateUTC) <= 0) && (DateTime.Compare(DateTo, model.DateUTC) >= 0))
                {
                    povratna.Add(model);
                }
            }
            return povratna;
        }
    }
}
