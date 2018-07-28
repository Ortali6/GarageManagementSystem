using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public static class VehicleTypesAndBuilder
    {
        public enum eVehicleType
        {
            ElectricMotorcycle = 1,
            ElectricCar = 2,
            FuelMotorcycle = 3,
            FuelCar = 4,
            FuelTruck = 5,
        }

        public static StringBuilder GetVehicleTypes()
        {
            StringBuilder VehicleTypes = new StringBuilder();
            int vehilecTypeAmount = Enum.GetValues(typeof(eVehicleType)).Length;
            for (int i = 0; i < vehilecTypeAmount; i++)
            {
                VehicleTypes.AppendLine(string.Format("{0}: {1}", i + 1, Enum.GetName(typeof(eVehicleType), i + 1)));
            }

            return VehicleTypes;
        }

        public static Vehicle BuildVehicle(eVehicleType i_VehicleType, string i_LicenseNumber, string i_ModelType, string i_ManufacturerName)
        {
            Vehicle newVehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleType.ElectricCar:
                    {
                        newVehicle = new ElectricCar(i_LicenseNumber, i_ModelType, i_ManufacturerName);
                        break;
                    }

                case eVehicleType.ElectricMotorcycle:
                    {
                        newVehicle = new ElectricMotorcycle(i_LicenseNumber, i_ModelType, i_ManufacturerName);
                        break;
                    }

                case eVehicleType.FuelCar:
                    {
                        newVehicle = new FuelCar(i_LicenseNumber, i_ModelType, i_ManufacturerName);
                        break;
                    }

                case eVehicleType.FuelMotorcycle:
                    {
                        newVehicle = new FuelMotorcycle(i_LicenseNumber, i_ModelType, i_ManufacturerName);
                        break;
                    }

                case eVehicleType.FuelTruck:
                    {
                        newVehicle = new FuelTruck(i_LicenseNumber, i_ModelType, i_ManufacturerName);
                        break;
                    }
            }

            return newVehicle;
        }
    }
}
