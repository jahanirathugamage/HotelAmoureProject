using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace hotelreservationsystem1
{
    public partial class RoomControlPanel : Form
    {
        // initialise the create room attributes
        private int defaultCreateRoomID = 1; // room id is irrelevant as it is not going to be added to db
        private string selectedCreateRoomType = "Standard";
        private double inputCreatePrice = 0.00;
        private List<string> selectedCreateAmenities = new List<string> { "WiFi", "TV", "AC", "Hot Water", "Desk"};
        private string selectedCreateBedType = "Double";
        private int selectedCreateBedrooms = 1;
        private List<string> selectedCreateServices = new List<string> { "Butler", "Private Pool", "Personal Chef"};
        private string selectedCreateViewType = "Ocean";

        // initialise the update room attributes
        private int selectedUpdateRoomID = 0; 
        private string selectedUpdateRoomType = "Standard";
        private double inputUpdatePrice = 0.00;
        private string selectedUpdateAmenities = "WiFi, TV, AC, Hot Water, Desk";


        // default checkbox values
        private bool isRoomTypeChecked = false;
        private bool isPriceChecked = false;
        private bool isAmenitiesChecked = false;

        // initalise the delete room ID
        private int selectedDeleteRoomID = 0;

        public RoomControlPanel()
        {
            InitializeComponent();

            // call the create room drop down menu populator
            populateCreateRoomMenu();

            // call the update room drop down menu populator
            populateUpdateRoomMenu();

        }

        // populate the create room down menus
        private void populateCreateRoomMenu()
        {
            // room type
            createRoomType.Items.Add("Standard");
            createRoomType.Items.Add("Suite");
            createRoomType.Items.Add("Deluxe");

            // set default selection
            createRoomType.SelectedIndex = 0;

            // amenities
            createAmenities.Items.Add("WiFi,TV,AC,Hot Water,Desk");
            createAmenities.Items.Add("WiFi,TV,AC,Minibar,Desk");

            createAmenities.SelectedIndex = 0;

            // bed type
            createBedType.Items.Add("Double");
            createBedType.Items.Add("Queen");
            createBedType.Items.Add("King");

            createBedType.SelectedIndex = 0;

            // bedrooms
            createBedrooms.Items.Add("1");
            createBedrooms.Items.Add("2");
            createBedrooms.Items.Add("3");

            createBedrooms.SelectedIndex = 0;

            // additional services
            createServices.Items.Add("Butler,Private Pool,Personal Chef");

            createServices.SelectedIndex = 0;

            // view type
            createViewType.Items.Add("Ocean");
            createViewType.Items.Add("Garden");
            createViewType.Items.Add("City");

            createViewType.SelectedIndex = 0;
        }

        // populate the create room down menus
        private void populateUpdateRoomMenu()
        {
            // room type
            updateRoomType.Items.Add("Standard");
            updateRoomType.Items.Add("Suite");
            updateRoomType.Items.Add("Deluxe");

            // set default selection
            updateRoomType.SelectedIndex = 0;

            // amenities
            updateAmenities.Items.Add("WiFi,TV,AC,Hot Water,Desk");
            updateAmenities.Items.Add("WiFi,TV,AC,Minibar,Desk");

            updateAmenities.SelectedIndex = 0;
        }

        // clear out the comboBox and textbox data after create room submit button is clicked
        private void clearCreateRoomData() 
        {
            createRoomType.SelectedIndex = 0;
            createPrice.Text = "";
            createAmenities.SelectedIndex = 0;
            createBedType.SelectedIndex = 0;
            createBedrooms.SelectedIndex = 0;
            createServices.SelectedIndex = 0;
            createViewType.SelectedIndex = 0;
        }

        // clear out the comboBox and textbox data after update room submit button is clicked
        private void clearUpdateRoomData()
        {
            updateRoomNo.Text = "";
            updateRoomType.SelectedIndex = 0;
            updatePrice.Text = "";
            updateAmenities.SelectedIndex = 0;
            updateRoomTypeCheckbox.Checked = false;
            updatePriceCheckbox.Checked = false;
            updateAmenitiesCheckbox.Checked = false;
        }

        private void home_homeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form AdminDashboard = new AdminDashboard();
            AdminDashboard.ShowDialog();
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void home_roomsBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form AdminRoomAvailability = new AdminRoomAvailability();
            AdminRoomAvailability.ShowDialog();
        }

        private void home_reserveBtn_Click(object sender, EventArgs e){}

        private void home_offersBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form ManageOffers = new ManageOffers();
            ManageOffers.ShowDialog();
        }

        private void home_feedback_Click(object sender, EventArgs e)
        {
            this.Close();
            Form ManageFeedback = new ManageFeedback();
            ManageFeedback.ShowDialog();
        }

        private void home_profileBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form AdminProfile = new AdminProfile();
            AdminProfile.ShowDialog();
        }

        // store the user selected data for create room 
        private void createRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCreateRoomType = createRoomType.SelectedItem.ToString(); 
        }

        private void createPrice_TextChanged(object sender, EventArgs e)
        {
            // TryParse to handle potential parsing errors
            if (double.TryParse(createPrice.Text, out double parsedPrice))
            {
                inputCreatePrice = parsedPrice;
            }
            else
            {
                // handle the case where the input is not a valid number
                inputCreatePrice = 0.00;
            }
        }

        private void createAmenities_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get selection as a string
            string selectedAmenitiesRaw = createAmenities.SelectedItem.ToString();

            // trim spaces around each item and convert to a list
            selectedCreateAmenities = selectedAmenitiesRaw.Split(',').Select(item => item.Trim()).ToList();
        }

        private void createBedType_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCreateBedType = createBedType.SelectedItem.ToString();  
        }

        private void createBedrooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get the user input as string
            string selectedBedroomsRaw = createBedrooms.SelectedItem.ToString();

            // convert to int
            selectedCreateBedrooms = int.Parse(selectedBedroomsRaw);
        }

        private void createServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get selection as a string
            string selectedServicesRaw = createServices.SelectedItem.ToString();

            // trim spaces around each item and convert to a list
            selectedCreateServices = selectedServicesRaw.Split(',').Select(item => item.Trim()).ToList();
        }

        private void createViewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCreateViewType = createViewType.SelectedItem.ToString();
        }

        // actions that happen when user clicks create room submit button
        private void createRoom_submitBtn_Click(object sender, EventArgs e)
        {
            // call the function that will validate the user inputs, create a room object and pass it to the function that will add it to the db
            Room.createRoom(selectedCreateRoomType, defaultCreateRoomID, inputCreatePrice, selectedCreateAmenities, selectedCreateBedType, selectedCreateBedrooms, selectedCreateServices, selectedCreateViewType);

            clearCreateRoomData();
        }

        // store the user selected data for update room
        private void updateRoomNo_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(updateRoomNo.Text, out int updateRoomID))
            {
                selectedUpdateRoomID = updateRoomID;
            }
            else
            {
                selectedUpdateRoomID = 0;
            }
        }

        private void updateRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedUpdateRoomType = updateRoomType.SelectedItem.ToString();
        }

        private void updatePrice_TextChanged(object sender, EventArgs e)
        {
            // TryParse to handle potential parsing errors
            if (double.TryParse(updatePrice.Text, out double parsedUpdatePrice))
            {
                inputUpdatePrice = parsedUpdatePrice;
            }
            else
            {
                // handle the case where the input is not a valid number
                inputUpdatePrice = 0.00;
            }
        }

        private void updateAmenities_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedUpdateAmenities = updateAmenities.SelectedItem?.ToString() ?? "";
        }


        private void updateBedType_SelectedIndexChanged(object sender, EventArgs e){}

        private void updateBedrooms_SelectedIndexChanged(object sender, EventArgs e){}

        private void updateServices_SelectedIndexChanged(object sender, EventArgs e){}

        private void updateViewType_SelectedIndexChanged(object sender, EventArgs e){}
        
        // actions that occur when user clicks update room submit button
        private void updateRoom_submitBtn_Click(object sender, EventArgs e)
        {
            Room.updateRoom(updateRoomNo, isRoomTypeChecked, isPriceChecked, isAmenitiesChecked, selectedUpdateRoomID, selectedUpdateRoomType, inputUpdatePrice, selectedUpdateAmenities);
            clearUpdateRoomData();
        }

        private void panel4_Paint(object sender, PaintEventArgs e){}

        // check which attribute the user picked to update
        private void updateRoomTypeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            // if checked, it becomes true; if unchecked, it stays false
            isRoomTypeChecked = updateRoomTypeCheckbox.Checked; 
        }

        private void updatePriceCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            isPriceChecked = updatePriceCheckbox.Checked;
        }

        private void updateAmenitiesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            isAmenitiesChecked = updateAmenitiesCheckbox.Checked;
        }

        private void deleteRoomNo_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(deleteRoomNo.Text, out int deleteRoomID))
            {
                selectedDeleteRoomID = deleteRoomID;
            }
            else
            {
                selectedDeleteRoomID = 0;
            }
        }
        
        // actions that occur when user clicks delete confirm button
        private void deleteRoom_confirmBtn_Click(object sender, EventArgs e)
        {
            Room.deleteRoom(deleteRoomNo, selectedDeleteRoomID);
        }
    }
}
