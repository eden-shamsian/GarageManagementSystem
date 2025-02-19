using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public string m_ModelName { get; set; }

        public string m_LicenseNumber { get; set; } 

        public float m_EnergyPercentage { get; protected set; } 

        public List<Wheel> m_Wheels { get; protected set; }

        public Dictionary<string,string> m_ClassQuestions { get; protected set; } = new Dictionary<string, string>();

        public eTypeOfSystem m_TypeOfSystem { get; protected set; }

        protected Vehicle(string i_ModelName, string i_LicenseNumber, float i_EnergyPercentage, List<Wheel> i_Wheels)
        {
            m_ModelName = i_ModelName;
            m_LicenseNumber = i_LicenseNumber;
            m_EnergyPercentage = i_EnergyPercentage;
            m_Wheels = i_Wheels;
        }
        protected Vehicle() 
        {
            m_ModelName = string.Empty;
            m_LicenseNumber = string.Empty;
            m_EnergyPercentage = 0;
            m_Wheels = null;
        }

        public abstract string GetVehicleInfo();

        public abstract Dictionary <string, string> GetQuestions();

        public abstract void SetProperties(Dictionary<string, string> properties);

        public virtual void RefuelOrRecharge(float amount)
        {
            if (m_TypeOfSystem == eTypeOfSystem.FuelVehicle)
            {
                throw new ArgumentException("Fuel Vehicle cannot be recharged. Please refuel instead.");
            }
        }

        public virtual void RefuelOrRecharge(float amount, string fuelType)
        {
            if (m_TypeOfSystem == eTypeOfSystem.ElectricVehicle)
            {
                throw new ArgumentException("Electric vehicles cannot be refueled. Please recharge instead.");
            }
        }
    }
}
