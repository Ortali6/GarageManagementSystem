using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_LicenseNumber;
        private string m_ModelType;
        private float m_CurrentEnergyLeft;
        // $G$ DSN-999 (-3) This List should be readonly.
        private List<Wheel> m_Wheels;
        protected EngineType m_TypeEngine;
        protected List<string> m_VehicleProperties;
        protected List<string> m_VehiclePropertiesValues;

        internal Vehicle(
            string i_LicenseNumber, 
            string i_ModelType, 
            int i_NumOfWheels, 
            string i_ManufacturerName, 
            float i_MaxWheelAirPressure)
        {
            m_ModelType = i_ModelType;
            r_LicenseNumber = i_LicenseNumber;
            m_CurrentEnergyLeft = 0;
            m_Wheels = new List<Wheel>(i_NumOfWheels);
            for (int i = 0; i < i_NumOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(i_ManufacturerName, i_MaxWheelAirPressure));
            }
        }

        public EngineType Engine
        {
            get
            {
                return m_TypeEngine;
            }

            set
            {
                m_TypeEngine = value;
            }
        }

        public List<string> GetVehicleStringProperties
        {
            get
            {
                return m_VehicleProperties;
            }
        } // לקבל את הרשימה של הדפסת התכונות

        public List<string> GetPropertiesValues
        {
            get
            {
                return m_VehiclePropertiesValues;
            }
        } // לקבל את הערכים של התכונות 

        public List<Wheel> GetWheelsList
        {
            get
            {
                return m_Wheels;
            }
        }

        public virtual void SetVehicleStringProperties()
        {
        }

        public virtual void SetVehiclePropertiesValues(int i_PropertyType, string i_PropertyValue)
        {
        }

        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        public override string ToString()
        {
            StringBuilder vehicleInfo = new StringBuilder();
            StringBuilder wheelInfo = new StringBuilder(Environment.NewLine);
            int i = 1;
            foreach (Wheel wheel in m_Wheels)
            {
                wheelInfo.Append(string.Format(
@"Wheel number {0}: {1} ", i, wheel.ToString()));
                i++;
            }

            vehicleInfo.Append(string.Format(
@"License number: {0}
Model: {1}
Engine type: {2}
{3}
Energy bar: {4}%
Wheels information: {5}",
             r_LicenseNumber,
             m_ModelType,
             m_TypeEngine.CurrentEngineType.ToString(),
             m_TypeEngine.ToString(),
             EnergyPercentage.ToString(),
             wheelInfo));
            return vehicleInfo.ToString();
        }

        public float EnergyPercentage
        {
            get
            {
                return (float)(m_TypeEngine.CurrentAmountOfEnergy / m_TypeEngine.MaxEnergy) * 100f;
            }
        }

        public virtual float MaxAmountOfEnergy
        {
            get;
            private set; // Added because did not compile
        }
    }
}