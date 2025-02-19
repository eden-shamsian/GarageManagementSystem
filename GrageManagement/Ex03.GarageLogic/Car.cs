using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        public eCarColor m_Color { get; set; }

        public eCarDoorCount m_NumberOfDoors { get; set; }

        public Car(eCarColor i_Color, eCarDoorCount i_NumberOfDoors)
        {
            m_Color = i_Color;
            m_NumberOfDoors = i_NumberOfDoors;
        }

        public Car():base()
        {
            m_NumberOfDoors = 0;
        }

        public override string GetVehicleInfo()
        {
            return $"Color: {m_Color}\nNumber of Doors: {m_NumberOfDoors}";
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

            return m_ClassQuestions;
        }
        public override void SetProperties(Dictionary<string, string> i_Properties)
        {
            if (i_Properties.TryGetValue("CarColor", out string carColorStr))
            {
                if (Enum.TryParse(carColorStr, out eCarColor carColor))
                {
                    m_Color = carColor;
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
                        m_NumberOfDoors = (eCarDoorCount)numberOfDoors; 
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
        }
    }
}