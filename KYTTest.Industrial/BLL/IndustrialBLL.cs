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
        /// 获取串口设备信息/当前为从app.config中获取
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
        /// 获取modbus通信协议
        /// </summary>

        public DataResult<List<ModebusModel>> InitModeBusModel()
        {
            DataResult<List<ModebusModel>> result = new DataResult<List<ModebusModel>>();

            try
            {
                ModebusModel mode = new ModebusModel();

                var sa = da.GetmodebudAgr();

                result.State = true;
                result.Data = (from q in sa.AsEnumerable()
                                  select new ModebusModel
                                  {
                                      id = q.Field<string>("id"),
                                      SlaveAddress = q.Field<Int32>("slave_add"),
                                      FuncCode = q.Field<string>("func_code"),
                                      StartAddress = int.Parse(q.Field<string>("start_reg")),
                                      Length = int.Parse(q.Field<string>("length"))
                                  }).ToList();
            }
            catch(Exception ex)
            {
                result.Mseeage = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 获取设备名及通信协议
        /// </summary>

        public DataResult<List<DeviceMode>> InitDevices()
        {
            DataResult<List<DeviceMode>> result = new DataResult<List<DeviceMode>>();
            try
            {
                var devices = da.GetTestderive();
                var Monitor_Values = da.GetMonitorValues();

                List<DeviceMode> deviceList = new List<DeviceMode>();
                foreach(var dr in devices.AsEnumerable())
                {
                    DeviceMode dModel = new DeviceMode();
                    deviceList.Add(dModel);

                    dModel.DeviceID = dr.Field<string>("station_id");
                    dModel.DeviceName = dr.Field<string>("station_name");
                    dModel.DeviceConnType = dr.Field<Int32>("station_conn_type");
                }

            }
            catch(Exception ex)
            {
                result.Mseeage = ex.Message;
            }



            return result;
        }

    }


}
