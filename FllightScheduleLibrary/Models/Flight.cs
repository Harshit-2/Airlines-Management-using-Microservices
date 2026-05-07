using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlightScheduleLibrary.Models
{
    [Table("Flight")]
    public class Flight
    {
        [Key]
        [Column(TypeName = "CHAR(6)")]
        public string FlightNo { get; set; }

        public virtual ICollection<FlightSchedule> FlightSchedules { get; set; } = new List<FlightSchedule>();
    }

}
