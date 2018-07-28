using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public class Wheel
    {
        private readonly float r_MaxWheelAirPressure;
        private string m_ManufacturerName;
        private float m_CurrentWheelAirPressure;

        internal Wheel(string i_ManufacturerName, float i_MaxWheelAirPressure)
        {
            m_ManufacturerName = i_ManufacturerName;
            m_CurrentWheelAirPressure = 0;
            r_MaxWheelAirPressure = i_MaxWheelAirPressure;
        }

        public float MaxWheelAirPressure
        {
            get
            {
                return r_MaxWheelAirPressure;
            }
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }
        }

        public void TireInflation(float i_AddAirPressure)
        {
            if (m_CurrentWheelAirPressure + i_AddAirPressure <= r_MaxWheelAirPressure)
            {
                m_CurrentWheelAirPressure += i_AddAirPressure;
            }
            else
            {
                throw new ValueOutOfRangeException(1, r_MaxWheelAirPressure - m_CurrentWheelAirPressure, "Amount of air to add is out of range");
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentWheelAirPressure;
            }

            // $G$ DSN-005 (-5) The setter of this property should not have been public. Modification of the current air pressure should be done in the inflate method exclusively
            set
            {
                if (value >= 0 && value <= r_MaxWheelAirPressure)
                {
                    m_CurrentWheelAirPressure = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, r_MaxWheelAirPressure, "Value out of range");
                }
            }
        }

        public override string ToString()
        {
            StringBuilder wheelInfo = new StringBuilder();
            wheelInfo.AppendLine(string.Format(
@"
Wheel manufacture name: {0}
Wheel maximum air pressure: {1}
Wheel current air pressure: {2} 
",
            m_ManufacturerName,
            r_MaxWheelAirPressure.ToString(),
            m_CurrentWheelAirPressure.ToString()));

            return wheelInfo.ToString();
        }
    }
}