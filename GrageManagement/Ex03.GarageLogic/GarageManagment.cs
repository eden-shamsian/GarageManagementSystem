using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ex03.GarageLogic
{

    public static class GarageManagment
    {
        public static readonly Dictionary<string, VehicleRecord> r_Vehicles = new Dictionary<string, VehicleRecord>();
        public static StringBuilder GetLicensePlatesByStatus(eVehicleStatus i_Status)
        {
            StringBuilder licensePlates = new StringBuilder();

            if (r_Vehicles.Count == 0)
            {
                licensePlates.AppendLine("No vehicles found in the garage.");
            }

            foreach (VehicleRecord i_vehicleRecord in r_Vehicles.Values)
            {
                if (i_vehicleRecord.m_VehicleStatus == i_Status)
                {
                    licensePlates.AppendLine(i_vehicleRecord.m_Vehicle.m_LicenseNumber);
                }
            }

            return licensePlates;
        }

        public static StringBuilder GetAllLicensePlates()
        {
            StringBuilder licensePlates = new StringBuilder();

            if (r_Vehicles.Count == 0)
            {
                licensePlates.AppendLine("No vehicles found in the garage.");
            }


            licensePlates.AppendLine("License plates of all vehicles:");
            foreach (VehicleRecord i_vehicleRecord in r_Vehicles.Values)
            {
                licensePlates.AppendLine(i_vehicleRecord.m_Vehicle.m_LicenseNumber);
            }

            return licensePlates;
        }

        public static void ChangeVehicleStatus(string i_LicensePlate, eVehicleStatus i_NewStatus)
        {
            if (r_Vehicles.TryGetValue(i_LicensePlate, out VehicleRecord vehicleRecord))
            {
                if (vehicleRecord.m_VehicleStatus == i_NewStatus)
                {
                    throw new ArgumentException($"The status of the vehicle with license plate {i_LicensePlate} is already {i_NewStatus}.");
                }
                else
                {
                    vehicleRecord.m_VehicleStatus = i_NewStatus;
                }
            }
        }

        public static bool CheckIfVehicleInGarage(string i_LicenceNumber)
        {
            return r_Vehicles.ContainsKey(i_LicenceNumber);
        }
        public static string GetVehicleDetails(string i_LicensePlate)
        {
            StringBuilder details = new StringBuilder();
            VehicleRecord vehicleRecord;
            Vehicle vehicle;

            if (!CheckIfVehicleInGarage(i_LicensePlate))
            {
                throw new KeyNotFoundException($"No vehicle found with license plate {i_LicensePlate}.");
            }

            vehicleRecord = r_Vehicles[i_LicensePlate];
            vehicle = vehicleRecord.m_Vehicle;
            details.AppendLine(vehicle.GetVehicleInfo());
            details.AppendLine("Wheels:");
            foreach (Wheel wheel in vehicle.m_Wheels)
            {
                details.AppendLine($"\tManufacturer: {wheel.m_Manufacturer}, Current Air Pressure: {wheel.m_CurrentAirPressure} / Max Air Pressure: {wheel.m_MaxAirPressure}");
            }

            details.AppendLine($"Owner Name: {vehicleRecord.m_OwnerName}");
            details.AppendLine($"Garage Status: {vehicleRecord.m_VehicleStatus}");

            return details.ToString();
        }

        public static void SetAllWheels(Vehicle i_Vehicle, float i_AirPressure, string i_Manufacturer)
        {
            foreach (Wheel wheel in i_Vehicle.m_Wheels)
            {
                if (i_AirPressure < 0 || i_AirPressure > wheel.m_MaxAirPressure)
                {
                    throw new ValueOutOfRangeException(0, wheel.m_MaxAirPressure, $"Air pressure must be between 0 and {wheel.m_MaxAirPressure} PSI.");
                }

                wheel.m_CurrentAirPressure = i_AirPressure;
                wheel.m_Manufacturer = i_Manufacturer;
            }
        }

        public static void SetWheel(Vehicle i_Vehicle, int i_WheelIndex, float i_AirPressure, string i_Manufacturer)
        {
            Wheel wheel;
            if (i_WheelIndex < 0 || i_WheelIndex >= i_Vehicle.m_Wheels.Count)
            {
                throw new ArgumentException("Invalid wheel index.");
            }

            wheel = i_Vehicle.m_Wheels[i_WheelIndex];
            if (i_AirPressure < 0 || i_AirPressure > wheel.m_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, wheel.m_MaxAirPressure, $"Air pressure must be between 0 and {wheel.m_MaxAirPressure} PSI.");
            }

            wheel.m_CurrentAirPressure = i_AirPressure;
            wheel.m_Manufacturer = i_Manufacturer;
        }

        public static Vehicle GetVehicleByLicense(string i_LicensePlate)
        {
            if (string.IsNullOrWhiteSpace(i_LicensePlate))
            {
                throw new FormatException("Invalid input: License plate cannot be null or whitespace.");
            }

            if (r_Vehicles.TryGetValue(i_LicensePlate, out VehicleRecord vehicle))
            {
                return vehicle.m_Vehicle;
            }

            else
            {
                throw new KeyNotFoundException($"No vehicle with license plate '{i_LicensePlate}' was found in the garage.");
            }
        }

        public static void InflateAllWheelsToMax(string i_LicensePlate)
        {
            Vehicle vehicle = GetVehicleByLicense(i_LicensePlate);
            List<Wheel> listOfWheels = vehicle.m_Wheels;

            foreach (Wheel wheel in listOfWheels)
            {
                float amountToInflate = wheel.m_MaxAirPressure - wheel.m_CurrentAirPressure;
                wheel.Inflate(amountToInflate);
            }
        }
    }
}

