using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hotelreservationsystem1
{
    public partial class ManageOffers : Form
    {
        // initialize the create offer attributes
        private int DefaultOfferID = 1; // offer id is irrelevant as it is not going to be added to the db
        private string selectedCreateOfferName = "";
        private int selectedCreateDiscount = 0;
        private DateTime selectedCreateStartDate;
        private DateTime selectedCreateEndDate;
        private List<string> selectedCreateApplicableRoomTypes = new List<string>();
        private bool DefaultOfferStatus = true; // newly created offer is available by default

        // initialize the applicable room check box data
        private bool isStandardRoomChecked = false;
        private bool isSuiteRoomChecked = false;
        private bool isDeluxeRoomChecked = false;

        // initialize the update offer attributes
        private int selectedUpdateOfferID = 1;
        private int selectedUpdateDiscount = 0;
        private DateTime selectedUpdateStartDate;
        private DateTime selectedUpdateEndDate;

        // initialize the update field choice check box
        private bool isDiscountChecked = false;
        private bool isStartDateChecked = false;
        private bool isEndDateChecked = false;

        // initialize the activate button value and offer status keyword
        private bool isActivateBtnClicked = false;
        private string updateOfferStatusToActive = "";

        // initialize the deactivate offer id
        private int selectedDeactivateOfferID = 1;

        // initialize the deactivate offer status keyword
        private string updateOfferStatusToUnactive = "";

        public ManageOffers()
        {
            InitializeComponent();

            InitializeTable();

            SpecialOffer.displayActiveSpecialOffers(activeSpecialOfferTable);

        }

        // initialize the table
        private void InitializeTable() 
        {
            // clear existing columns and rows if any
            activeSpecialOfferTable.Columns.Clear();
            activeSpecialOfferTable.Rows.Clear();

            // add columns to the table
            activeSpecialOfferTable.Columns.Add("offerID", "Offer No");
            activeSpecialOfferTable.Columns.Add("offerName", "Name");
            activeSpecialOfferTable.Columns.Add("discountPercentage", "Discount");
            activeSpecialOfferTable.Columns.Add("applicableRooms", "Rooms");
            activeSpecialOfferTable.Columns.Add("startDate", "Start Date");
            activeSpecialOfferTable.Columns.Add("endDate", "End Date");
        }

        
        // clear out the date picker, textboxes and checkboxes data
        private void clearCreateOfferData() 
        {
            createOfferName.Text = "";
            createDiscount.Text = "";

            createStartDate.Value = DateTime.Now;
            createEndDate.Value = DateTime.Now;

            createApplicableStandardRoom.Checked = false;
            createApplicableSuiteRoom.Checked = false;
            createApplicableDeluxeRoom.Checked = false;
        }

        // clear out the date pick, textboxes and checkbox data
        private void clearUpdateOfferData() 
        {
            updateOfferNo.Text = "";
            updateDiscount.Text = "";

            updateStartDate.Value = DateTime.Now;
            updateEndDate.Value = DateTime.Now;

            updateDiscountCheckbox.Checked = false;
            updateStartDateCheckbox.Checked = false;
            updateEndDateCheckbox.Checked = false;

            isActivateBtnClicked = false;
            updateOfferStatusToActive = "";
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void offers_homeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form AdminDashboard = new AdminDashboard();
            AdminDashboard.ShowDialog();
        }

        private void offers_roomsBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form AdminRoomAvailability = new AdminRoomAvailability();
            AdminRoomAvailability.ShowDialog();
        }

        private void offers_manageBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form RoomControlPanel = new RoomControlPanel();
            RoomControlPanel.ShowDialog();
        }

        private void offers_offersBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void offers_feedback_Click(object sender, EventArgs e)
        {
            this.Close();
            Form ManageFeedback = new ManageFeedback();
            ManageFeedback.ShowDialog();
        }

        private void offers_profileBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            Form AdminProfile = new AdminProfile();
            AdminProfile.ShowDialog();
        }

        // get user input data from fields
        private void createOfferName_TextChanged(object sender, EventArgs e)
        {
            selectedCreateOfferName = createOfferName.Text;
        }

        private void createDiscount_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(createDiscount.Text, out int createDiscountTemp))
            {
                selectedCreateDiscount = createDiscountTemp; 
            }
            else
            {
                selectedCreateDiscount = 0; 
            }
        }

        private void createStartDate_ValueChanged(object sender, EventArgs e)
        {
            selectedCreateStartDate = createStartDate.Value;

        }

        private void createEndDate_ValueChanged(object sender, EventArgs e)
        {
            selectedCreateEndDate = createEndDate.Value;
        }

        private void createApplicableStandardRoom_CheckedChanged(object sender, EventArgs e)
        {
            isStandardRoomChecked = createApplicableStandardRoom.Checked;
        }

        private void createApplicableSuiteRoom_CheckedChanged(object sender, EventArgs e)
        {
            isSuiteRoomChecked = createApplicableSuiteRoom.Checked;
        }

        private void createApplicableDeluxeRoom_CheckedChanged(object sender, EventArgs e)
        {
            isDeluxeRoomChecked = createApplicableDeluxeRoom.Checked;
        }

        // actions that happen when the user clicks the create offer submit button
        private void createOffer_submitBtn_Click(object sender, EventArgs e)
        {
            SpecialOffer.createSpecialOffer(createOfferName, isStandardRoomChecked, isSuiteRoomChecked, isDeluxeRoomChecked, selectedCreateApplicableRoomTypes, DefaultOfferID, selectedCreateOfferName, selectedCreateDiscount, selectedCreateStartDate, selectedCreateEndDate, DefaultOfferStatus);

            clearCreateOfferData();
        }

        // get the user input update offer data
        private void updateOfferNo_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(updateOfferNo.Text, out int updateOfferID))
            {
                selectedUpdateOfferID = updateOfferID;
            }
            else
            {
                selectedUpdateOfferID = 0;
            }
        }

        private void updateDiscount_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(updateDiscount.Text, out int updateDiscountTemp))
            {
                selectedUpdateDiscount = updateDiscountTemp;
            }
            else
            {
                selectedUpdateDiscount = 0;
            }
        }

        private void updateStartDate_ValueChanged(object sender, EventArgs e)
        {
            selectedUpdateStartDate = updateStartDate.Value;
        }

        private void updateEndDate_ValueChanged(object sender, EventArgs e)
        {
            selectedUpdateEndDate = updateEndDate.Value;
        }

        private void updateDiscountCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            isDiscountChecked = updateDiscountCheckbox.Checked;
        }

        private void updateStartDateCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            isStandardRoomChecked = updateStartDateCheckbox.Checked;
        }

        private void updateEndDateCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            isEndDateChecked = updateEndDateCheckbox.Checked;
        }

        // the actions that happen when the user clicks the update offer submit offer
        private void updateOffer_submitBtn_Click(object sender, EventArgs e)
        {
            // call the create special offer that will validate the inputs
            SpecialOffer.updateSpecialOffer(updateOfferNo, isDiscountChecked, isStartDateChecked, isEndDateChecked, isActivateBtnClicked, selectedUpdateDiscount, selectedUpdateStartDate, selectedUpdateEndDate, selectedUpdateOfferID, updateOfferStatusToActive);

            clearUpdateOfferData();
        }

        // if the user clicks the activate offer status button
        private void updateActivateOfferBtn_Click(object sender, EventArgs e)
        {
            // activation if offer is chosen
            isActivateBtnClicked = true;

            // store the Active word in a string to store in db
            updateOfferStatusToActive = "Active";
        }

        private void deleteOfferNo_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(deleteOfferNo.Text, out int deactivateOfferID))
            {
                selectedDeactivateOfferID = deactivateOfferID;
            }
            else
            {
                selectedDeactivateOfferID = 0;
            }
        }

        // actions that happen when the user clicks the deactivate offer confirm button
        private void deleteOffer_confirmBtn_Click(object sender, EventArgs e)
        {
            SpecialOffer.deactivateOffer(deleteOfferNo, updateOfferStatusToUnactive, selectedDeactivateOfferID);

            deleteOfferNo.Text = "";
        }
    }
}
