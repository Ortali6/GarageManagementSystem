using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public class FuelEngine : EngineType
    {
        private readonly eEngineType m_FuelType;

        internal FuelEngine(eEngineType io_FuelType, float io_MaxAmountOfFuel) : base(io_FuelType, io_MaxAmountOfFuel)
        {
            m_FuelType = io_FuelType;
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
                    throw new ValueOutOfRangeException(0, MaxEnergy, "Amount of fuel is out of range");
                }
                else
                {
                    CurrenteEnergy = value;
                }
            }
        }

        public override eEngineType CurrentEngineType
        {
            get
            {
                return m_FuelType;
            }
        }

        public override void AddEnergy(eEngineType i_FuelType, float i_FuelAmountToAdd)
        {
            eEngineType checkIfCurentFuelType = (eEngineType)i_FuelType;
            if (checkIfCurentFuelType == m_FuelType)
            {
                if (i_FuelAmountToAdd + CurrenteEnergy <= MaxEnergy)
                {
                    CurrenteEnergy += i_FuelAmountToAdd;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, MaxEnergy, "Amount of fuel is out of range");
                }
            }
            else
            {
                throw new ArgumentException("Wrong fuel type");
            }
        }
    }
}