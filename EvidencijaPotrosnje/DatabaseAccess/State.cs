namespace DatabaseAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("State")]
    public partial class State
    {
        [Required]
        [StringLength(10)]
        public string stateName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int stateID { get; set; }

        public int stateConsumptionID { get; set; }

        public int stateWeatherID { get; set; }

        public virtual StateConsumption StateConsumption { get; set; }

        public virtual StateWeather StateWeather { get; set; }
    }
}
