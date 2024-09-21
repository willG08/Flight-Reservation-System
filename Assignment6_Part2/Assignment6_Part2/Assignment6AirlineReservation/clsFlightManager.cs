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
    internal class clsFlightManager
    {
        /// <summary>
        /// create list of flights
        /// </summary>
        public List<clsFlight> lstFlights;



        /// <summary>
        /// return the list of flights
        /// </summary>
        /// <returns></returns>
        public List<clsFlight> GetFlights()
        {

            try 
            { 
                //data access
                clsDataAccess db = new clsDataAccess();

                //dataset
                DataSet ds = new DataSet();

                //return value
                int iRet = 0;

                //list of flights
                lstFlights = new List<clsFlight>();

                string SQL = clsSQL.GetFlights();

                //sql to select all flights
                //ds = db.ExecuteSQLStatement("Select * from Flights", ref iRet);
                ds = db.ExecuteSQLStatement(SQL, ref iRet);

                //instance of clsFlight
                clsFlight fl = new clsFlight();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsFlight Flight = new clsFlight();
                    Flight.sflightId = dr[0].ToString();
                    Flight.sflightNumber = dr[1].ToString();
                    Flight.saircraftType = dr[2].ToString();

                    //add the flight to the list
                    lstFlights.Add(Flight);
                }

                //return
                return lstFlights;

            }

            catch (Exception ex)
            {
                //throw exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
