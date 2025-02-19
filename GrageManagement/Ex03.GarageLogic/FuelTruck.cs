using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelTruck : Vehicle
    {
        public Truck m_Truck { get; private set; }
        public FuelSystem m_FuelSystem { get; private set; }

        private const eFuelType k_FuelType = eFuelType.Soler;      
        private const float k_MaxTankCapacity = 125f;   
        private const int k_NumberOfWheels = 14;       
        private const float k_MaxAirPressure = 29f;

        public FuelTruck(
        string i_ModelName,
        string i_LicenseNumber,
        float i_CargoVolume,
        bool i_IsCarryingCoolingMaterials,
        float i_FuelCapacity,
        float i_InitialFuelLevel)
        : base(i_ModelName, i_LicenseNumber, (i_InitialFuelLevel / i_FuelCapacity) * 100, Wheel.CreateWheels(k_NumberOfWheels, k_MaxAirPressure))
        {
            m_Truck = new Truck(i_CargoVolume, i_IsCarryingCoolingMaterials);
            m_FuelSystem = new FuelSystem(k_FuelType, i_FuelCapacity, i_InitialFuelLevel);
            m_TypeOfSystem = eTypeOfSystem.FuelVehicle;
        }

        public FuelTruck() : base()
        {
            m_Truck = new Truck();
            m_FuelSystem = new FuelSystem();
            m_Wheels = Wheel.CreateWheels(k_NumberOfWheels, k_MaxAirPressure);
            m_FuelSystem.m_FuelCapacity = k_MaxTankCapacity;
            m_FuelSystem.m_FuelType = k_FuelType;
            m_TypeOfSystem = eTypeOfSystem.FuelVehicle;
        }

        public override string GetVehicleInfo()
        {
            string vehicleInfo = $"Model Name: {m_ModelName}{Environment.NewLine}" +
                                 $"License Number: {m_LicenseNumber}{Environment.NewLine}" +
                                 $"Energy Percentage: {m_EnergyPercentage}%{Environment.NewLine}" +
                                 $"Number of Wheels: {m_Wheels.Count}{Environment.NewLine}";
            string truckInfo = m_Truck.GetVehicleInfo();
            string fuelSystemInfo = m_FuelSystem.GetFuelSystemInfo();
            return $"{vehicleInfo}{truckInfo}{fuelSystemInfo}";
        }
        public override Dictionary<string, string> GetQuestions()
        {
            if (!m_ClassQuestions.ContainsKey("CarryingCoolMaterials"))
            {
                m_ClassQuestions["CarryingCoolMaterials"] = "Is the truck carrying cooling materials? (yes/no):";
            }

            if (!m_ClassQuestions.ContainsKey("CargoVolume"))
            {
                m_ClassQuestions["CargoVolume"] = "Enter the cargo volume (in cubic meters):";
            }

            if (m_ClassQuestions.ContainsKey("FuelLevel"))
            {
                m_ClassQuestions["FuelLevel"] = "Enter initial fuel level:";
            }

            return m_ClassQuestions;
        }
        public override void SetProperties(Dictionary<string, string> i_Properties)
        {
            if (i_Properties.TryGetValue("CarryingCoolMaterials", out string carryingCoolMaterialsStr))
            {
                if (bool.TryParse(carryingCoolMaterialsStr, out bool carryingCoolMaterials))
                {
                    m_Truck.m_IsCarryingCoolingMaterials = carryingCoolMaterials;
                    m_EnergyPercentage = (m_FuelSystem.m_CurrentFuelLevel / m_FuelSystem.m_FuelCapacity) * 100;
                }
                else
                {
                    throw new FormatException($"Invalid format for CarryingCoolMaterials: {carryingCoolMaterialsStr}. Please enter 'true' or 'false'.");
                }
            }

            if (i_Properties.TryGetValue("CargoVolume", out string cargoVolumeStr))
            {
                if (float.TryParse(cargoVolumeStr, out float cargoVolume))
                {
                    if (cargoVolume >= 0)
                    {
                       m_Truck.m_CargoVolume =cargoVolume;
                    }
                }
                else
                {
                    throw new FormatException($"Invalid format for CargoVolume: {cargoVolumeStr}. Please enter a valid number.");
                }
            }

            if (i_Properties.TryGetValue("FuelLevel", out string fuelLevelStr))
            {
                if (float.TryParse(fuelLevelStr, out float fuelLevel))
                {

                    if (fuelLevel >= 0 && fuelLevel <= k_MaxTankCapacity)
                    {
                        m_FuelSystem.m_CurrentFuelLevel = fuelLevel;
                    }
                    else
                    {
                        throw new ValueOutOfRangeException(0, k_MaxTankCapacity);
                    }
                }
                else
                {
                    throw new FormatException($"Invalid fuel level format: {fuelLevelStr}. Please enter a valid number.");
                }
            }
        }

        public override void RefuelOrRecharge(float i_AmountToRefuel, string i_FuelTypeString)
        {
            if (m_TypeOfSystem == eTypeOfSystem.ElectricVehicle)
            {
                throw new Exception("Electric vehicles cannot be refueled. Please recharge instead.");
            }

            eFuelType fuelType;
            bool isValidFuelType = Enum.TryParse(i_FuelTypeString, true, out fuelType);
            if (!isValidFuelType)
            {
                throw new FormatException($"Invalid fuel type format: '{i_FuelTypeString}'. Please provide a valid fuel type.");
            }

            if (!Enum.IsDefined(typeof(eFuelType), fuelType))
            {
                throw new ArgumentException($"Invalid fuel type: '{i_FuelTypeString}'. This fuel type is not valid.");
            }

            m_FuelSystem.Refuel(i_AmountToRefuel, fuelType);
        }
    }
}
