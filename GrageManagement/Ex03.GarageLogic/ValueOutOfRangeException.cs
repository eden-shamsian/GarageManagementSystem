using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        public float m_MinValue;
        public float m_MaxValue;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
            : base($"Value is out of range. Allowed range: {i_MinValue} - {i_MaxValue}")
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, string i_Message)
     : base(i_Message)
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }
    }
}
