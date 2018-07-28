using System;
using System.Collections.Generic;
using System.Text;

namespace GarageLogic
{
    public class CarOwnerInformation
    {
        public enum eCurrentVehicleSituation
        {
            InRepair = 1,
            Repaired = 2,
            Paid = 3,
        }

        private readonly string r_OwnerName;
        private string m_OwnerCellPhoneNumber;
        private eCurrentVehicleSituation m_VehicleSituation;
        private Vehicle m_OwenersVehicle;

        public CarOwnerInformation(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerCellPhoneNumber)
        {
            m_OwenersVehicle = i_Vehicle;
            r_OwnerName = i_OwnerName;
            m_OwnerCellPhoneNumber = i_OwnerCellPhoneNumber;
            m_VehicleSituation = eCurrentVehicleSituation.InRepair;
        }

        public eCurrentVehicleSituation VehicleSituation
        {
            get
            {
                return m_VehicleSituation;
            }

            set
            {
                m_VehicleSituation = value;
            }
        }

        public string OwnerName
        {
            get
            {
                return r_OwnerName;
            }
        }

        public string OwnerPhone
        {
            get
            {
                return m_OwnerCellPhoneNumber;
            }
        }

        public Vehicle OwenersVehicle
        {
            get
            {
                return m_OwenersVehicle;
            }
        }
    }
}