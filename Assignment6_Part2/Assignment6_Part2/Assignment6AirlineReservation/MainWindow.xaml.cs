using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using Label = System.Windows.Controls.Label;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //classData class reference
        clsDataAccess clsData;
        //class reference wndAddPass
        wndAddPassenger wndAddPass;
        //fclass reference flight manager
        clsFlightManager flightManager;
        //fclass reference passenger manager
        clsPassengerManager passengerManager;
        //class reference of list of flights
        List<clsFlight> lstFlights;
        //class reference of list of passengers
         List<clsPassenger> lstPassengers;
        //stores the changeseat boolean variable
        public bool changeSeat { get; set; } = false;
        //stores the add passenger boolean variable
        public bool addPassenger { get; set; } = false;
        //stores the brush color for red
        SolidColorBrush Red = (SolidColorBrush) new BrushConverter().ConvertFromString("#FFFD0000");
        //stores the brush color for blue
        SolidColorBrush Blue = (SolidColorBrush) new BrushConverter().ConvertFromString("#FF0000FF");
        //Brush Blue = (Brush) new BrushConverter().ConvertFromString("#FF0000FF");
        //stores the brush color for green
        SolidColorBrush Green = (SolidColorBrush) new BrushConverter().ConvertFromString("#FF00FD00");
        


        /// <summary>
        /// MainWindow intializes the window
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

                

                //create dataset
                DataSet ds = new DataSet();

                // Instantiate flightmanager object
                flightManager = new clsFlightManager();
                
                //instantiate passengerManager
                passengerManager = new clsPassengerManager();
               
                //ret
                int iRet = 0;

                //cls data class reference
                clsData = new clsDataAccess();

                // Get list of clsFlight objects
                //lstFlights = flightManager.GetFlights();

                //Bind list to UI combo box
                cbChooseFlight.ItemsSource = flightManager.GetFlights();

                //selectedFlight item
                clsFlight selectedFlight = (clsFlight)cbChooseFlight.SelectedItem;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// updates the buttons on the page and the passengers for the selected Flight
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChooseFlight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //resets the changeseat boolean variable
                changeSeat = false;
                //resets the add passenger boolean variable
                addPassenger = false;
                

                // Get list of clsFlight objects
                lstFlights = flightManager.GetFlights();

                //converts the selected item into a flight object
                //clsFlight selectedFlight = cbChooseFlight.SelectedItem as clsFlight;

                //selectedFlight item
                clsFlight selectedFlight = (clsFlight)cbChooseFlight.SelectedItem;

                //enables button to choose passenger
                cbChoosePassenger.IsEnabled = true;

                //enables passenger commands
                gPassengerCommands.IsEnabled = true;

                //disable change seat button
                cmdChangeSeat.IsEnabled = false;

                //disable delete passenger button
                cmdDeletePassenger.IsEnabled = false;

                //enable add passenger button
                cmdAddPassenger.IsEnabled = true;

                DataSet ds = new DataSet();                
                int iRet = 0;

                //Should be using a flight object to get the flight ID here
                if (selectedFlight.sflightId == "1")
                {
                    //show seats for flight
                    CanvasA380.Visibility = Visibility.Hidden;
                    Canvas767.Visibility = Visibility.Visible;
                }
                else
                {
                    //show seats for flight
                    Canvas767.Visibility = Visibility.Hidden;
                    CanvasA380.Visibility = Visibility.Visible;
                }

                //updates list of passengers
                lstPassengers = passengerManager.GetPassengers(selectedFlight.sflightId);

                //bind the passengers from the given flight to the combo box
                cbChoosePassenger.ItemsSource = lstPassengers;
                initializeTakenSeats();





            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// set taken seats for the passengers
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void initializeTakenSeats()
        {
            try
            {
                //initializes the fullseatNumber
                string fullSeatNumber = "";

                if (cbChooseFlight.SelectedItem != null)
                {
                    //stores the selected flight into a variable
                    clsFlight clsSelectedFlight = (clsFlight)cbChooseFlight.SelectedItem;

                    //updates list of passengers
                    lstPassengers = passengerManager.GetPassengers(clsSelectedFlight.sflightId);

                    //search through passengers
                    foreach (clsPassenger passenger in lstPassengers)
                    {
                        //we are looking at CanvasFlight1 
                        if (Canvas767.Visibility == Visibility.Visible)
                        {
                            //convert seatnumber to string and add full name to number
                            fullSeatNumber = "Seat" + passenger.seatNumber.ToString();

                            //cast string name into a label
                            Label label = this.FindName(fullSeatNumber) as Label;

                            // Check if the label was found
                            if (label != null)
                            {
                                // Set the background color if it is not already set
                                if (!label.Background.Equals(Red))
                                {
                                    //set background color
                                    label.Background = Red;
                                }
                            }


                        }
                        //we are looking at CanvasFlight2 
                        else
                        {
                            //convert seatnumber to string and add full name to number
                            fullSeatNumber = "SeatA" + passenger.seatNumber.ToString();

                            //cast string name into a label
                            Label label = this.FindName(fullSeatNumber) as Label;

                            // Check if the label was found
                            if (label != null)
                            {
                                // Set the background color if it is not already set
                                if (!label.Background.Equals(Red))
                                {
                                    label.Background = Red;
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                //throw exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// opens the message box to enter the full name of the new passenger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //shows window to add passenger name
                wndAddPass = new wndAddPassenger(this);
                //shows page
                wndAddPass.ShowDialog();
               

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

       

        /// <summary>
        /// resets the seat colors. 
        /// </summary>
        private void ResetSeats()
        {
            try
            {
                //we are looking at Canvas767
                if (Canvas767.Visibility == Visibility.Visible)
                {
                    //search through passengers in combo box
                    foreach (clsPassenger p in cbChoosePassenger.Items)
                    {
                        foreach (Label child in c767_Seats.Children)
                        {
                            //compare the seat content to the passenger content
                            if (child.Content.ToString() == p.seatNumber.ToString())
                            {
                                //set background color
                                child.Background = Red;

                                //update passenger seat number
                                //lblPassengersSeatNumber.Content = p.seatNumber;

                                //update the combo box selected Item
                                //cbChoosePassenger.SelectedItem = p;
                            }
                            

                        }

                    }
                }

                else
                {
                    //search through passengers in combo box
                    foreach (clsPassenger p in cbChoosePassenger.Items)
                    {
                        foreach (Label child in cA380_Seats.Children)
                        {
                            //compare the seat content to the passenger content
                            if (child.Content.ToString() == p.seatNumber.ToString())
                            {
                                //set background color
                                child.Background = Red;

                                //update passenger seat number
                                //lblPassengersSeatNumber.Content = p.seatNumber;

                                //update the combo box selected Item
                                //cbChoosePassenger.SelectedItem = p;
                            }
                           
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //throw exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// handles errors and shows a message box
        /// </summary>
        /// <param name="sClass"></param>
        /// <param name="sMethod"></param>
        /// <param name="sMessage"></param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the display if a new passenger is selected on the passenger combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChoosePassenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try 
            {

                //enable change seat button
                cmdChangeSeat.IsEnabled = true;
                //enable delete passenger button
                cmdDeletePassenger.IsEnabled = true;
                //enable add passenger button
                cmdAddPassenger.IsEnabled = true;
                //disable choose flight
                cbChooseFlight.IsEnabled = true;

                //enable choose passenger
                cbChoosePassenger.IsEnabled = true;

                if (sender != null)
                {
                    //string to seat number label name
                    string fullSeatNumber = "";
                    //selectedFlight item
                    clsFlight selectedFlight = (clsFlight)cbChooseFlight.SelectedItem;

                    //passenger instance
                    clsPassenger passenger = (clsPassenger)cbChoosePassenger.SelectedItem;

                    if (passenger != null)
                    {

                        if (Canvas767.Visibility == Visibility.Visible)
                        {
                            //convert seatnumber to string and add full name to number
                            fullSeatNumber = "Seat" + passenger.seatNumber.ToString();
                        }
                        else
                        {
                            //convert seatnumber to string and add full name to number
                            fullSeatNumber = "SeatA" + passenger.seatNumber.ToString();
                        }

                        //cast string name into a label
                        Label label = this.FindName(fullSeatNumber) as Label;

                        //reset all of the seats
                        initializeTakenSeats();

                        // Check if the label was found
                        if (label != null)
                        {
                            // Set the background color if it is not already set
                            if (!label.Background.Equals(Green))
                            {
                                //update color
                                label.Background = Green;

                                //update passenger seat number
                                lblPassengersSeatNumber.Content = passenger.seatNumber.ToString();

                                //update the combo box selected Item
                                cbChoosePassenger.SelectedItem = passenger;
                            }
                        }
                    }
                }
                
            }

            
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
    }
}

        /// <summary>
        /// This seat will take care of the seat click event and take care of the different button types
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Seat_Click(object sender, MouseButtonEventArgs e)
        {
            try { 
                // Cast the sender as a Label
                Label seat = sender as Label;

                //selectedFlight item
                clsFlight selectedFlight = (clsFlight)cbChooseFlight.SelectedItem;

                //initialize passenger
                clsPassenger Passenger;

                //updates list of passengers
                lstPassengers = passengerManager.GetPassengers(selectedFlight.sflightId);
            

                //enable choose passenger
                cbChoosePassenger.IsEnabled = true;

                //search through passengers in combo box
                foreach (clsPassenger p in cbChoosePassenger.Items)
                {
                    //compare the seat content to the passenger content
                    if (seat.Content.ToString() == p.seatNumber.ToString())
                    {
                        Passenger = p as clsPassenger;
                    }
                }

                //checks the changeseat boolean variable
                if (changeSeat == true)
                {
                    //check seat color
                        if (seat.Background.ToString() == Brushes.Blue.ToString())
                        {
                        //turn on empty label
                        lblEmpty.Visibility = Visibility.Hidden;

                        //selected Passenger
                        Passenger = (clsPassenger)cbChoosePassenger.SelectedItem;

                        if (Passenger != null)
                        {
                            //enable change seat button
                            cmdChangeSeat.IsEnabled = true;
                            //enable delete passenger button
                            cmdDeletePassenger.IsEnabled = true;
                            //enable add passenger button
                            cmdAddPassenger.IsEnabled = true;
                            //enable choose flight
                            cbChooseFlight.IsEnabled = true;
                            //enable choose passenger
                            cbChoosePassenger.IsEnabled = true;
                            //we are looking at Canvas767
                            if (Canvas767.Visibility == Visibility.Visible)
                            {
                                //search through seats
                                foreach (Label child in c767_Seats.Children)
                                {
                                    //compare the seat content to the old passenger seat
                                    if (child.Content.ToString() == Passenger.seatNumber.ToString())
                                    {
                                        //reset the background of the old seat
                                        child.Background = Blue;
                                    }
                                }
                            }
                            else
                            {
                                //search through seats
                                foreach (Label child in cA380_Seats.Children)
                                {
                                    //compare the seat content to the old passenger seat
                                    if (child.Content.ToString() == Passenger.seatNumber.ToString())
                                    {
                                        //reset the background of the old seat
                                        child.Background = Blue;
                                    }
                                }
                            }

                            //seat Number
                            int seatNum = 0;

                            //input last name
                            int.TryParse(seat.Content.ToString(), out seatNum);


                            //Add Passenger
                            lstPassengers = passengerManager.ChangeSeat(Passenger.passengerId.ToString(), selectedFlight.sflightId, seatNum.ToString());

                            //update source
                            cbChoosePassenger.ItemsSource = lstPassengers;

                            //reset seats
                            ResetSeats();

                            //go through combo box passengers to find the proper item to select
                            foreach (clsPassenger p in cbChoosePassenger.Items)
                            {
                                //compare the seat content to the passenger content
                                if (seat.Content.ToString() == p.seatNumber.ToString())
                                {
                                    //update combo box
                                    cbChoosePassenger.SelectedItem = p;
                                }

                            }
                                    

                            //update seat box
                            lblPassengersSeatNumber.Content = seat.Content.ToString();

                            //sets the background as green
                            seat.Background = Green;

                            //reset changeSeat
                            changeSeat = false;
                        }
                    }
                }

                //checks the add passenger boolean variable
                else if (addPassenger == true)
                {
                    //reset add passenger
                    addPassenger = false;

                    //check if seat is blue
                    //if (seat.Background == Brushes.Blue)
                    if (seat.Background.ToString() == Brushes.Blue.ToString())
                        {
                        Passenger = new clsPassenger();

                        //input first name
                        Passenger.firstName = wndAddPass.txtFirstName.Text;

                        //input last name
                        Passenger.lastName = wndAddPass.txtLastName.Text;

                        //stores seat number
                        int seatNum;

                        //input seat number
                        int.TryParse(seat.Content.ToString(), out seatNum);

                        //updates seatNumber
                        Passenger.seatNumber = seatNum;

                        //Add Passenger
                        lstPassengers = passengerManager.AddPassenger(Passenger, selectedFlight.sflightId);

                        //add the passenger to the list
                        //lstPassengers.Add(Passenger);

                        //reset addPassenger
                        addPassenger = false;

                        //change seat to green 
                        seat.Background = Green;

                        //update passenger seat number
                        lblPassengersSeatNumber.Content = Passenger.seatNumber;

                        cbChoosePassenger.ItemsSource = lstPassengers;//Need to set the combo box to the new list of passengers

                        //update the combo box selected Item
                        //cbChoosePassenger.SelectedItem = Passenger;

                        foreach (clsPassenger item in cbChoosePassenger.ItemsSource)
                        {
                            if(item.seatNumber == seatNum)
                            {
                                cbChoosePassenger.SelectedItem = item;
                            }
                        }
                        //update seat number in box
                        lblPassengersSeatNumber.Content = seat.Content;

                        //disable change seat button
                        cmdChangeSeat.IsEnabled = true;
                        //disable delete passenger button
                        cmdDeletePassenger.IsEnabled = true;
                        //disable add passenger button
                        cmdAddPassenger.IsEnabled = true;
                        //disable choose flight
                        cbChooseFlight.IsEnabled = true;
                        //disable choose passenger
                        cbChoosePassenger.IsEnabled = true;

                    }
                }
                
                

                //if none of the buttons are activated and the background is red. If it's blue we don't want to turn on the buttons and combo boxes
                else if(seat.Background == Red)
                {
                    //enable change seat button
                    cmdChangeSeat.IsEnabled = true;
                    //enable delete passenger button
                    cmdDeletePassenger.IsEnabled = true;
                    //enable add passenger button
                    cmdAddPassenger.IsEnabled = true;
                    //enable choose flight
                    cbChooseFlight.IsEnabled = true;

                        //search through passengers in combo box
                        foreach (clsPassenger p in cbChoosePassenger.Items)
                        {
                                //compare the seat content to the passenger content
                                if (seat.Content.ToString() == p.seatNumber.ToString())
                                {
                            //reset seats
                            //ResetSeats();
                            initializeTakenSeats();

                                    //set background color
                                    seat.Background = Green;

                                    //update passenger seat number
                                    lblPassengersSeatNumber.Content = p.seatNumber;

                                    //update the combo box selected Item
                                    cbChoosePassenger.SelectedItem = p;
                                }
                        
                            

                        }
                    

                    
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }


        }

        /// <summary>
        /// changes the seat of the selected passenger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChangeSeat_Click(object sender, RoutedEventArgs e)
        {
            try {
                //turn on empty label
                lblEmpty.Visibility = Visibility.Visible;

                //disable change seat button
                cmdChangeSeat.IsEnabled = false;
                //disable delete passenger button
                cmdDeletePassenger.IsEnabled = false;
                //disable add passenger button
                cmdAddPassenger.IsEnabled = false;
                //disable choose flight
                cbChooseFlight.IsEnabled = false;
                //disable choose passenger
                cbChoosePassenger.IsEnabled = false;

                //make true changeSeat to change seat_click method
                changeSeat = true;

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
}
        /// <summary>
        /// This method will delete a passenger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDeletePassenger_Click(object sender, RoutedEventArgs e)
        {
        try {
                //badSeat boolean allows us to keep track of if this seat is an invalid submissio
                //bool badSeat = false;
                //if (lstPassengers.Count > 0)
                //{
                    //disable these buttons because there will be 0 passengers after the method has ran
                    if(lstPassengers.Count == 1)
                    {
                        //disable change seat button
                        cmdChangeSeat.IsEnabled = false;
                        //disable delete passenger button
                        cmdDeletePassenger.IsEnabled = false;
                    }

                    //initialize seat
                    Label seat = null;

                    //selectedFlight item
                    clsFlight selectedFlight = (clsFlight)cbChooseFlight.SelectedItem;

                    //selected Passenger
                    clsPassenger Passenger = (clsPassenger)cbChoosePassenger.SelectedItem;

                    //we are looking at Canvas767
                    if (Canvas767.Visibility == Visibility.Visible)
                    {
                        //search through seats
                        foreach (Label child in c767_Seats.Children)
                        {
                            //compare the seat number to the passenger numbers
                            if (Passenger.seatNumber.ToString() == child.Content.ToString())
                            {

                                //store seat
                                seat = child;
                            }


                        }


                    }

                    else
                    {
                        //search through passengers in combo box
                        foreach (clsPassenger p in cbChoosePassenger.Items)
                        {
                            foreach (Label child in cA380_Seats.Children)
                            {

                                //set background color
                                child.Background = Blue;

                            }

                        }
                    }


                    // Show a message box with Yes and No buttons
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this passenger?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    // pushed yes
                    if (result == MessageBoxResult.Yes)
                    {
                        //update list of passengers
                        //lstPassengers = passengerManager.DeletePassenger(Passenger, selectedFlight.ToString());
                        lstPassengers = passengerManager.DeletePassenger(Passenger, selectedFlight.sflightId);///////////////////////////////////have to pass in the flight ID

                        //updated passenger list
                        cbChoosePassenger.ItemsSource = lstPassengers;

                        //update passenger seat number
                        lblPassengersSeatNumber.Content = "";

                        //choose nothing in combo box
                        cbChoosePassenger.SelectedIndex = -1;

                        //blue seats
                        //BlueSeats();

                        //reset Seats
                        initializeTakenSeats();

                        //disable change seat button
                        cmdChangeSeat.IsEnabled = false;
                        //disable delete passenger button
                        cmdDeletePassenger.IsEnabled = false;
                        //enable add passenger button
                        cmdAddPassenger.IsEnabled = true;
                        //enable choose flight
                        cbChooseFlight.IsEnabled = true;
                        //enable choose passenger
                        cbChoosePassenger.IsEnabled = true;

                        //reset the background color of the selected seat
                        seat.Background = Blue;
                    }
                //}
            
        }
        catch (Exception ex)
        {
            HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
            MethodInfo.GetCurrentMethod().Name, ex.Message);
        }
}

        /// <summary>
        /// turns all seats blue to reset after deleting a passenger
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void BlueSeats()
        {
            try
            {
                //we are looking at Canvas767
                if (Canvas767.Visibility == Visibility.Visible)
                {
                    //search through passengers in combo box
                    foreach (clsPassenger p in cbChoosePassenger.Items)
                    {
                        foreach (Label child in c767_Seats.Children)
                        {
                            
                            //set background color
                            child.Background = Blue;
                            

                        }

                    }
                }

                else
                {
                    //search through passengers in combo box
                    foreach (clsPassenger p in cbChoosePassenger.Items)
                    {
                        foreach (Label child in cA380_Seats.Children)
                        {
                            
                            //set background color
                            child.Background = Blue;
                            
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //throw exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
