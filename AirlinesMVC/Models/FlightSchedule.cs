namespace AirlinesMVC.Models
{
    public class FlightSchedule
    {
        public string FlightNo { get; set; }
        public DateTime FlightDate { get; set; }
        public DateTime DepartTime { get; set; }
        public DateTime ArriveTime { get; set; }
    }
}
