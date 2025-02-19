using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    public class UI
    {
        public void UserMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Garage Management System!");
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. Add a new vehicle to the garage");
                Console.WriteLine("2. View all vehicles in the garage");
                Console.WriteLine("3. Change the status of a vehicle in the garage");
                Console.WriteLine("4. Inflate tires of a vehicle to maximum");
                Console.WriteLine("5. Refuel a fuel-powered vehicle");
                Console.WriteLine("6. Charge an electric vehicle");
                Console.WriteLine("7. Display full details of a vehicle");
                Console.WriteLine("8. Exit");
                Console.Write("Your choice: ");
                string userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        AddVehicle();
                        break;
                    case "2":
                        if (GarageManagment.r_Vehicles.Count > 0)
                        {
                            ShowLicensePlates();
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. No vehicles in the garage.");
                        }

                        break;
                    case "3":
                        if (GarageManagment.r_Vehicles.Count > 0)
                        {
                            ChangeVehicleStatus();
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. No vehicles in the garage.");
                        }

                        break;
                    case "4":
                        if (GarageManagment.r_Vehicles.Count > 0)
                        {
                            InflateAllWheelsToMax();
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. No vehicles in the garage.");
                        }

                        break;
                    case "5":
                        if (GarageManagment.r_Vehicles.Count > 0)
                        {
                            RefuelVehicle();
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. No vehicles in the garage.");
                        }

                        break;
                    case "6":
                        if (GarageManagment.r_Vehicles.Count > 0)
                        {
                            ChargeVehicle();
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. No vehicles in the garage.");
                        }

                        break;
                    case "7":
                        if (GarageManagment.r_Vehicles.Count > 0)
                        {
                            PrintVehicleDetails();
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. No vehicles in the garage.");
                        }

                        break;
                    case "8":
                        {
                            Console.WriteLine("Thank you for using the Garage Management System! Goodbye!");
                        }

                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }

                Console.WriteLine($"{Environment.NewLine}Press any key to return to the menu...");
                Console.ReadKey();
            }
        }

        public void InflateAllWheelsToMax()
        {
            bool isValid = false;
            Vehicle vehicle = null;
            string licenseNumber = null;
            while (!isValid)
            {
                try
                {
                    licenseNumber = EnterLicenseNumber();
                    vehicle = GarageManagment.GetVehicleByLicense(licenseNumber);
                    GarageManagment.InflateAllWheelsToMax(licenseNumber);
                    Console.WriteLine("All wheels have been inflated to the maximum pressure.");
                    isValid = true;
                }

                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please try again with a valid license number.");
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    Console.WriteLine("Please try again.");
                }
            }
        }

        public string EnterPhoneNumber()
        {
            string phoneNumber;
            while (true)
            {
                Console.Write("Please enter your phone number: ");
                phoneNumber = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(phoneNumber) ||
                    !phoneNumber.All(char.IsDigit) ||
                    phoneNumber.Length < 9 ||
                    phoneNumber.Length > 10 ||
                    !phoneNumber.StartsWith("0"))
                {
                    Console.WriteLine("Invalid phone number. It must be 9 or 10 digits, start with '0', and contain only numbers.");
                }
                else
                {
                    break;
                }
            }
            return phoneNumber;
        }

        public void RefuelVehicle()
        {
            bool isValid = false;

            while (!isValid)
            {
                try
                {
                    string licenseNumber = EnterLicenseNumber();
                    Vehicle vehicle = GarageManagment.GetVehicleByLicense(licenseNumber);
                    Console.Write("Enter fuel type: ");
                    string fuelType = Console.ReadLine();
                    Console.Write("Enter amount of fuel (in liters): ");
                    float amountInLiters;
                    while (!float.TryParse(Console.ReadLine(), out amountInLiters) || amountInLiters <= 0)
                    {
                        Console.WriteLine("Invalid input. Please enter a positive number for the amount of fuel.");
                        Console.Write("Enter amount of fuel (in liters): ");
                    }

                    vehicle.RefuelOrRecharge(amountInLiters, fuelType);
                    Console.WriteLine($"Vehicle with license number {licenseNumber} has been successfully refueled/recharged with {amountInLiters} liters of {fuelType}.");
                    isValid = true;
                }

                catch (FormatException ex)
                {
                    Console.WriteLine($"Format error: {ex.Message}");
                }

                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Argument error: {ex.Message}");
                    return;
                }

                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Value out of range: {ex.Message}");
                }

                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (!isValid)
                {
                    Console.WriteLine("Please try again with valid input.");
                }
            }
        }

        public void ChargeVehicle()
        {
            bool isValid = false;

            while (!isValid)
            {
                try
                {
                    int minutesRequiredForCharging;
                    float hoursRequiredForCharging;
                    string licenseNumber = EnterLicenseNumber();
                    Vehicle vehicle = GarageManagment.GetVehicleByLicense(licenseNumber);

                    Console.Write("Enter amount of charge (in minutes): ");
                    while (!int.TryParse(Console.ReadLine(), out minutesRequiredForCharging) || minutesRequiredForCharging <= 0)
                    {
                        Console.WriteLine("Invalid input. Please enter a positive number for the amount of charge.");
                        Console.Write("Enter amount of charge (in minutes): ");
                    }

                    hoursRequiredForCharging = minutesRequiredForCharging / 60f;
                    vehicle.RefuelOrRecharge(hoursRequiredForCharging);
                    Console.WriteLine($"Vehicle with license number {licenseNumber} has been successfully recharged with {hoursRequiredForCharging} hours of battery.");
                    isValid = true;
                }

                catch (FormatException ex)
                {
                    Console.WriteLine($"Format error: {ex.Message}");
                }

                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Value out of range: {ex.Message}");
                }

                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Argument error: {ex.Message}");

                    return;
                }

                if (!isValid)
                {
                    Console.WriteLine("Please try again with valid input.");
                }
            }
        }

        public eVehicleStatus EnterStatus()
        {
            string userInputStatus;
            eVehicleStatus status;

            Console.WriteLine("Please enter the vehicle status (InRepair, Repaired, Paid):");
            userInputStatus = Console.ReadLine();
            while (!Enum.TryParse(userInputStatus, true, out status) || !Enum.IsDefined(typeof(eVehicleStatus), status))
            {
                Console.WriteLine("Invalid status. Please enter a valid status (InRepair, Repaired, Paid):");
                userInputStatus = Console.ReadLine();
            }

            return status;
        }

        public void PrintVehicleDetails()
        {
            bool isValid = false;
            while (!isValid)
            {
                try
                {
                    Console.Write("Enter the vehicle's license plate: ");
                    string licensePlate = EnterLicenseNumber();
                    string vehicleDetails = GarageManagment.GetVehicleDetails(licensePlate);
                    Console.WriteLine(vehicleDetails);
                    isValid = true;
                }

                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please check the license plate and try again.");
                }

                if (!isValid)
                {
                    Console.WriteLine("Please try again with valid input.");
                }
            }
        }
     
        public void ShowLicensePlates()
        {
            eVehicleStatus userInputstatus;
            StringBuilder vehiclesInGarage;
            string userResponse;
            Console.WriteLine("Would you like to filter by status? (Yes/No)");
            userResponse = Console.ReadLine().Trim().ToLower();
            if (userResponse == "yes")
            {
                try
                {
                    userInputstatus = EnterStatus();  
                    vehiclesInGarage = GarageManagment.GetLicensePlatesByStatus(userInputstatus); 
                    Console.WriteLine($"Vehicles with status '{userInputstatus}':");
                    Console.WriteLine(vehiclesInGarage);
                }

                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            else if (userResponse == "no")
            {
                try
                {
                    vehiclesInGarage = GarageManagment.GetAllLicensePlates();
                    Console.WriteLine("All vehicles in the garage:");
                    Console.WriteLine(vehiclesInGarage);
                }

                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        public string EnterLicenseNumber()
        {
            string licenseNumber;

            while (true)
            {
                Console.WriteLine("Please enter the license number of the vehicle:");
                licenseNumber = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(licenseNumber) ||
                    !licenseNumber.All(char.IsDigit) ||
                    (licenseNumber.Length != 7 && licenseNumber.Length != 8))
                {
                    Console.WriteLine("Invalid license number. It must be 7 or 8 digits and contain only numbers. Please try again.");
                }
                else
                {
                    break;
                }
            }

            return licenseNumber;
        }

        public string EnterWheelsManafacturer()
        {
            string manufacturer;

            while (true)
            {
                Console.Write("Enter wheels manufacturer: ");
                manufacturer = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(manufacturer))
                {
                    Console.WriteLine("Manufacturer cannot be empty. Please enter a valid manufacturer.");
                    continue;
                }

                if (manufacturer.All(char.IsLetter))
                {
                    return manufacturer;
                }
                else
                {
                    Console.WriteLine("Manufacturer must contain only letters. Please try again.");
                }
            }
        }

        public void UserWheelsInput(Vehicle i_Vehicle)
        {
            bool isValid = false;

            while (!isValid)
            {
                try
                {
                    Console.WriteLine("Please choose an option:");
                    Console.WriteLine("1. Enter the same air pressure and manufacturer for all wheels");
                    Console.WriteLine("2. Enter air pressure and manufacturer for each wheel individually");
                    string userChoice = Console.ReadLine();

                    switch (userChoice)
                    {
                        case "1":
                            Console.Write("Enter air pressure for all wheels: ");
                            if (float.TryParse(Console.ReadLine(), out float airPressure))
                            {
                                Console.Write("Enter tire manufacturer: ");
                                string manufacturer = EnterWheelsManafacturer();
                                try
                                {
                                    GarageManagment.SetAllWheels(i_Vehicle, airPressure, manufacturer);
                                    Console.WriteLine("Data successfully updated for all wheels.");
                                    isValid = true;
                                }

                                catch (ValueOutOfRangeException ex)
                                {
                                    Console.WriteLine($"Error: {ex.Message}");
                                    Console.WriteLine("Please enter valid air pressure between 0 and max value.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input for air pressure. Please enter a valid number.");
                            }
                            break;

                        case "2":
                            for (int i = 0; i < i_Vehicle.m_Wheels.Count; i++)
                            {
                                bool isWheelValid = false;

                                while (!isWheelValid)
                                {
                                    Console.WriteLine($"Wheel {i + 1}:");
                                    Console.Write("Enter air pressure: ");
                                    if (float.TryParse(Console.ReadLine(), out float individualAirPressure))
                                    {
                                        Console.Write("Enter tire manufacturer: ");
                                        string manufacturer = EnterWheelsManafacturer();
                                        try
                                        {
                                            GarageManagment.SetWheel(i_Vehicle, i, individualAirPressure, manufacturer);
                                            Console.WriteLine($"Data successfully updated for wheel {i + 1}.");
                                            isWheelValid = true; 
                                        }

                                        catch (ValueOutOfRangeException ex)
                                        {
                                            Console.WriteLine($"Error: {ex.Message}");
                                            Console.WriteLine("Please enter valid air pressure for the wheel.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input for air pressure. Please enter a valid number.");
                                    }
                                }
                            }

                            isValid = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please select option 1 or 2.");
                            break;
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    Console.WriteLine("Please try again.");
                }

                if (!isValid)
                {
                    Console.WriteLine("Please try again with valid input.");
                }
            }
        }

        public void ChangeVehicleStatus()
        {
            string licenseNumber = string.Empty;
            eVehicleStatus newStatus;
            bool validInput = false;

            while (true)
            {
                licenseNumber = EnterLicenseNumber();
                if (!GarageManagment.CheckIfVehicleInGarage(licenseNumber))
                {
                    Console.WriteLine($"Vehicle with license plate {licenseNumber} does not exist in the garage. Please enter a valid license plate.");
                }
                else
                {
                    break;
                }
            }

            newStatus = EnterStatus();
            while (!validInput)
            {
                try
                {
                    GarageManagment.ChangeVehicleStatus(licenseNumber, newStatus);
                    Console.WriteLine($"The status of vehicle with license plate {licenseNumber} was successfully changed to {newStatus}.");
                    validInput = true; 
                }

                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    newStatus = EnterStatus(); 
                }
            }
        }

        public void AddVehicle()
        {
            bool isValid = false;

            while (!isValid)
            {
                try
                {
                    string licenseNumber = EnterLicenseNumber();

                    if (GarageManagment.CheckIfVehicleInGarage(licenseNumber))
                    {
                        Console.WriteLine($"The vehicle with license number {licenseNumber} is already in the garage. Status updated to 'In Repair'.");
                    }
                    else
                    {
                        string inputTypeOfVehicle;
                        string ownerName;
                        string ownerPhone;
                        string modelName;

                        Console.Write("Enter the type of vehicle: ");
                        inputTypeOfVehicle = Console.ReadLine();
                        Console.Write("Enter the owner's name: ");
                        ownerName = Console.ReadLine();
                        ownerPhone = EnterPhoneNumber();
                        Console.Write("Enter the model name: ");
                        modelName = Console.ReadLine();
                        Vehicle vehicle = Factory.CreateVehicle(inputTypeOfVehicle);
                        vehicle.m_ModelName = (ownerName);
                        vehicle.m_LicenseNumber = licenseNumber;
                        UserWheelsInput(vehicle);
                        Dictionary<string, string> questions = vehicle.GetQuestions();
                        Dictionary<string, string> answers = new Dictionary<string, string>();
                        foreach (var question in questions)
                        {
                            Console.WriteLine(question.Value);
                            string answer = Console.ReadLine();
                            answers[question.Key] = answer;
                        }

                        vehicle.SetProperties(answers);
                        VehicleRecord vehicleRecord = new VehicleRecord(vehicle, eVehicleStatus.InRepair, ownerName, ownerPhone);
                        GarageManagment.r_Vehicles.Add(vehicle.m_LicenseNumber, vehicleRecord);
                    }

                    isValid = true;
                }

                catch (FormatException ex)
                {
                    Console.WriteLine($"Format error: {ex.Message}");
                }

                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Value out of range: {ex.Message}");
                }

                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Argument error: {ex.Message}");
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }

                if (!isValid)
                {
                    Console.WriteLine("Please try again.");
                }
            }
        }
    }
}