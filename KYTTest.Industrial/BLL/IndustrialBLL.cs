using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KYTTest.communication;

namespace KYTTest.Industrial.BLL
{
    class IndustrialBLL
    {
        /// <summary>
        /// 获取串口设备信息
        /// </summary>
        public SerialInfo InitSerialInfo()
        {
            SerialInfo si = new SerialInfo();

            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return si;
        }
    }
}
