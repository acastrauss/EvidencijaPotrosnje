namespace DatabaseAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StateConsumption")]
    public partial class StateConsumption
    {
        public int? covRation { get; set; }

        public DateTime? dateFrom { get; set; }

        public DateTime? dateShort { get; set; }

        public DateTime? dateTo { get; set; }

        public DateTime? dateUTC { get; set; }

        [StringLength(30)]
        public string stateCode { get; set; }

        public double? value { get; set; }

        public double? valueScale { get; set; }

        public int stateConsumptionID { get; set; }

        public int? stateID { get; set; }

        public virtual State State { get; set; }
    }
}
