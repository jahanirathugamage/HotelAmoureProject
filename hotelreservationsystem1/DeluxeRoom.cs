using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelreservationsystem1
{
    public class DeluxeRoom : Room
    {
        private string viewType;

        // constructor
        public DeluxeRoom(int roomID, double pricePerNight, List<string> amenities, string viewType): base(roomID, "Deluxe", pricePerNight, amenities)
        {
            this.viewType = viewType;
        }

        // getter
        public string GetViewType() 
        { 
            return viewType;
        }

        public override Dictionary<string, string> GetRoomDetails()
        {
            Dictionary<string, string> details = base.GetRoomDetails();
            details["ViewType"] = viewType;
            return details;
        }
    }

}
