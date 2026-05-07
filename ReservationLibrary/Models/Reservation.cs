using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ReservationLibrary.Models
{
    [Table("Reservation")]
    public class Reservation
    {
        [Key]
        [Column(TypeName = "CHAR(6)")]
        public string PNR { get; set; }
        [Column(TypeName = "CHAR(6)")]
        public string FlightNo { get; set; }
        public DateTime FlightDate { get; set; }
        public DateTime ReservationDate { get; set; }

        [ForeignKey("FlightNo, FlightDate")]
        public virtual FlightSchedule? FlightSchedule { get; set; }
        public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
    }

}
