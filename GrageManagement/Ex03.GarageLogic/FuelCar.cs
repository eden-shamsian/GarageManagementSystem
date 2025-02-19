using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelCar : Vehicle
    {
        public Car m_Car { get; private set; }
        public FuelSystem m_FuelSystem { get; private set; }

        private const eFuelType k_FuelType = eFuelType.Octan95;    
        private const float k_MaxTankCapacity = 52f;  
        private const int k_NumberOfWheels = 5;       
        private const float k_MaxAirPressure = 34f;    

        public FuelCar() : base()
        {
            m_Car = new Car();
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
            string carInfo = m_Car.GetVehicleInfo();
            string fuelSystemInfo = m_FuelSystem.GetFuelSystemInfo();

            return $"{vehicleInfo}{carInfo}{Environment.NewLine}{fuelSystemInfo}";
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

            if (!m_ClassQuestions.ContainsKey("FuelLevel"))
            {
                m_ClassQuestions["FuelLevel"] = "Enter initial fuel level:";
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

            if (i_Properties.TryGetValue("FuelLevel", out string fuelLevelStr))
            {
                if (float.TryParse(fuelLevelStr, out float fuelLevel))
                {

                    if (fuelLevel >= 0 && fuelLevel <= k_MaxTankCapacity)
                    {
                        m_FuelSystem.m_CurrentFuelLevel = fuelLevel;
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

            eFuelType fuelType;
            bool isValidFuelType;

            if (m_TypeOfSystem == eTypeOfSystem.ElectricVehicle)
            {
                throw new Exception("Electric vehicles cannot be refueled. Please recharge instead.");
            }

            isValidFuelType = Enum.TryParse(i_FuelTypeString, true, out fuelType);
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