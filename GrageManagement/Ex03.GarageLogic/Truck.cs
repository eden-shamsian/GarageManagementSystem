using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        public float m_CargoVolume { get; set; }

        public bool m_IsCarryingCoolingMaterials { get; set; }

        public Truck(float i_CargoVolume, bool i_IsCarryingCoolingMaterials)
        {
            if (i_CargoVolume < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(i_CargoVolume), "Cargo volume must be non-negative.");
            }

            m_CargoVolume = i_CargoVolume;
            m_IsCarryingCoolingMaterials = i_IsCarryingCoolingMaterials;
        }

        public Truck() : base()
        {
            m_CargoVolume = 0;
            m_IsCarryingCoolingMaterials = false;
        }

        public override string GetVehicleInfo() 
        { 
            string coolingMaterialsStatus = m_IsCarryingCoolingMaterials ? "Yes" : "No";

            return $"Cargo Volume: {m_CargoVolume} cubic meters{Environment.NewLine}Carrying Cooling Materials: {coolingMaterialsStatus}";
        }

        public override Dictionary<string, string> GetQuestions()
        {
            if (!m_ClassQuestions.ContainsKey("CarryingCoolMaterials"))
            {
                m_ClassQuestions["CarryingCoolMaterials"] = "Is the truck carrying cooling materials? (true/false):";
            }

            if (!m_ClassQuestions.ContainsKey("CargoVolume"))
            {
                m_ClassQuestions["CargoVolume"] = "Enter the cargo volume (in cubic meters):";
            }

            return m_ClassQuestions;
        }

        public override void SetProperties(Dictionary<string, string> i_Properties)
        {
            if (i_Properties.TryGetValue("CarryingCoolMaterials", out string carryingCoolMaterialsStr))
            {
                if (bool.TryParse(carryingCoolMaterialsStr, out bool carryingCoolMaterials))
                {
                    m_IsCarryingCoolingMaterials = carryingCoolMaterials;
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
                    if (cargoVolume >= 0 )
                    {
                        m_CargoVolume = cargoVolume;
                    }
                }
                else
                {
                    throw new FormatException($"Invalid format for CargoVolume: {cargoVolumeStr}. Please enter a valid number.");
                }
            }
        }
    }
}
