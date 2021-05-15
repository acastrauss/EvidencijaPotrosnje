namespace DatabaseAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StateWeather")]
    public partial class StateWeather
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StateWeather()
        {
            States = new HashSet<State>();
        }

        public double? airTemperature { get; set; }

        [StringLength(50)]
        public string cloudCover { get; set; }

        public int? devpointTemperature { get; set; }

        public int? gustValue { get; set; }

        public int? horizontalVisibility { get; set; }

        public int? humidity { get; set; }

        [StringLength(50)]
        public string presentWeather { get; set; }

        [StringLength(50)]
        public string recentWeather { get; set; }

        public double? reducedPressure { get; set; }

        public double? stationPressure { get; set; }

        [StringLength(50)]
        public string windDirection { get; set; }

        public int? windSpeed { get; set; }

        public DateTime? localTime { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int stateWeatherID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<State> States { get; set; }
    }
}
