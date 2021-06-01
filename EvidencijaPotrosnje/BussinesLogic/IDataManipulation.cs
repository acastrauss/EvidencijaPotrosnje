using SharedModels.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic
{
    public interface IDataManipulation
    {
        IEnumerable<ShowingData> FilterByTime(DateTime DateFrom, DateTime DateTo, IEnumerable<ShowingData> lista);
        IEnumerable<ShowingData> FilterByName(string StateName, IEnumerable<ShowingData> lista);

    }
}
