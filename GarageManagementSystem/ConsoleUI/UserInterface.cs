using System;
using System.Collections.Generic;
using System.Text;
using GarageLogic;

namespace ConsoleUI
{
    public class UserInterface
    {
        public UserInterface()
        {
            runGarageProgram();
        }

        private OurGarage m_OurGarage = new OurGarage();

        private eMenu m_eUserMenuChoice;

        public enum eMenu
        {
            NewVehicle = 1,
            ShowListOfVehicles = 2,
            ChangeVeihcleSituation = 3,
            MaxAirWheelsPressure = 4,
            RefuelFuelEnginVehicle = 5,
            RechargeElectricEngineVehicle = 6,
            ShowAllVeihcleDetails = 7,
            ExitProgram = 8,
        }

        private void runGarageProgram()
        {
            StringBuilder garageMenu = new StringBuilder();
            buildMenu(garageMenu);
            Console.WriteLine("Welcome to our garage!");
            bool CheckIfLegitValue = false;
            do
            {
                Console.WriteLine(garageMenu);
                try
                {
                    functionOfGarage(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (!CheckIfLegitValue);
        }

        private void functionOfGarage(string i_UserChoice)
        {
            m_eUserMenuChoice = (eMenu)Enum.Parse(typeof(eMenu), i_UserChoice);

            switch (m_eUserMenuChoice)
            {
                case eMenu.NewVehicle:
                    {
                        addNewVehicle();
                        break;
                    }

                case eMenu.ShowListOfVehicles:
                    {
                        listOfVehicles();
                        break;
                    }

                case eMenu.ChangeVeihcleSituation:
                    {
                        changeVehicleSituation();
                        break;
                    }

                case eMenu.MaxAirWheelsPressure:
                    {
                        changeAirPressureToMax();
                        break;
                    }

                case eMenu.RefuelFuelEnginVehicle:
                    {
                        refuleVhicle();
                        break;
                    }

                case eMenu.RechargeElectricEngineVehicle:
                    {
                        chargeElectricVehicle();
                        break;
                    }

                case eMenu.ShowAllVeihcleDetails:
                    {
                        printOwnersVehicleDetails();
                        break;
                    }

                case eMenu.ExitProgram:
                    {
                        Console.WriteLine("Good bye!");
                        // $G$ NTT-999 (-1) You should avoid brutally terminating the process. (The program can end in the regular flow). 
                        Environment.Exit(0);
                        break;
                    }

                default:
                    {
                        throw new ValueOutOfRangeException(1, Enum.GetNames(typeof(eMenu)).Length, "Value out of range");
                    }
            }
        }

        private void buildMenu(StringBuilder i_GarageMenu)
        {
            i_GarageMenu.AppendLine(string.Format(@"
Press 1 to 8:

*All options is by license number*

1 - To enter a new vehicle 
2 - To show license list
3 - To change vehicle situation
4 - To max wheels air pressure
5 - To refuel a fuel vehicle
6 - To recharge an electric vehicle
7 - To show vehicle information
8 - To exit"));
        }

        private void addNewVehicle()
        {
            bool IsInGarage;
            string msgToUser;
            string licenseNum;
            Console.WriteLine("Please enter license number");
            licenseNum = Console.ReadLine();
            m_OurGarage.IsTheVehicleInTheGarageChangeSituation(licenseNum, out IsInGarage, out msgToUser);
            Console.WriteLine(msgToUser);
            if (!IsInGarage)
            {
                int vehicleType;
                string modelType;
                string manufacturerName;
                string ownerName;
                string ownerPhone;
                Console.WriteLine("Please choose vehicle type");
                Console.WriteLine(m_OurGarage.ListOfExistsVehicleTypes());
                vehicleType = getVehicleTypeFromUser();
                Console.WriteLine("Please enter model type");
                modelType = Console.ReadLine();
                Console.WriteLine("Please enter your name");
                ownerName = Console.ReadLine();
                Console.WriteLine("Please enter your phone number");
                ownerPhone = Console.ReadLine();
                Console.WriteLine("Please enter manufacturer name");
                manufacturerName = Console.ReadLine();
                m_OurGarage.AddNewVehicle(vehicleType, licenseNum, modelType, manufacturerName, ownerName, ownerPhone);
                setWheelCurrentAirPressure(licenseNum);
                setCurrentEnergy(licenseNum);
                setCurrentVehicleProperties(licenseNum);
            }
        }

        private int getVehicleTypeFromUser()
        {
            int vehicleType;
            bool CheckIfLegitChoice = false;
            do
            {
                if (int.TryParse(Console.ReadLine(), out vehicleType))
                {
                    try
                    {
                        CheckIfLegitChoice = m_OurGarage.IsVehicleTypeChoiceExists(vehicleType);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
            while (!CheckIfLegitChoice);

            return vehicleType;
        }

        private void setWheelCurrentAirPressure(string i_LicenseNum)
        {
            float maxAirPressure = m_OurGarage.OwnersList[i_LicenseNum].OwenersVehicle.GetWheelsList[0].MaxWheelAirPressure;
            float currentAirPressure;
            bool CheckIfValidInput = false;
            do
            {
                Console.WriteLine("Please enter current wheel air pressure");
                if (float.TryParse(Console.ReadLine(), out currentAirPressure))
                {
                    try
                    {
                        m_OurGarage.OwnersList[i_LicenseNum].OwenersVehicle.GetWheelsList[0].CurrentAirPressure = currentAirPressure;
                        CheckIfValidInput = true;
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Legit value are between {0} to {1}", ex.MinimumValue, ex.MaximumValue);
                    }
                }
            }
            while (!CheckIfValidInput);
            foreach (Wheel perWheel in m_OurGarage.OwnersList[i_LicenseNum].OwenersVehicle.GetWheelsList)
            {
                perWheel.CurrentAirPressure = currentAirPressure;
            }
        }

        private void setCurrentEnergy(string i_LicenseNum)
        {
            float maxEnergy = m_OurGarage.OwnersList[i_LicenseNum].OwenersVehicle.Engine.MaxEnergy;
            float currentAmountOfEnergy;
            bool CheckIfValidInput = false;
            do
            {
                Console.WriteLine("Please enter current amount of enegy");
                if (float.TryParse(Console.ReadLine(), out currentAmountOfEnergy))
                {
                    try
                    {
                        m_OurGarage.OwnersList[i_LicenseNum].OwenersVehicle.Engine.CurrentAmountOfEnergy = currentAmountOfEnergy;
                        CheckIfValidInput = true;
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Legit value are between {0} to {1}", ex.MinimumValue, ex.MaximumValue);
                    }
                }
            }
            while (!CheckIfValidInput);
        }

        private void setCurrentVehicleProperties(string i_LicenseNum)
        {
            List<string> listOfVehicleProperties = m_OurGarage.GetVehicleStringProperties(i_LicenseNum);
            bool CheckIfValidInput;

            for (int i = 0; i < listOfVehicleProperties.Count; i++)
            {
                CheckIfValidInput = false;
                Console.WriteLine(listOfVehicleProperties[i]);
                do
                {
                    try
                    {
                        m_OurGarage.SetVehicleProperties(i + 1, Console.ReadLine(), i_LicenseNum);
                        CheckIfValidInput = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                while (!CheckIfValidInput);
            }
        }

        private void listOfVehicles()
        {
            int sortSituationOrNot;
            int SituationType;
            bool CheckIfValidInput;
            do
            {
                Console.WriteLine(@"
Do you want to present by vehicle situation? 
0 - Yes
1 - No");
                if (int.TryParse(Console.ReadLine(), out sortSituationOrNot))
                {
                    bool isSituationValid = false;
                    if (sortSituationOrNot == 0)
                    {
                        Console.WriteLine(string.Format(
@"Choose car situation: {0}",
m_OurGarage.ListOfExistsSituationTypes()));

                        do
                        {
                            if (int.TryParse(Console.ReadLine(), out SituationType))
                            {
                                try
                                {
                                    Console.WriteLine(string.Format(
@" {0} 
",
                                    m_OurGarage.OwnersLicenseListBySituation(SituationType)));
                                    isSituationValid = true;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input");
                            }
                        }
                        while (!isSituationValid);
                        CheckIfValidInput = true;
                    }
                    else if (sortSituationOrNot == 1)
                    {
                        Console.WriteLine(
@"All License numbers list:
{0}                              
",
                        m_OurGarage.OwnersLicenseList());
                        CheckIfValidInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please entrer between 0 to 1");
                        CheckIfValidInput = false;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input");
                    CheckIfValidInput = false;
                }
            }
            while (!CheckIfValidInput);
        }

        private void changeVehicleSituation()
        {
            bool CheckIfValidNumber = false;
            int vehicleSituationOption;
            Console.WriteLine("Please enter vehicle license number");
            string licenseNum = Console.ReadLine();
            Console.Write("Choose car situation {0} {1}", Environment.NewLine, m_OurGarage.ListOfExistsSituationTypes());
            do
            {
                if (int.TryParse(Console.ReadLine(), out vehicleSituationOption))
                {
                    try
                    {
                        m_OurGarage.ChangeVehicleSituation(vehicleSituationOption, licenseNum);
                        CheckIfValidNumber = true;
                        Console.WriteLine("Vehicle situation has been changed!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid value, Please try again");
                }
            }
            while (!CheckIfValidNumber);
        }

        private void changeAirPressureToMax()
        {
            Console.WriteLine("Please enter vehicle license number");
            string licenseNum = Console.ReadLine();
            try
            {
                m_OurGarage.ChangeWheelsAirPressureToMax(licenseNum);
                Console.WriteLine("Wheels pressure has been maxed!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void refuleVhicle()
        {
            bool CheckIfValidInput = false;
            bool CheckIfValidInputOfEngineType = false;
            float amountOfEnergyToAdd;
            int engineType;
            Console.WriteLine("Please enter vehicle license number");
            string licenseNum = Console.ReadLine();
            do
            {
                Console.WriteLine("Please enter amount of fuel to add");
                if (float.TryParse(Console.ReadLine(), out amountOfEnergyToAdd))
                {
                    Console.WriteLine("Please enter engine type");
                    Console.WriteLine(m_OurGarage.ListOfExistsEngineTypes());
                    do
                    {
                        if (int.TryParse(Console.ReadLine(), out engineType))
                        {
                            if (m_OurGarage.IsVehicleEngineTypeExists(engineType))
                            {
                                CheckIfValidInputOfEngineType = true;
                            }
                            else
                            {
                                Console.WriteLine("Not valid input");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Not valid input");
                        }
                    }
                    while (!CheckIfValidInputOfEngineType);
                    try
                    {
                        m_OurGarage.RefuelVehicl(amountOfEnergyToAdd, engineType, licenseNum);
                        CheckIfValidInput = true;
                        Console.WriteLine("Vehicle been refuled!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid value, Please try again");
                }
            }
            while (!CheckIfValidInput);
        }

        private void chargeElectricVehicle()
        {
            bool CheckIfValidInput = false;
            float amountOfEnergyToAdd;
            Console.WriteLine("Please enter vehicle license number");
            string licenseNum = Console.ReadLine();
            do
            {
                Console.WriteLine("Please enter amount of energy in minuts to add");
                if (float.TryParse(Console.ReadLine(), out amountOfEnergyToAdd))
                {
                    try
                    {
                        m_OurGarage.RechargeVehicl(amountOfEnergyToAdd, licenseNum);
                        CheckIfValidInput = true;
                        Console.WriteLine("Vehicle been charged!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid value, Please try again");
                }
            }
            while (!CheckIfValidInput);
        }

        private void printOwnersVehicleDetails()
        {
            bool CheckIfOwnerExists = false;
            Console.WriteLine("Please enter vehicles license number");
            do
            {
                try
                {
                    string licenseNum = Console.ReadLine();
                    Console.WriteLine(m_OurGarage.ClientAndVehicleProperties(licenseNum));
                    CheckIfOwnerExists = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (!CheckIfOwnerExists);
        }
    }
}
