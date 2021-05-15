using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.HelperClasses
{
    /// <summary>
    /// Keys for current data
    /// </summary>
    public class DataKeys
    {
        public string Name { get; set; }
        public DateTime DateInfo { get; set; }

        public override bool Equals(object obj)
        {
            DataKeys dk = (DataKeys)obj;
            return 
                dk.DateInfo == this.DateInfo &&
                dk.Name == this.Name
                ;
        }

        public DataKeys() { }
        public DataKeys(string name, DateTime date) 
        {
            this.Name = name;
            this.DateInfo = date;
        }

    }
}
