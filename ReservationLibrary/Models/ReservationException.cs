using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationLibrary.Models
{
    public class ReservationException : Exception
    {
        public ReservationException(string errMsg) : base(errMsg)
        {
            
        }
    }
}
