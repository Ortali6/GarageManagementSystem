using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public class ElectricEngine : EngineType
    {
        private readonly eEngineType m_ElectricType;

        internal ElectricEngine(eEngineType io_ElectricType, float io_MaxAmountOfEnergy)
            : base(io_ElectricType, io_MaxAmountOfEnergy)
        {
            m_ElectricType = io_ElectricType;
        }

        public override void AddEnergy(eEngineType i_ElectricType, float i_EnergyAmountToAdd)
        {
            eEngineType checkIfCurentFuelType = (eEngineType)i_ElectricType;
            if (checkIfCurentFuelType == m_ElectricType)
            {
                {
                    if (i_EnergyAmountToAdd + CurrenteEnergy <= MaxEnergy)
                    {
                        CurrenteEnergy += i_EnergyAmountToAdd;
                    }
                    else
                    {
                        throw new ValueOutOfRangeException(0, MaxEnergy, "Amount of energy is out of range");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Not electric type");
            }
        }

        public override eEngineType CurrentEngineType
        {
            get
            {
                return m_ElectricType;
            }
        }

        public override float CurrentAmountOfEnergy
        {
            get
            {
                return CurrenteEnergy;
            }

            set
            {
                if (value > MaxEnergy && value < 0)
                {
                    throw new ValueOutOfRangeException(0, MaxEnergy, "Amount of energy is out of range");
                }
                else
                {
                    CurrenteEnergy = value;
                }
            }
        }
    }
}