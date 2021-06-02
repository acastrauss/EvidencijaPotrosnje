using SharedModels.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic
{
    public interface IExportData
    {
        string SaveData(IEnumerable<ShowingData> showingDataI, string[] columns);
    }
}
