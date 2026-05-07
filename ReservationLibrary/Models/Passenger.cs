using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ReservationLibrary.Models
{
    [Table("Passenger")]
    [PrimaryKey("PNR", "PassengerNo")]
    public class Passenger
    {
        [Column(TypeName = "CHAR(6)")]
        [ForeignKey("ReservationPNRNavigation")]
        public string PNR { get; set; }
        public int PassengerNo { get; set; }
        [Column(TypeName = "VARCHAR(30)")]
        public string PassengerName { get; set; }
        [Column(TypeName = "CHAR(1)")]
        public string Gender { get; set; }
        public byte Age { get; set; }

        public virtual Reservation? ReservationPNRNavigation { get; set; }
    }

}
