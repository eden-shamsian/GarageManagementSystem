using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Factory
    {
        public static Vehicle CreateVehicle(string i_TypeOfVehicle)
        {
            switch (i_TypeOfVehicle.Trim().ToLower())
            {
                case "fuel motorcycle":
                    return new FuelMotorcycle();
                case "electric motorcycle":
                    return new ElectricMotorcycle();
                case "fuel car":
                    return new FuelCar();
                case "electric car":
                    return new ElectricCar();
                case "truck":
                    return new FuelTruck();
                default:
                    throw new ArgumentException("Invalid vehicle type.");
            }
        }
    }
}