using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleRecord
    {
        public Vehicle m_Vehicle { get; private set; }

        public eVehicleStatus m_VehicleStatus { get; set; }

        public string m_OwnerName { get; private set; }

        public string m_OwnerPhone { get; private set; }

        public VehicleRecord(Vehicle i_Vehicle, eVehicleStatus i_Status, string i_OwnerName, string i_OwnerPhone)
        {
            m_Vehicle = i_Vehicle;
            m_VehicleStatus = i_Status;
            m_OwnerName = i_OwnerName;
            m_OwnerPhone = i_OwnerPhone;
        }
    }
}
