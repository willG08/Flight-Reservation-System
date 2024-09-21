using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class clsPassenger
    {
        //stores the passengerId
        public int passengerId { get; set; }

        //stores the passesnger first name
        public string firstName { get; set; }

        //stores the passesnger last name
        public string lastName { get; set; }

        //stores the passengerId
        public int seatNumber { get; set; }

        /// <summary>
        /// formats the passenger for the combo box
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //firstName space lastName
            return firstName + " " + lastName;
        }
    }
}
