using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelreservationsystem1
{
    public class SuiteRoom : Room
    {
        private int bedrooms;
        private List<string> additionalServices;

        public SuiteRoom(int roomID, double pricePerNight, List<string> amenities, int bedrooms, List<string> additionalServices): base(roomID, "Suite", pricePerNight, amenities)
        {
            this.bedrooms = bedrooms;
            this.additionalServices = additionalServices;
        }

        public int GetBedrooms() 
        {
            return this.bedrooms;
        }

        public List<string> GetAdditionalServices() 
        { 
            return this.additionalServices;
        }

        public override Dictionary<string, string> GetRoomDetails()
        {
            Dictionary<string, string> details = base.GetRoomDetails();
            details["Bedrooms"] = bedrooms.ToString();
            details["AdditionalServices"] = additionalServices != null ? string.Join(", ", additionalServices) : "None";
            return details;
        }
    }
}
