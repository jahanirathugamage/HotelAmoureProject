using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelreservationsystem1
{
    public class StandardRoom : Room
    {
        private string bedType;

        public StandardRoom(int roomID, double pricePerNight, List<string> amenities, string bedType): base(roomID, "Standard", pricePerNight, amenities) 
        {
            this.bedType = bedType;
        }

        public string GetBedType() 
        { 
            return bedType;
        }

        public override Dictionary<string, string> GetRoomDetails()
        {
            Dictionary<string, string> details = base.GetRoomDetails();
            details["BedType"] = bedType ?? "Not specified"; 
            return details;
        }

    }

}
