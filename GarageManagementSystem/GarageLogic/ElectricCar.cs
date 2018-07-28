using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    internal sealed class ElectricCar : Car
    {
        private const eEngineType m_ElectricType = eEngineType.Electric;
        private const float k_MaxAmountOfBattery = 3.6f;

        internal ElectricCar(string i_LicenseNumber, string i_ModelType, string i_ManufacturerName)
            : base(i_LicenseNumber, i_ModelType, i_ManufacturerName)
        {
            m_TypeEngine = new ElectricEngine(m_ElectricType, k_MaxAmountOfBattery);
        }

        public void AddEnergy(int i_EnergyType, float i_EnergyAmountToAdd)
        {
            m_TypeEngine.AddEnergy((eEngineType)i_EnergyType, i_EnergyAmountToAdd);
        }

        public override float MaxAmountOfEnergy
        {
            get
            {
                return k_MaxAmountOfBattery;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Vehicle type: Electric Car");
            stringBuilder.Append(base.ToString());
            return stringBuilder.ToString();
        }
    }
}
