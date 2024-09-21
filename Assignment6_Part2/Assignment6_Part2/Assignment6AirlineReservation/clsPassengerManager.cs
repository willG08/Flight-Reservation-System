using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assignment6AirlineReservation
{
    internal class clsPassengerManager
    {

        //create a list of passengers
        List<clsPassenger> lstPassengers;

        /// <summary>
        /// This will return the list of passengers for the given flight
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns></returns>
        public List<clsPassenger> GetPassengers(string flightId)
        {

            try
            {
                //data access
                clsDataAccess db = new clsDataAccess();

                //dataset
                DataSet ds = new DataSet();

                //return value
                int iRet = 0;

                //list of passengers
                lstPassengers = new List<clsPassenger>();

                string SQL = clsSQL.GetPassengers(flightId);

                //sql to select all passengers where flightid matches
                ds = db.ExecuteSQLStatement(SQL, ref iRet);

                //instance of clsPassengers
                clsPassenger ps = new clsPassenger();

                ///bind the variables for each passenger
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsPassenger Passenger = new clsPassenger();
                    Passenger.passengerId = Convert.ToInt32(dr[0]);
                    Passenger.firstName = dr[1].ToString();
                    Passenger.lastName = dr[2].ToString();
                    Passenger.seatNumber = Convert.ToInt32(dr[3]);

                    //add the passenger to the list
                    lstPassengers.Add(Passenger);
                }

               
                //return list of passengers
                return lstPassengers;
            }

            catch (Exception ex)
            {
                //throw exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

}

        /// <summary>
        /// Adds a passenger to the list of passengers
        /// </summary>
        /// <param name="Passenger"></param>
        /// <param name="sFlightID"></param>
        /// <returns></returns>
        public List<clsPassenger> AddPassenger(clsPassenger Passenger, string sFlightID)
        {
            try { 
                //data access
                clsDataAccess db = new clsDataAccess();

                //dataset
                DataSet ds = new DataSet();

                //class reference of list of passengers
                List<clsPassenger> lstPassengers;

                //return value
                int iRet = 0;

                //insert passenger
                string SQL = clsSQL.InsertPassenger(Passenger.firstName, Passenger.lastName);
                db.ExecuteNonQuery(SQL);//have to use ExecuteNonQuery to insert the record into the db, not query out data


                SQL = clsSQL.FindPassengerID(Passenger.firstName, Passenger.lastName);//Call your method to get the PassengerID
                //store id
                string sPassengerID = db.ExecuteScalarSQL(SQL);
                //variable to parse id
                int passID;
                //parse id
                int.TryParse(sPassengerID, out passID);
                //update passenger id
                Passenger.passengerId = passID;

                //SQL = clsSQL.InsertFlight_Passenger_Link(sFlightID.ToString(), Passenger.passengerId.ToString(), Passenger.seatNumber.ToString());
                SQL = clsSQL.InsertFlight_Passenger_Link(sFlightID.ToString(), sPassengerID, Passenger.seatNumber.ToString());//Use the passenger ID that was just extracted from the new passenger
                db.ExecuteNonQuery(SQL);//have to use ExecuteNonQuery to insert the record into the db, not query out data



                //getpassengers
                lstPassengers = GetPassengers(sFlightID);

                //return the new list of passengers
                return lstPassengers;
            }

            catch (Exception ex)
            {
                //throw exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
}

       
        /// <summary>
        /// This accepts the passenger id, flight id, and new seat number for a passenger. This will update the database's seat number for this passenger
        /// </summary>
        /// <param name="sPassengerID"></param>
        /// <param name="sFlightID"></param>
        /// <param name="sNewSeatNumber"></param>
        /// <returns></returns>
        public List<clsPassenger> ChangeSeat(string sPassengerID, string sFlightID, string sNewSeatNumber)//////////////////////////////////You aren't using the passenger's seat, you are updating it to the new seat number
        {
            try { 
                //data access
                clsDataAccess db = new clsDataAccess();

                //dataset
                DataSet ds = new DataSet();

                //class reference of list of passengers
                List<clsPassenger> lstPassengers;

                //return value
                int iRet = 0;

                //insert passenger
                string SQL = clsSQL.UpdateFlightPassengerLink(sFlightID, sPassengerID, sNewSeatNumber);

                db.ExecuteNonQuery(SQL);//Execute non query, not getting data

                //getpassengers
                lstPassengers = GetPassengers(sFlightID);

                //returns the updated list of passengers
                return lstPassengers;
            }

            catch (Exception ex)
            {
                //throw exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
    }
}

        /// <summary>
        /// this deletes the passenger from the database
        /// </summary>
        /// <param name="Passenger"></param>
        /// <param name="sFlightID"></param>
        /// <returns></returns>
        public List<clsPassenger> DeletePassenger(clsPassenger Passenger, string sFlightID)
        {
            try { 
                //data access
                clsDataAccess db = new clsDataAccess();

                //dataset
                DataSet ds = new DataSet();

                //class reference of list of passengers
                List<clsPassenger> lstPassengers;

                //delete Flight passenger Link
                string SQL = clsSQL.DeleteFlight_Passenger_Link(sFlightID, Passenger.passengerId.ToString());
                //delete data
                db.ExecuteNonQuery(SQL);

                //delete Passenger
                SQL = clsSQL.DeletePassenger(Passenger.passengerId.ToString());
                //delete data
                db.ExecuteNonQuery(SQL);

                //getpassengers
                lstPassengers = GetPassengers(sFlightID);


                //returns list of updated passengers
                return lstPassengers;
            }

            catch (Exception ex)
            {
                //throw exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
