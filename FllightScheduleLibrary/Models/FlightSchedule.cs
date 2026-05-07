using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace FlightScheduleLibrary.Models
{
    [Table("FlightSchedule")]
    [PrimaryKey("FlightNo", "FlightDate")]
    public class FlightSchedule
    {
        [ForeignKey("FlightNoNavigation")]
        [Column(TypeName = "CHAR(6)")]
        public string FlightNo { get; set; }
        public DateTime FlightDate { get; set; }
        public DateTime DepartTime { get; set; }
        public DateTime ArriveTime { get; set; }

        public virtual Flight? FlightNoNavigation { get; set; }
    }
}
