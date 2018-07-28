using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, string i_ErrorMessage)
            : base(i_ErrorMessage)
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }

        public float MaximumValue
        {
            get
            {
                return m_MaxValue;
            }

            set
            {
                m_MaxValue = value;
            }
        }

        public float MinimumValue
        {
            get
            {
                return m_MinValue;
            }

            set
            {
                m_MinValue = value;
            }
        }
    }
}
