using System.ComponentModel.DataAnnotations.Schema;

namespace AirlinesMVC.Models
{
    public class Flight
    {
        public string FlightNo { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public int TotalSeats { get; set; }
    }
}
