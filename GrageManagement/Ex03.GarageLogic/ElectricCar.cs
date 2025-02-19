using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Vehicle
    {
        public Car m_Car { get; private set; }
        public ElectricalSystem m_ElectricalSystem { get; private set; }

        private const int k_NumberOfWheels = 5;        
        private const float k_MaxAirPressure = 34f;     
        private const float k_MaxBatteryCapacity = 5.4f;

        public ElectricCar() : base()
        {
            m_Car = new Car();
            m_ElectricalSystem = new ElectricalSystem();
            m_Wheels = Wheel.CreateWheels(k_NumberOfWheels, k_MaxAirPressure);
            m_ElectricalSystem.m_BatteryCapacity = k_MaxBatteryCapacity;
            m_TypeOfSystem = eTypeOfSystem.ElectricVehicle;
        }

        public override string GetVehicleInfo()
        {
            string vehicleInfo = $"Model Name: {m_ModelName}{Environment.NewLine}" +
                                 $"License Number: {m_LicenseNumber}{Environment.NewLine}" +
                                 $"Energy Percentage: {m_EnergyPercentage}%{Environment.NewLine}";
            string carInfo = m_Car.GetVehicleInfo();
            string electricalSystemInfo = m_ElectricalSystem.GetElectricalSystemInfo();

            return $"{vehicleInfo}{Environment.NewLine}{carInfo}{Environment.NewLine}{electricalSystemInfo}";
        }

        public override Dictionary<string, string> GetQuestions()
        {
            if (!m_ClassQuestions.ContainsKey("CarColor"))
            {
                m_ClassQuestions["CarColor"] = "Enter car color (Black, White, Gray, Blue):";
            }

            if (!m_ClassQuestions.ContainsKey("NumberOfDoors"))
            {
                m_ClassQuestions["NumberOfDoors"] = "Enter number of doors (2, 3, 4, 5):";
            }

            if (!m_ClassQuestions.ContainsKey("BatteryLevel"))
            {
                m_ClassQuestions["BatteryLevel"] = "Enter current battery level (in kWh):";
            }

            return m_ClassQuestions;
        }
        public override void SetProperties(Dictionary<string, string> i_Properties)
        {
            if (i_Properties.TryGetValue("CarColor", out string carColorStr))
            {
                if (Enum.TryParse(carColorStr, out eCarColor carColor))
                {
                    m_Car.m_Color = (carColor);
                }
                else
                {
                    throw new ArgumentException($"Invalid car color: {carColorStr}. Please enter a valid color.");
                }
            }

            if (i_Properties.TryGetValue("NumberOfDoors", out string numberOfDoorsStr))
            {
                if (int.TryParse(numberOfDoorsStr, out int numberOfDoors))
                {
                    if (numberOfDoors >= 2 && numberOfDoors <= 5)
                    {
                        m_Car.m_NumberOfDoors = ((eCarDoorCount)numberOfDoors);
                    }
                    else
                    {
                        throw new ValueOutOfRangeException(2, 5);
                    }
                }
                else
                {
                    throw new FormatException($"Invalid format for number of doors: {numberOfDoorsStr}. Please enter a valid integer.");
                }
            }

            if (i_Properties.TryGetValue("BatteryLevel", out string batteryLevelStr))
            {
                if (float.TryParse(batteryLevelStr, out float batteryLevel))
                {
                    if (batteryLevel >= 0 && batteryLevel <= k_MaxBatteryCapacity)
                    {
                        m_ElectricalSystem.m_CurrentBatteryLevel = batteryLevel;
                        m_EnergyPercentage = (m_ElectricalSystem.m_CurrentBatteryLevel / m_ElectricalSystem.m_BatteryCapacity)* 100;
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