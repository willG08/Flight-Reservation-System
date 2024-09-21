using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class clsFlight
    {
        //stores the flightId
        //public static int flightId = 0;
        public string sflightId { get; set; }

        //stores the flightNumber
        //public static string flightNumber = "";
        public string sflightNumber { get; set; }

        //stores the aircraft type
        //public static string aircraftType = "";
        public string saircraftType { get; set; }


        /// <summary>
        /// formats the flight number and aircraft type for the combo box by overriding the tostring method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //formats the flight number and aircraft type
            return sflightNumber + " - " + saircraftType;
        }
    }
}
