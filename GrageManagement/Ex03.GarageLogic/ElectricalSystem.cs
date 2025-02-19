using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricalSystem
    {
        public float m_BatteryCapacity { get; set; }

        public float m_CurrentBatteryLevel { get; set; }

        public ElectricalSystem(float i_BatteryCapacity, float i_InitialBatteryLevel)
        {
            if (i_InitialBatteryLevel > i_BatteryCapacity || i_InitialBatteryLevel < 0)
            {
                throw new ValueOutOfRangeException(0, i_BatteryCapacity, "Initial battery level must be between 0 and battery capacity.");
            }

            m_BatteryCapacity = i_BatteryCapacity;
            m_CurrentBatteryLevel = i_InitialBatteryLevel;
        }

        public ElectricalSystem()
        {
            m_BatteryCapacity = 0;
            m_CurrentBatteryLevel = 0;
        }

        public string GetElectricalSystemInfo()
        {

            return $"Current Battery Level: {m_CurrentBatteryLevel} kWh";
        }

        public void Recharge(float i_HoursToCharge)
        {
            if (i_HoursToCharge <= 0 || m_CurrentBatteryLevel + i_HoursToCharge > m_BatteryCapacity)
            {
                float minValue = 0;
                float maxValue = m_BatteryCapacity - m_CurrentBatteryLevel;
                throw new ValueOutOfRangeException(minValue, maxValue);
            }

            m_CurrentBatteryLevel += i_HoursToCharge;
        }
    }
}