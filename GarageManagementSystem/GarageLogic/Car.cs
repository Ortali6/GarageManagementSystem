using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public abstract class Car : Vehicle
    {
        public enum eProperties
        {
            eNumOfDoors = 1,
            eCarColor = 2,
        }

        public enum eCarColor
        {
            White = 1,
            Black = 2,
            Blue = 3,
            Silver = 4,
        }

        public enum eNumOfDoors
        {
            TwoDoors = 1,
            ThreeDoors = 2,
            FourDoors = 3,
            FiveDoors = 4,
        }

        private eNumOfDoors m_numOfDoors;
        private eCarColor m_CarColor;
        private const int k_NumberOfWheels = 4;
        private const float k_MaxWheelAirPressure = 34;

        internal Car(string i_LicenseNumber, string i_ModelType, string i_ManufacturerName)
            : base(i_LicenseNumber, i_ModelType, k_NumberOfWheels, i_ManufacturerName, k_MaxWheelAirPressure)
        {
        }

        public eCarColor CarColor
        {
            get
            {
                return m_CarColor;
            }
        }

        public eNumOfDoors NumOfDoors
        {
            get
            {
                return m_numOfDoors;
            }
        }

        public override void SetVehicleStringProperties()
        {
            m_VehicleProperties = new List<string>(2);
            m_VehicleProperties.Add(string.Format(@"Enter number of doors between 1 to 4:
1: Two doors,
2: Three doors,
3: Four doors,
4: Five doors"));
            m_VehicleProperties.Add(string.Format(@"Enter car color between 1 to 4:
1: White,
2: Black,
3: Blue,
4: Silver"));
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
                    case eProperties.eNumOfDoors:
                        {
                            if (Enum.IsDefined(typeof(eNumOfDoors), valueInputFromUser))
                            {
                                m_numOfDoors = (eNumOfDoors)valueInputFromUser;
                                break;
                            }
                            else
                            {
                                throw new ValueOutOfRangeException(1, Enum.GetValues(typeof(eNumOfDoors)).Length, "Number of doors value is out of range");
                            }
                        }

                    case eProperties.eCarColor:
                        {
                            if (Enum.IsDefined(typeof(eCarColor), valueInputFromUser))
                            {
                                m_CarColor = (eCarColor)valueInputFromUser;
                                break;
                            }
                            else
                            {
                                throw new ValueOutOfRangeException(1, Enum.GetValues(typeof(eCarColor)).Length, "Car color value is out of range");
                            }
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
