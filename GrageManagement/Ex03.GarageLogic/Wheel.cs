using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        public string m_Manufacturer { get; set; }

        public float m_CurrentAirPressure { get; set; }

        public float m_MaxAirPressure { get; set; }

        public Wheel(string i_Manufacturer, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            m_Manufacturer = i_Manufacturer;
            m_MaxAirPressure = i_MaxAirPressure;

            if (i_CurrentAirPressure > i_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, i_MaxAirPressure);
            }

            m_CurrentAirPressure = i_CurrentAirPressure;
        }

        public Wheel(string i_Manufacturer, float i_CurrentAirPressure)
        {
            m_Manufacturer = i_Manufacturer;

            if (i_CurrentAirPressure > m_MaxAirPressure)
            {
                throw new ValueOutOfRangeException(0, m_MaxAirPressure);
            }
            m_CurrentAirPressure = i_CurrentAirPressure;
        }

        public Wheel()
        {
            m_Manufacturer = string.Empty;
            m_CurrentAirPressure = 0;
            m_MaxAirPressure = 0;
        }

        public Wheel(float i_MaxAirPressure)
        {
            m_CurrentAirPressure = 0;
            m_MaxAirPressure = i_MaxAirPressure;
            m_Manufacturer = "";
        }

        public void Inflate(float i_Amount)
        {
            if (i_Amount <= 0 || m_CurrentAirPressure + i_Amount > m_MaxAirPressure)
            {
                float minAllowed = 0;
                float maxAllowed = m_MaxAirPressure - m_CurrentAirPressure;
                throw new ValueOutOfRangeException(minAllowed, maxAllowed);
            }
            m_CurrentAirPressure += i_Amount;
        }

        public static List<Wheel> CreateWheels(int i_NumOfWheels, float i_MaxAirPressure)
        {
            List<Wheel> listOfWheels = new List<Wheel>();
            for (int i = 0; i < i_NumOfWheels; i++)
            {
                listOfWheels.Add(new Wheel(i_MaxAirPressure));
            }
            return listOfWheels;
        }

        public static List<Wheel> CreateWheels(int i_NumOfWheels)
        {
            List<Wheel> listOfWheels = new List<Wheel>();

            for (int i = 0; i < i_NumOfWheels; i++)
            {
                listOfWheels.Add(new Wheel());
            }

            return listOfWheels;
        }
    }
}
