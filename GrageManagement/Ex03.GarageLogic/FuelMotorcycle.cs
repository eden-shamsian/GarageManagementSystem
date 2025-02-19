using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : Vehicle
    {

        public Motorcycle m_Motorcycle { get; private set; } 
        public FuelSystem m_FuelSystem { get; private set; } 

        private const eFuelType k_FuelType = eFuelType.Octan98; 
        private const float k_MaxTankCapacity = 6.2f;
        private const int k_NumberOfWheels = 2; 
        private const float k_MaxAirPressure = 32f;

        public FuelMotorcycle() : base()
        {
            m_Motorcycle = new Motorcycle();
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
            string motorcycleInfo = m_Motorcycle.GetVehicleInfo();
            string fuelSystemInfo = m_FuelSystem.GetFuelSystemInfo();
            return $"{vehicleInfo}{motorcycleInfo}{fuelSystemInfo}";
        }

        public override Dictionary<string, string> GetQuestions()
        {
            if (!m_ClassQuestions.ContainsKey("LicensesType"))
            {
                m_ClassQuestions["LicensesType"] = "Enter license type (A1, A2, B1, B2):";
            }

            if (!m_ClassQuestions.ContainsKey("EngineVolume"))
            {
                m_ClassQuestions["EngineVolume"] = "Enter engine volume (positive integer):";
            }

            if (!m_ClassQuestions.ContainsKey("FuelLevel"))
            {
                m_ClassQuestions["FuelLevel"] = "Enter initial fuel level:";
            }

            return m_ClassQuestions;
        }

        public override void SetProperties(Dictionary<string, string> i_Properties)
        {
            if (i_Properties.TryGetValue("LicenseType", out string licenseTypeStr))
            {
                if (Enum.TryParse(licenseTypeStr, true, out eMotorcycleLicenseType licenseType))
                {
                    m_Motorcycle.m_LicenseType = licenseType;
                }
                else
                {
                    throw new FormatException($"Invalid license type format: {licenseTypeStr}. Expected values are A1, A2, B1, B2.");
                }
            }

            if (i_Properties.TryGetValue("EngineVolume", out string engineVolumeStr))
            {
                if (int.TryParse(engineVolumeStr, out int engineVolume))
                {
                    if (engineVolume >= 0)
                    {
                        m_Motorcycle.m_MotorcycleEngineVolume = engineVolume;
                    }
                }
                else
                {
                    throw new FormatException($"Invalid engine volume format: {engineVolumeStr}. Please enter a valid integer.");
                }
            }

            if (i_Properties.TryGetValue("FuelLevel", out string fuelLevelStr))
            {
                if (float.TryParse(fuelLevelStr, out float fuelLevel))
                {

                    if (fuelLevel >= 0 && fuelLevel <= k_MaxTankCapacity)
                    {
                        m_FuelSystem.m_CurrentFuelLevel = (fuelLevel);
                        m_EnergyPercentage = (m_FuelSystem.m_CurrentFuelLevel / m_FuelSystem.m_FuelCapacity) * 100;
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