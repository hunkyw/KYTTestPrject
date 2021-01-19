using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
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
        public DataResult<SerialInfo> InitSerialInfo()
        {
            DataResult<SerialInfo> result = new DataResult<SerialInfo>();
            result.State = false;

            try
            {
                SerialInfo si = new SerialInfo();
                si.PortName = ConfigurationManager.AppSettings["port"].ToString();
                si.baudRate = int.Parse(ConfigurationManager.AppSettings["baud"].ToString());
                si.DataBit = int.Parse(ConfigurationManager.AppSettings["data_bit"].ToString());
                si.Parity = (Parity)Enum.Parse(typeof(Parity), ConfigurationManager.AppSettings["parity"].ToString(), true);
                si.StopBits = (StopBits)Enum.Parse(typeof(Parity), ConfigurationManager.AppSettings["stop_bits"].ToString(), true);

                result.State = true;
                result.Data = si;
            }
            catch (Exception ex)
            {
                result.Mseeage = ex.Message;

            }
            return result;
        }
    }
}
