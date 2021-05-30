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

        public int stateWeatherID { get; set; }

        public DateTime? localTime { get; set; }

        public int? stateID { get; set; }

        public virtual State State { get; set; }
    }
}
