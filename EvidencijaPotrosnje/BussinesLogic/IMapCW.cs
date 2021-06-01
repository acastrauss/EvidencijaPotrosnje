using SharedModels;
using SharedModels.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic
{
    public interface IMapCW
    {
        IEnumerable<ShowingData> MapData(StateInfoModel stateInfoModel);
    }
}
