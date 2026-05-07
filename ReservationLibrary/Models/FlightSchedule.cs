using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ReservationLibrary.Models
{
    [Table("FlightSchedule")]
    [PrimaryKey("FlightNo", "FlightDate")]
    public class FlightSchedule
    {
        [ForeignKey("FlightNoNavigation")]
        [Column(TypeName = "CHAR(6)")]
        public string FlightNo { get; set; }
        public DateTime FlightDate { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
