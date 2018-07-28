using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public abstract class Truck : Vehicle
    {
        public enum eProperties
        {
            CarringDangerousMaterials = 1,
            MaxCarryingWeight = 2,
        }

        private const int k_NumberOfWheels = 16;
        private const float k_MaxWheelAirPressure = 30;
        private bool m_IsCarringDangerousMaterials;
        private float k_MaxCarryingWeight;

        internal Truck(string i_LicenseNumber, string i_ModelType, string i_ManufacturerName)
            : base(i_LicenseNumber, i_ModelType, k_NumberOfWheels, i_ManufacturerName, k_MaxWheelAirPressure)
        {
        }

        public override void SetVehicleStringProperties()
        {
            m_VehicleProperties = new List<string>(2);
            m_VehicleProperties.Add("Is carrying dangerous material? Yes press 0 , No press 1");
            m_VehicleProperties.Add("Enter maximum carrying weight");
        }

        public override void SetVehiclePropertiesValues(int i_PropertyType, string i_PropertyValue)
        {
            eProperties propertyChoice = (eProperties)i_PropertyType;
            switch (propertyChoice)
            {
                case eProperties.CarringDangerousMaterials:
                    {
                        int boolValueInputFromUser;
                        bool IsValidBoolValue = int.TryParse(i_PropertyValue, out boolValueInputFromUser);
                        if (IsValidBoolValue)
                        {
                            if (boolValueInputFromUser == 0)
                            {
                                m_IsCarringDangerousMaterials = true;
                                break;
                            }
                            else
                            {
                                if (boolValueInputFromUser == 1)
                                {
                                    m_IsCarringDangerousMaterials = false;
                                    break;
                                }
                                else
                                {
                                    throw new ValueOutOfRangeException(0, 1, "Value out of range");
                                }
                            }
                        }
                        else
                        {
                            throw new FormatException("Not a valid input");
                        }
                    }

                case eProperties.MaxCarryingWeight:
                    {
                        float floatValueInputFromUser;
                        bool IsValidFloatWeight = float.TryParse(i_PropertyValue, out floatValueInputFromUser);
                        if (IsValidFloatWeight)
                        {
                            k_MaxCarryingWeight = floatValueInputFromUser;
                            break;
                        }
                        else
                        {
                            throw new FormatException("Not a valid input");
                        }
                    }
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.ToString());
            if (m_IsCarringDangerousMaterials)
            {
                stringBuilder.AppendLine("Dose carrying dangerous items");
            }
            else
            {
                stringBuilder.AppendLine("Dose not Carrying dangerous items");
            }

            stringBuilder.AppendFormat(
@"Maximum carrying weight : {0}",
            k_MaxCarryingWeight);
            return stringBuilder.ToString();
        }
    }
}