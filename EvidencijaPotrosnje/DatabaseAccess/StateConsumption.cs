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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StateConsumption()
        {
            States = new HashSet<State>();
        }

        public int? covRation { get; set; }

        public DateTime? dateFrom { get; set; }

        public DateTime? dateShort { get; set; }

        public DateTime? dateTo { get; set; }

        public DateTime? dateUTC { get; set; }

        [StringLength(30)]
        public string stateCode { get; set; }

        public double? value { get; set; }

        public double? valueScale { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int stateConsumptionID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<State> States { get; set; }
    }
}
