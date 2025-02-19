using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelSystem
    {
        public eFuelType m_FuelType { get; set; }
        public float m_FuelCapacity { get; set; }
        public float m_CurrentFuelLevel { get; set; }

        public FuelSystem(eFuelType i_FuelType, float i_FuelCapacity, float i_InitialFuelLevel)
        {
            if (i_InitialFuelLevel > i_FuelCapacity || i_InitialFuelLevel < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(i_InitialFuelLevel),
                    "Initial fuel level must be between 0 and fuel capacity.");
            }
            m_FuelType = i_FuelType;
            m_FuelCapacity = i_FuelCapacity;
            m_CurrentFuelLevel = i_InitialFuelLevel;
        }
        public FuelSystem()
        {
            m_FuelCapacity = 0; 
            m_CurrentFuelLevel = 0;
        }

        public void Refuel(float i_AmountToAdd, eFuelType i_FuelType)
        {
            if (i_FuelType != m_FuelType)
            {
                throw new ArgumentException("The fuel type does not match the vehicle's fuel type.");
            }

            if (i_AmountToAdd <= 0 || m_CurrentFuelLevel + i_AmountToAdd > m_FuelCapacity)
            {
                float minValue = 0;
                float maxValue = m_FuelCapacity - m_CurrentFuelLevel;
                throw new ValueOutOfRangeException(minValue, maxValue);
            }

            m_CurrentFuelLevel += i_AmountToAdd;
        }
        public string GetFuelSystemInfo()
        {

            return $"Fuel Type: {m_FuelType}{Environment.NewLine}Current Fuel Level: {m_CurrentFuelLevel} liters";
        }
    }
}
