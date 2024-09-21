using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class clsSQL
    {
        /// <summary>
        /// this returns the sql code to get the list of flights
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetFlights()
        {
            try
            {
                string sSQL = "SELECT Flight_ID, Flight_Number, Aircraft_Type FROM FLIGHT";
                return sSQL;

            }
            catch (Exception ex)
            {
                //throw exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// this returns the sql code to get the list of passengers
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetPassengers(string sFlightID)
        {
            try
            {
                string sSQL = "SELECT Passenger.Passenger_ID, First_Name, Last_Name, FPL.Seat_Number " +
                              "FROM Passenger, Flight_Passenger_Link FPL " +
                              "WHERE Passenger.Passenger_ID = FPL.Passenger_ID AND " +
                              "Flight_ID = " + sFlightID;

                return sSQL;
            }
            catch (Exception ex)
            {
                //throw exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// this returns the sql code to update the flight passenger link
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        /// <param name="sSeatNumber"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string UpdateFlightPassengerLink(string sFlightID, string sPassengerID, string sSeatNumber)
        {
            try
            {
                //Updating seat numbers
                string sSQL = "UPDATE FLIGHT_PASSENGER_LINK " +
                           "SET Seat_Number = " + sSeatNumber +
                           " WHERE FLIGHT_ID = " + sFlightID + " AND PASSENGER_ID = " + sPassengerID;

                return sSQL;
            }
            catch (Exception ex)
            {
                //throw exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// this returns the sql code to insert a new passenger
        /// </summary>
        /// <param name="First_Name"></param>
        /// <param name="Last_Name"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string InsertPassenger(string First_Name, string Last_Name)
        {
            try
            {
                //Inserting a passenger
                string sSQL = "INSERT INTO PASSENGER( First_Name  , Last_Name) VALUES('"+ First_Name  + "','"+ Last_Name  + "')";

                return sSQL;
            }
            catch (Exception ex)
            {
                //throw exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// this returns the sql code to insert the flight passenger link for the new passenger
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        /// <param name="sSeatNumber"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string InsertFlight_Passenger_Link(string sFlightID, string sPassengerID, string sSeatNumber)
        {
            try
            {
                //Insert into the link table
                string sSQL = "INSERT INTO Flight_Passenger_Link(Flight_ID, Passenger_ID, Seat_Number) " +
               "VALUES( "+ sFlightID + " , "+ sPassengerID + " , "+ sSeatNumber + ")";

                return sSQL;
            }
            catch (Exception ex)
            {
                //throw exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// this returns the sql code to delete the flight passenger link
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassengerID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string DeleteFlight_Passenger_Link(string sFlightID, string sPassengerID)
        {
            try
            {
                //Deleting the link
                string sSQL = "Delete FROM FLIGHT_PASSENGER_LINK " +
                   "WHERE FLIGHT_ID = " + sFlightID +" AND " +
                   "PASSENGER_ID = " + sPassengerID;

                return sSQL;
            }
            catch (Exception ex)
            {
                //throw exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// this returns the sql code to delete a passenger
        /// </summary>
        /// <param name="sPassengerID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string DeletePassenger(string sPassengerID)
        {
            try
            {
                //Delete the passenger
                string sSQL = "Delete FROM PASSENGER " + "WHERE PASSENGER_ID = " + sPassengerID;

                return sSQL;
            }
            catch (Exception ex)
            {
                //throw exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// this returns the sql code to find the passengersID
        /// </summary>
        /// <param name="First_Name"></param>
        /// <param name="Last_Name"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string FindPassengerID(string First_Name, string Last_Name)
        {
            try
            {
                //Get the passenger's ID
                string sSQL = "SELECT Passenger_ID from Passenger where First_Name = '"+ First_Name + "' AND Last_Name = '" + Last_Name + "'";

                return sSQL;
            }
            catch (Exception ex)
            {
                //throw exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


       

    }
}
