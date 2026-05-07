using System;
using System.Collections.Generic;
using System.Text;

namespace FlightScheduleLibrary.Models
{
    public class FlightScheduleException : Exception
    {
        public FlightScheduleException(string errMsg) : base(errMsg)
        {

        }
    }
}
