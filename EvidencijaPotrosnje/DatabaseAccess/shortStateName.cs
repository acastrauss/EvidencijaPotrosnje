namespace DatabaseAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("shortStateName")]
    public partial class shortStateName
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string fullName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string shortName { get; set; }
    }
}
