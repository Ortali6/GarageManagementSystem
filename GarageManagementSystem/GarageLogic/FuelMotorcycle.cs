using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    internal sealed class FuelMotorcycle : Motorcycle
    {
        private const eEngineType m_FuleType = eEngineType.Octran98;
        private const float k_MaxAmountOfFuel = 7.5f;

        internal FuelMotorcycle(string i_LicenseNumber, string i_ModelType, string i_ManufacturerName)
            : base(i_LicenseNumber, i_ModelType, i_ManufacturerName)
        {
            m_TypeEngine = new FuelEngine(m_FuleType, k_MaxAmountOfFuel);
        }

        public void AddFuel(int i_FuelType, float i_FuelAmountToAdd)
        {
            m_TypeEngine.AddEnergy((eEngineType)i_FuelType, i_FuelAmountToAdd);
        }

        public override float MaxAmountOfEnergy
        {
            get
            {
                return k_MaxAmountOfFuel;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Vehicle type: Fuel Motorcycle");
            stringBuilder.Append(base.ToString());
            return stringBuilder.ToString();
        }
    }
}
