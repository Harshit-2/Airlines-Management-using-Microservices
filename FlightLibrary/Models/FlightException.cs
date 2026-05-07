using System;
using System.Collections.Generic;
using System.Text;

namespace FlightLibrary.Models
{
    public class FlightException : Exception
    {
        public FlightException(string errMsg) : base(errMsg)
        {
            
        }
    }
}
