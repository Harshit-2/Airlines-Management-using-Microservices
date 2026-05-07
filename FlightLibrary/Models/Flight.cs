using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlightLibrary.Models
{
    [Table("Flight")]
    public class Flight
    {
        [Key]
        [Column(TypeName = "CHAR(6)")]
        public string FlightNo { get; set; }
        [Column(TypeName = "VARCHAR(20)")]
        public string FromCity { get; set; }
        [Column(TypeName = "VARCHAR(20)")]
        public string ToCity { get; set; }
        public int TotalSeats { get; set; }
    }
}
