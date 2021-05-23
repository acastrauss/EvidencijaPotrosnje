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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override bool Equals(object obj)
        {
            DataKeys dk = (DataKeys)obj;
            return 
                dk.EndDate == this.EndDate && dk.StartDate == this.StartDate &&
                dk.Name == this.Name
                ;
        }

        public DataKeys() {
            this.Name = "";
            this.StartDate = DateTime.MinValue;
            this.EndDate = DateTime.MaxValue;
        }
        public DataKeys(string name, DateTime start,DateTime end) 
        {
            this.Name = name;
            this.StartDate = start;
            this.EndDate = end;
        }

    }
}
