using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hotelreservationsystem1
{
    internal class SpecialOffer
    {
        // attributes
        private int offerID;
        private string offerName;
        private int discountPercentage;
        private DateTime startDate;
        private DateTime endDate;
        private List<string> applicableRoomTypes;
        private bool offerStatus;

        public SpecialOffer(int offerID, string offerName, int discountPercentage, DateTime startDate, DateTime endDate, List<string> applicableRoomTypes, bool offerStatus) 
        { 
            this.offerID = offerID;
            this.offerName = offerName;
            this.discountPercentage = discountPercentage;
            this.startDate = startDate;
            this.endDate = endDate;
            this.applicableRoomTypes = new List<string>(applicableRoomTypes);
            this.offerStatus = offerStatus;
        }

        // setters and getters
        public int getOfferID() 
        {
            return offerID;
        }

        public string getOfferName() 
        { 
            return offerName; 
        }

        public int getDiscountPercentage() 
        { 
            return discountPercentage; 
        }

        public DateTime getStartDate() 
        { 
            return startDate; 
        }

        public DateTime getEndDate() 
        { 
            return endDate;
        }

        public bool getOfferStatus() 
        { 
            return offerStatus; 
        }

        public void setOfferId(int offerID) 
        { 
            this.offerID = offerID;
        }

        public void setOfferName(string offerName) 
        { 
            this.offerName = offerName; 
        }

        public void setDiscountPercentage(int discountPercentage) 
        { 
            this.discountPercentage = discountPercentage;
        }

        public void setStartDate(DateTime startDate) 
        { 
            this.startDate = startDate;
        }

        public void setEndDate(DateTime endDate) 
        { 
            this.endDate = endDate;
        }

        public void setOfferStatus(bool offerStatus) 
        { 
            this.offerStatus = offerStatus; 
        }

        public List<string> ApplicableRoomTypes
        {
            // return a copy to prevent changes
            get
            {
                return new List<string>(applicableRoomTypes);
            }
            set
            {
                // create a new is to to avoid modifying the original list
                applicableRoomTypes = new List<string>(value);
            }
        }

        public static bool isDiscountValid(int discount) 
        {
            if ((discount <= 0) || (discount > 100))
            {
                return true;
            }
            else 
            { 
                return false;
            }
        }

        public static bool areDatesValid(DateTime startDate, DateTime endDate) 
        {
            if (startDate >= endDate) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // add offer to a table
        public static void addOfferToTable(SpecialOffer specialOffer, DataGridView activeSpecialOfferTable)
        {
            // get data
            string offerID = specialOffer.getOfferID().ToString();
            string offerName = specialOffer.getOfferName();
            string discountPercentage = $"{specialOffer.getDiscountPercentage():00}%";
            string startDate = specialOffer.getStartDate().ToString("yyyy-MM-dd");
            string endDate = specialOffer.getEndDate().ToString("yyyy-MM-dd");
            string applicableRooms = string.Join(", ", specialOffer.applicableRoomTypes);

            // add data to the row
            activeSpecialOfferTable.Rows.Add(offerID, offerName, discountPercentage, applicableRooms, startDate, endDate);
        }

        public static void displayActiveSpecialOffers(DataGridView activeSpecialOfferTable)
        {
            try
            {
                // get all of the offers
                List<SpecialOffer> specialOfferList = SpecialOfferManager.GetAllActiveSpecialOffers();

                // populate the table rows 
                foreach (SpecialOffer specialOffer in specialOfferList) addOfferToTable(specialOffer, activeSpecialOfferTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void createSpecialOffer(TextBox createOfferName, bool isStandardRoomChecked, bool isSuiteRoomChecked, bool isDeluxeRoomChecked, List<string> selectedCreateApplicableRoomTypes, int DefaultOfferID, string selectedCreateOfferName, int selectedCreateDiscount, DateTime selectedCreateStartDate, DateTime selectedCreateEndDate, bool DefaultOfferStatus)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(createOfferName.Text))
                {
                    // show an error message if offer name was not entered
                    MessageBox.Show("Please enter Offer No.");
                }

                // validate if the user has picked an applicable room
                else if (!isStandardRoomChecked && !isSuiteRoomChecked && !isDeluxeRoomChecked)
                {
                    MessageBox.Show("Please select applicable room types.");
                    return;
                }

                // get the applicable room types chosen into a list
                if (isStandardRoomChecked)
                {
                    selectedCreateApplicableRoomTypes.Add("Standard");
                }
                if (isSuiteRoomChecked)
                {
                    selectedCreateApplicableRoomTypes.Add("Suite");
                }
                if (isDeluxeRoomChecked)
                {
                    selectedCreateApplicableRoomTypes.Add("Deluxe");
                }

                // create offer object
                SpecialOffer specialOffer = new SpecialOffer(DefaultOfferID, selectedCreateOfferName, selectedCreateDiscount, selectedCreateStartDate, selectedCreateEndDate, selectedCreateApplicableRoomTypes, DefaultOfferStatus);

                if (isDiscountValid(specialOffer.getDiscountPercentage()))
                {
                    MessageBox.Show("Please enter a valid discount.");
                    return;
                }
                else if (SpecialOffer.areDatesValid(specialOffer.getStartDate(), specialOffer.getEndDate()))
                {
                    MessageBox.Show("Start date must be before end date.");
                    return;
                }
                else
                {
                    // call the add offer to the db function
                    SpecialOfferManager.AddNewSpecialOffer(specialOffer);
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.ToString());
            }
        }

        // validate the user inputs and call the function that will update the offer in the db
        public static void updateSpecialOffer(TextBox updateOfferNo, bool isDiscountChecked, bool isStartDateChecked, bool isEndDateChecked, bool isActivateBtnClicked, int selectedUpdateDiscount, DateTime selectedUpdateStartDate, DateTime selectedUpdateEndDate, int selectedUpdateOfferID, string updateOfferStatusToActive)
        {
            try 
            {
                if (string.IsNullOrWhiteSpace(updateOfferNo.Text))
                {
                    // show an error message if room id was not entered
                    MessageBox.Show("Please enter Offer No.");
                }

                else if (!isDiscountChecked && !isStartDateChecked && !isEndDateChecked && !isActivateBtnClicked)
                {
                    // show an error message if no check boxes were checked and activation button wasn't clicked
                    MessageBox.Show("Please choose an update.");
                }

                // validate the user input discount if the discount update was chosen
                else if ((isDiscountChecked) && (selectedUpdateDiscount > 100 || selectedUpdateDiscount <= 0))
                {
                    MessageBox.Show("Please enter a valid discount.");
                    return;
                }
                // if the user has chosen to update both the start and end date
                else if (isStartDateChecked && isEndDateChecked)
                {
                    // valiate if the start date is before the end date
                    if (selectedUpdateStartDate >= selectedUpdateEndDate)
                    {
                        MessageBox.Show("Start date must be before end date.");
                        return;
                    }
                }
                else
                {
                    // call the update offer attribute functions according to the checked checkboxes or activate button
                    if (isDiscountChecked)
                    {
                        // the offer discount was checked to be updated
                        SpecialOfferManager.UpdateOfferDiscount(selectedUpdateOfferID, selectedUpdateDiscount);
                    }
                    if (isStartDateChecked)
                    {
                        // the offer start date was checked to be updated
                        SpecialOfferManager.UpdateOfferStartDate(selectedUpdateOfferID, selectedUpdateStartDate);
                    }
                    if (isEndDateChecked)
                    {
                        // the offer end date was checked to be updated
                        SpecialOfferManager.UpdateOfferEndDate(selectedUpdateOfferID, selectedUpdateEndDate);
                    }
                    if (isActivateBtnClicked)
                    {
                        // to activate the offer button was clicked
                        SpecialOfferManager.ActivateOffer(selectedUpdateOfferID, updateOfferStatusToActive);
                    }

                    MessageBox.Show("Offer updated successfully!");

                }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.ToString()); 
            }
        }

        public static void deactivateOffer(TextBox deleteOfferNo, string updateOfferStatusToUnactive, int selectedDeactivateOfferID) 
        {
            try
            {
                if (string.IsNullOrWhiteSpace(deleteOfferNo.Text))
                {
                    // show an error message if room id was not entered
                    MessageBox.Show("Please enter Offer No.");
                }
                else
                {
                    updateOfferStatusToUnactive = "Deactivated";

                    SpecialOfferManager.DeactivateOffer(selectedDeactivateOfferID, updateOfferStatusToUnactive);

                    MessageBox.Show("Offer Deactivated.");

                    deleteOfferNo.Text = "";
                    updateOfferStatusToUnactive = "";
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
