using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public abstract class EngineType
    {
        private readonly eEngineType r_EngineType;
        protected float m_CurrentEnergy;
        private readonly float r_MaxEnergy;

        internal EngineType(eEngineType i_EngineType, float i_MaxEnergy)
        {
            r_EngineType = i_EngineType;
            r_MaxEnergy = i_MaxEnergy;
        }

        public float MaxEnergy
        {
            get
            {
                return r_MaxEnergy;
            }
        }

        public virtual eEngineType CurrentEngineType
        {
            get
            {
                return r_EngineType;
            }
        }

        public virtual void AddEnergy(eEngineType i_Type, float i_EnergyAmountToAdd)
        {
        }

        protected float CurrenteEnergy
        {
            get { return m_CurrentEnergy; }
            set
            {
                if (value <= r_MaxEnergy)
                {
                    m_CurrentEnergy = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(1, r_MaxEnergy, "Value out of range");
                }
            }
        }

        public virtual float CurrentAmountOfEnergy
        {
            get;
            set;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat(
@"Current Energy amount: {0}
Maximum Energy: {1}",
                         m_CurrentEnergy.ToString(),
                         r_MaxEnergy.ToString());
            return stringBuilder.ToString();
        }
    }
}
