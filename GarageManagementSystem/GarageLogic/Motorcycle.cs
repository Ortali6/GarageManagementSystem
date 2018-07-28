using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        public enum eProperties
        {
            LicenseType = 1,
            EngineVolume = 2,
        }

        public enum eLicenseType
        {
            A = 1,
            A1 = 2,
            B1 = 3,
            B2 = 4,
        }

        private eLicenseType m_LicenseType;
        private int m_EngineVolume;
        private const int k_NumberOfWheels = 2;
        private const float k_MaxWheelAirPressure = 29;

        internal Motorcycle(string i_LicenseNumber, string i_ModelType, string i_ManufacturerName)
            : base(i_LicenseNumber, i_ModelType, k_NumberOfWheels, i_ManufacturerName, k_MaxWheelAirPressure)
        {
        }

        public override void SetVehicleStringProperties()
        {
            m_VehicleProperties = new List<string>(2);
            m_VehicleProperties.Add(string.Format(@"Enter type of licence between 1 to 4:
1: A,
2: A1,
3: B1,
4: B2"));
            m_VehicleProperties.Add("Enter Engine volume:");
        }

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
        }

        public int EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }
        }

        public override void SetVehiclePropertiesValues(int i_PropertyType, string i_PropertyValue)
        {
            int valueInputFromUser;
            bool IsValidValue = int.TryParse(i_PropertyValue, out valueInputFromUser);
            if (IsValidValue)
            {
                eProperties propertyChoice = (eProperties)i_PropertyType;
                switch (propertyChoice)
                {
                    case eProperties.LicenseType:
                        {
                            if (Enum.IsDefined(typeof(eLicenseType), valueInputFromUser))
                            {
                                m_LicenseType = (eLicenseType)valueInputFromUser;
                                break;
                            }
                            else
                            {
                                throw new ValueOutOfRangeException(1, Enum.GetValues(typeof(eLicenseType)).Length, "License value is out of range");
                            }
                        }

                    case eProperties.EngineVolume:
                        {
                            m_EngineVolume = valueInputFromUser;
                            break;
                        }
                }
            }
            else
            {
                throw new FormatException("Not a valid input");
            }
        }
    }
}
