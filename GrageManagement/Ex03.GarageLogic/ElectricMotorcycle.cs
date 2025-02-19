using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Vehicle
    {
       public Motorcycle m_Motorcycle { get; private set; }
       public ElectricalSystem m_ElectricalSystem { get; private set; }

        private const int k_NumberOfWheels = 2; 
        private const float k_MaxAirPressure = 32.0f; 
        private const float k_MaxBatteryCapacity = 2.9f;

        public ElectricMotorcycle() : base()
        {
            m_Motorcycle = new Motorcycle();
            m_ElectricalSystem = new ElectricalSystem();
            m_Wheels = Wheel.CreateWheels(k_NumberOfWheels, k_MaxAirPressure);
            m_ElectricalSystem.m_BatteryCapacity = k_MaxBatteryCapacity;
            m_TypeOfSystem = eTypeOfSystem.ElectricVehicle;
        }

        public override string GetVehicleInfo()
        {
            string vehicleInfo = $"Model Name: {m_ModelName}{Environment.NewLine}" +
                                 $"License Number: {m_LicenseNumber}{Environment.NewLine}" +
                                 $"Energy Percentage: {m_EnergyPercentage}%{Environment.NewLine}" +
                                 $"Number of Wheels: {m_Wheels.Count}{Environment.NewLine}";
            string motorcycleInfo = m_Motorcycle.GetVehicleInfo();
            string electricalSystemInfo = m_ElectricalSystem.GetElectricalSystemInfo();

            return $"{vehicleInfo}{motorcycleInfo}{electricalSystemInfo}";
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

            if (!m_ClassQuestions.ContainsKey("BatteryLevel"))
            {
                m_ClassQuestions["BatteryLevel"] = "Enter current battery level (in kWh):";
            }

            return m_ClassQuestions;
        }

        public override void SetProperties(Dictionary<string, string> i_Properties)
        {
            if (i_Properties.TryGetValue("LicensesType", out string licenseTypeStr))
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

            if (i_Properties.TryGetValue("BatteryLevel", out string batteryLevelStr))
            {
                if (float.TryParse(batteryLevelStr, out float batteryLevel))
                {

                    if (batteryLevel >= 0 && batteryLevel <= k_MaxBatteryCapacity)
                    {
                        m_ElectricalSystem.m_CurrentBatteryLevel = batteryLevel;
                        m_EnergyPercentage = (m_ElectricalSystem.m_CurrentBatteryLevel / m_ElectricalSystem.m_BatteryCapacity) * 100;

                    }
                    else
                    {
                        throw new ValueOutOfRangeException(0, k_MaxBatteryCapacity);
                    }
                }
                else
                {
                    throw new FormatException($"Invalid fuel level format: {k_MaxBatteryCapacity}. Please enter a valid number.");
                }
            }
        }

        public override void RefuelOrRecharge(float i_AmountToCharge)
        {
            if (m_TypeOfSystem == eTypeOfSystem.FuelVehicle)
            {
                throw new Exception("Fuel Vehicle cannot be recharged. Please refuel instead.");
            }
            m_ElectricalSystem.Recharge(i_AmountToCharge);
        }
    }
}