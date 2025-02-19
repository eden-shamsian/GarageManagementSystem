using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public eMotorcycleLicenseType m_LicenseType { get; set; }

        public int m_MotorcycleEngineVolume { get; set; }

        public Motorcycle(eMotorcycleLicenseType i_LicenseType, int i_EngineVolume)
        {
            m_LicenseType = i_LicenseType;
            m_MotorcycleEngineVolume = i_EngineVolume;
        }

        public Motorcycle():base()
        {
            m_MotorcycleEngineVolume = 0;
        }
   
        public override string GetVehicleInfo()
        {

            return $"License Type: {m_LicenseType}{Environment.NewLine}Engine Volume: {m_MotorcycleEngineVolume} cc";
        }

        public override Dictionary<string, string> GetQuestions()
        {
            if (!m_ClassQuestions.ContainsKey("LicenseType"))
            {
                m_ClassQuestions["LicenseType"] = "Enter license type (A1, A2, B1, B2):";
            }

            if (!m_ClassQuestions.ContainsKey("EngineVolume"))
            {
                m_ClassQuestions["EngineVolume"] = "Enter engine volume (positive integer):";
            }

            return m_ClassQuestions;
        }
        public override void SetProperties(Dictionary<string, string> i_Properties)
        {
            if (i_Properties.TryGetValue("LicenseType", out string licenseTypeStr))
            {
                if (Enum.TryParse(licenseTypeStr, true, out eMotorcycleLicenseType licenseType))
                {
                    m_LicenseType = licenseType;
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
                        m_MotorcycleEngineVolume = engineVolume;
                    }
                }
                else
                {
                    throw new FormatException($"Invalid engine volume format: {engineVolumeStr}. Please enter a valid integer.");
                }
            }
        }
    }
}