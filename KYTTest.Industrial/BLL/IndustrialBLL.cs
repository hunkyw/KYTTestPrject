using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KYTTest.communication;
using KYTTest.Industrial.DAL;
using KYTTest.Industrial.Model;
using System.Data;

namespace KYTTest.Industrial.BLL
{
    class IndustrialBLL
    {
        DataAccess da = new DataAccess();
        /// <summary>
        /// 获取串口设备信息
        /// </summary>
        public DataResult<SerialInfo> InitSerialInfo()
        {
            DataResult<SerialInfo> result = new DataResult<SerialInfo>();


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
        /// <summary>
        /// 获取通信配置
        /// </summary>

        public DataResult<TestdataModel> InitTestdataModel()
        {
            DataResult<TestdataModel> result = new DataResult<TestdataModel>();

            try
            {
                TestdataModel mode = new TestdataModel();

                var sa = da.GetTestData();
                var values = (from q in sa.AsEnumerable()
                              select new TestdataModel
                              {
                                  
                              }).ToList();

                result.State = true;
                //result.Data = ;
            }
            catch(Exception ex)
            {
                result.Mseeage = ex.Message;
            }
            return result;
        }
    }
}
