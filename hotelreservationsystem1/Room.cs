using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hotelreservationsystem1
{
    public abstract class Room
    {
        private int roomID;
        private string roomType;
        private double pricePerNight;
        private List<string> amenities;

        public List<Feedback> Feedbacks = new List<Feedback>();

        // Constructor
        public Room(int roomID, string roomType, double pricePerNight, List<string> amenities)
        {
            this.roomID = roomID;
            this.roomType = roomType;
            this.pricePerNight = pricePerNight;
            this.amenities = amenities;
        }

        // Getters
        public int GetRoomID() 
        {
            return roomID;
        }

        public string GetRoomType() 
        { 
            return roomType;
        }

        public double GetPricePerNight() 
        {
            return pricePerNight;
        }

        public List<string> GetAmenities() 
        { 
            return amenities;
        }

        public List<Feedback> GetFeedback() 
        {
            return Feedbacks;
        }

        // feedback
        public void SetFeedbacks(List<Feedback> feedbacks)
        {
            this.Feedbacks = feedbacks ?? new List<Feedback>();
        }

        public void AddFeedback(Feedback feedback)
        {
            Feedbacks.Add(feedback);
        }

        // clearing feedbacks
        public void RemoveRoom()
        {
            Feedbacks.Clear();
        }


        // virtual method for room-specific details (overridden in child classes)
        public virtual Dictionary<string, string> GetRoomDetails()
        {
            return new Dictionary<string, string>
            {
                { "Amenities", string.Join(", ", amenities) }
            };
        }

        // price validator
        public virtual bool IsValidPrice(double price)
        {
            if (price <= 0.00)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        // create room by validating the user inputs
        public static void createRoom(string selectedCreateRoomType, int defaultCreateRoomID, double inputCreatePrice, List<string> selectedCreateAmenities, string selectedCreateBedType, int selectedCreateBedrooms, List<string> selectedCreateServices, string selectedCreateViewType) 
        {
            try
            {
                // create a room object based on room type
                if (selectedCreateRoomType == "Standard")
                {
                    // create a standard room 
                    StandardRoom standardRoom = new StandardRoom(defaultCreateRoomID, inputCreatePrice, selectedCreateAmenities, selectedCreateBedType);

                    // check the price before adding the room
                    if (standardRoom.IsValidPrice(standardRoom.GetPricePerNight()))
                    {
                        MessageBox.Show("Invalid price, try again.");
                    }
                    else
                    {
                        // add to db
                        RoomManager.AddNewStandardRoom(standardRoom);
                    }

                }
                else if (selectedCreateRoomType == "Suite")
                {
                    // create a suite room 
                    SuiteRoom suiteRoom = new SuiteRoom(defaultCreateRoomID, inputCreatePrice, selectedCreateAmenities, selectedCreateBedrooms, selectedCreateServices);

                    if (suiteRoom.IsValidPrice(suiteRoom.GetPricePerNight()))
                    {
                        MessageBox.Show("Invalid price, try again.");
                    }
                    else
                    {
                        // add to db
                        RoomManager.AddNewSuiteRoom(suiteRoom);
                    }
                }
                else if (selectedCreateRoomType == "Deluxe")
                {
                    // create a deluxe room 
                    DeluxeRoom deluxeRoom = new DeluxeRoom(defaultCreateRoomID, inputCreatePrice, selectedCreateAmenities, selectedCreateViewType);

                    if (deluxeRoom.IsValidPrice(deluxeRoom.GetPricePerNight()))
                    {
                        MessageBox.Show("Invalid price, try again.");
                    }
                    else
                    {
                        // add to db
                        RoomManager.AddNewDeluxeRoom(deluxeRoom);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // update room by validating user inputs
        public static void updateRoom(TextBox updateRoomNo, bool isRoomTypeChecked, bool isPriceChecked, bool isAmenitiesChecked, int selectedUpdateRoomID, string selectedUpdateRoomType, double inputUpdatePrice, string selectedUpdateAmenities) 
        {
            try
            {
                if (string.IsNullOrWhiteSpace(updateRoomNo.Text))
                {
                    // show an error message if room id was not entered
                    MessageBox.Show("Please enter Room No.");
                }
                else if (!isRoomTypeChecked && !isPriceChecked && !isAmenitiesChecked)
                {
                    // show an error message if no check boxes were checked
                    MessageBox.Show("Please choose an update.");
                }
                else
                {
                    // call the update room attribute functions according to the checked checkboxes
                    if (isRoomTypeChecked)
                    {
                        // the room type was checked to be updated
                        RoomManager.UpdateRoomType(selectedUpdateRoomID, selectedUpdateRoomType);
                    }
                    if (isPriceChecked)
                    {
                        // the room price was checked to be updated
                        RoomManager.UpdateRoomPrice(selectedUpdateRoomID, inputUpdatePrice);
                    }
                    if (isAmenitiesChecked)
                    {
                        // the room amenities was checked to be updated
                        RoomManager.UpdateRoomAmenities(selectedUpdateRoomID, selectedUpdateAmenities);
                    }


                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // delete room by validating the user inputs
        public static void deleteRoom(TextBox deleteRoomNo, int selectedDeleteRoomID)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(deleteRoomNo.Text))
                {
                    // show an error message if room id was not entered
                    MessageBox.Show("Please enter Room No.");
                }
                else
                {
                    // call the delete room function
                    RoomManager.DeleteRoom(selectedDeleteRoomID);

                    // clear the room id text field
                    deleteRoomNo.Text = "";
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // search availability based on attributes
        public static void searchForAvailableRoom(string selectedRoomType, string selectedBedType, int selectedBedrooms, string selectedViewType, Label resultField)
        {
            try
            {
                // initalize a list of room IDs
                List<int> availableRoomIDs = new List<int>();

                // get all rooms
                List<Room> rooms = RoomManager.GetAllRooms();

                // loop through all the rooms to find the room that fits the criteria
                foreach (Room room in rooms)
                {
                    // if the user has selected a room type
                    if (!string.IsNullOrWhiteSpace(selectedRoomType))
                    {
                        // check for the other criteria based on the room type and their relevant attributes
                        if (selectedRoomType == "Standard")
                        {
                            // check room type
                            if (room is StandardRoom standardRoom) // checks if the standard room type matches the user selected room type
                            {
                                // check for room specific attribute criteria if it was chosen
                                if (!string.IsNullOrWhiteSpace(selectedBedType))
                                {
                                    // get room attributes
                                    string bedType = standardRoom.GetBedType();

                                    // checks if the bed type matches with the user selection
                                    if (bedType == selectedBedType)
                                    {
                                        // add to available list
                                        availableRoomIDs.Add(standardRoom.GetRoomID());
                                    }
                                }
                                // if room specific attribute was not chosen and only the room type
                                else
                                {
                                    availableRoomIDs.Add(standardRoom.GetRoomID());
                                }
                            }
                        }
                        else if (selectedRoomType == "Suite")
                        {
                            if (room is SuiteRoom suiteRoom) // checks if the suite room type matches the user selected room type
                            {
                                // check for room specific attribute criteria if it was chosen
                                if (selectedBedrooms > 0)
                                {
                                    // get room attributes
                                    int bedrooms = suiteRoom.GetBedrooms();

                                    // checks if the bed type matches with the user selection
                                    if (bedrooms == selectedBedrooms)
                                    {
                                        // add to available list
                                        availableRoomIDs.Add(suiteRoom.GetRoomID());
                                    }
                                }
                                // if room specific attribute was not chosen and only the room type
                                else
                                {
                                    availableRoomIDs.Add(suiteRoom.GetRoomID());
                                }
                            }
                        }
                        else if (selectedRoomType == "Deluxe") 
                        {
                            if (room is DeluxeRoom deluxeRoom) // checks if the suite room type matches the user selected room type
                            {
                                // check for room specific attribute criteria if it was chosen
                                if (!string.IsNullOrWhiteSpace(selectedViewType))
                                {
                                    // get room attributes
                                    string viewType = deluxeRoom.GetViewType();

                                    // checks if the bed type matches with the user selection
                                    if (viewType == selectedViewType)
                                    {
                                        // add to available list
                                        availableRoomIDs.Add(deluxeRoom.GetRoomID());
                                    }
                                }
                                // if room specific attribute was not chosen and only the room type
                                else
                                {
                                    availableRoomIDs.Add(deluxeRoom.GetRoomID());
                                }
                            }
                        }
                        // when the user has not chosen a room type but other attributes

                        // check if the user has selected the bed type
                        else if (!string.IsNullOrWhiteSpace(selectedBedType))
                        {
                            // check if the room is a standard room in order to access the bed type attribute
                            if (room is StandardRoom standardRoom)
                            {
                                string bedType = standardRoom.GetBedType();

                                if (bedType == selectedBedType)
                                {
                                    availableRoomIDs.Add(standardRoom.GetRoomID());
                                }
                            }
                        }
                        // check if the user has selected the bed type
                        else if (selectedBedrooms > 0)
                        {
                            // check if the room is a suite room in order to access the bedrooms attribute
                            if (room is SuiteRoom suiteRoom)
                            {
                                int bedrooms = suiteRoom.GetBedrooms();

                                if (bedrooms == selectedBedrooms)
                                {
                                    availableRoomIDs.Add(suiteRoom.GetRoomID());
                                }
                            }
                        }
                        // check if the user has selected the view type
                        else if (!string.IsNullOrWhiteSpace(selectedViewType))
                        {
                            // check if the room is a deluxe room in order to access the view type attribute
                            if (room is DeluxeRoom deluxeRoom)
                            {
                                string viewType = deluxeRoom.GetViewType();

                                if (viewType == selectedViewType)
                                {
                                    availableRoomIDs.Add(deluxeRoom.GetRoomID());
                                }
                            }
                        }
                    }
                }

                // check if any rooms were found
                if (availableRoomIDs.Count == 0)
                {
                    MessageBox.Show("No results.");
                }
                else
                {
                    // display the list on the gui
                    resultField.Text = "Available Rooms: " + string.Join(", ", availableRoomIDs);
                    resultField.Visible = true;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void addRoomToTable(Room room, DataGridView AllRoomsTable)
        {
            // common room attributes
            string roomID = room.GetRoomID().ToString();
            string roomType = room.GetRoomType();
            string pricePerNight = $"LKR{room.GetPricePerNight():N0}"; // format price

            // get room-specific details from the overridden method
            Dictionary<string, string> details = room.GetRoomDetails();

            // Extract details safely
            string amenities = details.ContainsKey("Amenities") ? details["Amenities"] : "";
            string bedType = details.ContainsKey("BedType") ? details["BedType"] : "";
            string bedrooms = details.ContainsKey("Bedrooms") ? details["Bedrooms"] : "";
            string additionalServices = details.ContainsKey("AdditionalServices") ? details["AdditionalServices"] : "";
            string viewType = details.ContainsKey("ViewType") ? details["ViewType"] : "";

            // add data to the row
            AllRoomsTable.Rows.Add(roomID, roomType, pricePerNight, amenities, bedType, bedrooms, additionalServices, viewType);
        }

        public static void displayRooms(DataGridView AllRoomsTable) 
        {
            try
            {
                // get all the rooms
                List<Room> roomList = RoomManager.GetAllRooms();

                // display all the room entries
                foreach (Room room in roomList) Room.addRoomToTable(room, AllRoomsTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // check for room occupancy for a specific period of time 
        public static void CheckRoomOccupancy(int roomID, DateTime ? startDate, DateTime ? endDate, Label roomAvailableLabel, TextBox roomIDInput) 
        {
            try
            {
                // validate if the user has entered a roomid
                if (roomID == 0)
                {
                    MessageBox.Show("Please enter a Room No.");
                }

                // check if the user has entered both the start and end date of the period of time the room should be checked for occupancy
                if (!startDate.HasValue || !endDate.HasValue)
                {
                    MessageBox.Show("Please enter the start and end date to check for occupancy.");
                }

                // check if room is available
                bool isRoomAvailable = ReservationManager.SearchRoomAvailabilityByID(roomID, startDate.Value, endDate.Value);

                if (isRoomAvailable)
                {
                    roomAvailableLabel.Text = "Room is available.";
                }
                else
                {
                    roomAvailableLabel.Text = "Room is not available.";
                }

                roomAvailableLabel.Visible = true;
                roomIDInput.Text = "";
                startDate = DateTime.Now;
                endDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }

}
