using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public class OurGarage
    {
        private readonly Dictionary<string, CarOwnerInformation> r_AllVehiclesInfo = new Dictionary<string, CarOwnerInformation>();

        public OurGarage()
        {
        }

        public Dictionary<string, CarOwnerInformation> OwnersList
        {
            get
            {
                return r_AllVehiclesInfo;
            }
        }

        public StringBuilder ListOfExistsVehicleTypes()
        {
            return VehicleTypesAndBuilder.GetVehicleTypes();
        }

        public StringBuilder ListOfExistsSituationTypes()
        {
            StringBuilder situationType = new StringBuilder(Environment.NewLine);
            int situationTypeAmount = Enum.GetValues(typeof(CarOwnerInformation.eCurrentVehicleSituation)).Length;
            for (int i = 0; i < situationTypeAmount; i++)
            {
                situationType.AppendLine(string.Format("{0}: {1}", i + 1, Enum.GetName(typeof(CarOwnerInformation.eCurrentVehicleSituation), i + 1)));
            }

            return situationType;
        }

        public StringBuilder ListOfExistsEngineTypes()
        {
            StringBuilder engineTypes = new StringBuilder();
            int vehilecTypeAmount = Enum.GetValues(typeof(eEngineType)).Length;
            for (int i = 0; i < vehilecTypeAmount; i++)
            {
                engineTypes.AppendLine(string.Format("{0}: {1}", i + 1, Enum.GetName(typeof(eEngineType), i + 1)));
            }

            return engineTypes;
        }

        public StringBuilder OwnersLicenseListBySituation(int i_VehicleSituation)
        {
            CarOwnerInformation.eCurrentVehicleSituation vehicleSituation = (CarOwnerInformation.eCurrentVehicleSituation)i_VehicleSituation;
            StringBuilder ownersLicenseListBySituation = new StringBuilder();
            int situationTypeAmount = Enum.GetValues(typeof(CarOwnerInformation.eCurrentVehicleSituation)).Length;
            int i = 1;
            if ((i_VehicleSituation > 0) && (i_VehicleSituation <= situationTypeAmount))
            {
                if (r_AllVehiclesInfo.Count > 0)
                {
                    foreach (CarOwnerInformation owner in r_AllVehiclesInfo.Values)
                    {
                        if (owner.VehicleSituation == vehicleSituation)
                        {
                            ownersLicenseListBySituation.AppendLine(string.Format("{0}: {1}", i, owner.OwenersVehicle.LicenseNumber));
                            i++;
                        }
                    }
                }
                else
                {
                    ownersLicenseListBySituation.AppendLine(string.Format("There is no cars in our garage by {0} situation", Enum.GetName(typeof(CarOwnerInformation.eCurrentVehicleSituation), i_VehicleSituation)));
                }
            }
            else
            {
                throw new ValueOutOfRangeException(1, situationTypeAmount, "Value out of range");
            }

            return ownersLicenseListBySituation;
        }

        public StringBuilder OwnersLicenseList()
        {
            StringBuilder ownersLicenseList = new StringBuilder();
            int i = 1;
            if (r_AllVehiclesInfo.Count > 0)
            {
                foreach (CarOwnerInformation owner in r_AllVehiclesInfo.Values)
                {
                    ownersLicenseList.AppendLine(string.Format("{0}: {1}", i, owner.OwenersVehicle.LicenseNumber));
                    i++;
                }
            }
            else
            {
                ownersLicenseList.AppendLine("There is no cars in our garage");
            }

            return ownersLicenseList;
        }

        public Dictionary<string, CarOwnerInformation> AllOwnersList
        {
            get
            {
                return r_AllVehiclesInfo;
            }
        }

        public void IsTheVehicleInTheGarageChangeSituation(string i_LicenseNumber, out bool o_IsInGarage, out string o_MessegeToUI)
        {
            if (r_AllVehiclesInfo != null && r_AllVehiclesInfo.ContainsKey(i_LicenseNumber))
            {
                r_AllVehiclesInfo[i_LicenseNumber].VehicleSituation = CarOwnerInformation.eCurrentVehicleSituation.InRepair;
                o_MessegeToUI = "The vehicle already in our garage!";
                o_IsInGarage = true;
                throw new ArgumentException("The vehicle already in our garage");
            }
            else
            {
                o_IsInGarage = false;
                o_MessegeToUI = "New car, Welcome to our garage";
            }
        }

        public void AddNewVehicle(int i_VehicleType, string i_LicenseNumber, string i_ModelType, string i_ManufacturerName, string i_OwnerName, string i_OwnerPhone)
        {
            Vehicle newVehicle;
            VehicleTypesAndBuilder.eVehicleType vehicleType = (VehicleTypesAndBuilder.eVehicleType)i_VehicleType;
            newVehicle = VehicleTypesAndBuilder.BuildVehicle(vehicleType, i_LicenseNumber, i_ModelType, i_ManufacturerName);
            CarOwnerInformation owner = new CarOwnerInformation(newVehicle, i_OwnerName, i_OwnerPhone);
            r_AllVehiclesInfo.Add(i_LicenseNumber, owner);
        }

        public void ChangeVehicleSituation(int i_VehicleSituation, string i_LicenseNumber)
        {
            CarOwnerInformation.eCurrentVehicleSituation vehicleSituation = (CarOwnerInformation.eCurrentVehicleSituation)i_VehicleSituation;
            int situationTypeAmount = Enum.GetValues(typeof(CarOwnerInformation.eCurrentVehicleSituation)).Length;
            if (Enum.IsDefined(typeof(CarOwnerInformation.eCurrentVehicleSituation), i_VehicleSituation))
            {
                if (r_AllVehiclesInfo != null && r_AllVehiclesInfo.ContainsKey(i_LicenseNumber))
                {
                    r_AllVehiclesInfo[i_LicenseNumber].VehicleSituation = vehicleSituation;
                }
                else
                {
                    throw new ArgumentException("This vehicle is not in our garage");
                }
            }
            else
            {
                throw new ValueOutOfRangeException(1, situationTypeAmount, "Value out of range");
            }
        }

        public void ChangeWheelsAirPressureToMax(string i_LicenseNumber)
        {
            if (r_AllVehiclesInfo != null && r_AllVehiclesInfo.ContainsKey(i_LicenseNumber))
            {
                foreach (Wheel wheel in r_AllVehiclesInfo[i_LicenseNumber].OwenersVehicle.GetWheelsList)
                {
                    wheel.TireInflation(wheel.MaxWheelAirPressure - wheel.CurrentAirPressure);
                }
            }
            else
            {
                throw new ArgumentException("This vehicle is not in our garage");
            }
        }

        public void RefuelVehicl(float i_EnergyAmount, int i_EngineType, string i_LicenseNumber)
        {
            if (r_AllVehiclesInfo != null && r_AllVehiclesInfo.ContainsKey(i_LicenseNumber))
            {
                FuelEngine fuelEngine = r_AllVehiclesInfo[i_LicenseNumber].OwenersVehicle.Engine as FuelEngine;
                if (fuelEngine != null)
                {
                    eEngineType engineType = (eEngineType)i_EngineType;
                    if (engineType == fuelEngine.CurrentEngineType)
                    {
                        r_AllVehiclesInfo[i_LicenseNumber].OwenersVehicle.Engine.AddEnergy(engineType, i_EnergyAmount);
                    }
                    else
                    {
                        throw new ArgumentException(string.Format("This vehicle cannot refuel by {0}", engineType.ToString()));
                    }
                }
                else
                {
                    throw new ArgumentException("This vehicle does not have a fuel engine");
                }
            }
            else
            {
                throw new ArgumentException("This vehicle is not in our garage");
            }
        }

        public void RechargeVehicl(float i_MinutesToCharge, string i_LicenseNumber)
        {
            if (r_AllVehiclesInfo != null && r_AllVehiclesInfo.ContainsKey(i_LicenseNumber))
            {
                ElectricEngine ElectricEngine = r_AllVehiclesInfo[i_LicenseNumber].OwenersVehicle.Engine as ElectricEngine;
                if (ElectricEngine != null)
                {
                    if (eEngineType.Electric == ElectricEngine.CurrentEngineType)
                    {
                        r_AllVehiclesInfo[i_LicenseNumber].OwenersVehicle.Engine.AddEnergy(eEngineType.Electric, i_MinutesToCharge / 60);
                    }
                }
                else
                {
                    throw new ArgumentException("This vehicle does not have an electric engine");
                }
            }
            else
            {
                throw new ArgumentException("This vehicle is not in our garage");
            }
        }

        public StringBuilder ClientAndVehicleProperties(string i_LicenseNumber)
        {
            StringBuilder listOfInformation = new StringBuilder();
            if (r_AllVehiclesInfo != null && r_AllVehiclesInfo.ContainsKey(i_LicenseNumber))
            {
                listOfInformation.AppendLine(string.Format(
 @"Owner name: {0}
Owner phone number: {1}
Car situation: {2}",
                 r_AllVehiclesInfo[i_LicenseNumber].OwnerName,
                 r_AllVehiclesInfo[i_LicenseNumber].OwnerPhone,
                 r_AllVehiclesInfo[i_LicenseNumber].VehicleSituation.ToString()));
                listOfInformation.Append(r_AllVehiclesInfo[i_LicenseNumber].OwenersVehicle.ToString());
            }
            else
            {
                throw new ArgumentException("This vehicle is not in our garage");
            }

            return listOfInformation;
        }

        public void SetVehicleProperties(int i_PropertyType, string i_InputFromUser, string i_LicenseNumber)
        {
            if (r_AllVehiclesInfo[i_LicenseNumber] != null)
            {
                r_AllVehiclesInfo[i_LicenseNumber].OwenersVehicle.SetVehiclePropertiesValues(i_PropertyType, i_InputFromUser);
            }
            else
            {
                throw new ArgumentException("No vehicle was set!");
            }
        }

        public List<string> GetVehicleStringProperties(string i_LicenseNumber)
        {
            List<string> vehicleStringProperties = null;
            if (r_AllVehiclesInfo[i_LicenseNumber] != null)
            {
                r_AllVehiclesInfo[i_LicenseNumber].OwenersVehicle.SetVehicleStringProperties();
                vehicleStringProperties = r_AllVehiclesInfo[i_LicenseNumber].OwenersVehicle.GetVehicleStringProperties;
            }
            else
            {
                throw new ArgumentException("No vehicle was set!");
            }

            return vehicleStringProperties;
        }

        public bool IsVehiclesSituationExists(int i_UserInput)
        {
            return isEnumExists<CarOwnerInformation.eCurrentVehicleSituation>(i_UserInput);
        }

        public bool IsVehicleTypeChoiceExists(int i_UserInput)
        {
            return isEnumExists<VehicleTypesAndBuilder.eVehicleType>(i_UserInput);
        }

        public bool IsVehicleEngineTypeExists(int i_UserInput)
        {
            return isEnumExists<eEngineType>(i_UserInput);
        }

        public bool IsVehicleFuelEngine(int i_UserInput)
        {
            bool IsFuelEngine = false;
            if (isEnumExists<eEngineType>(i_UserInput) && (eEngineType)i_UserInput != eEngineType.Electric)
            {
                IsFuelEngine = true;
            }

            return IsFuelEngine;
        }

        private bool isEnumExists<T>(int i_UserInput)
        {
            bool IsDefined = Enum.IsDefined(typeof(T), i_UserInput);
            if (!IsDefined)
            {
                throw new ValueOutOfRangeException(1, Enum.GetValues(typeof(T)).Length, "Value out of range");
            }

            return IsDefined;
        }

        public float MaximumEnergy(string i_LicenseNumber)
        {
            return r_AllVehiclesInfo[i_LicenseNumber].OwenersVehicle.MaxAmountOfEnergy;
        }
    }
}
