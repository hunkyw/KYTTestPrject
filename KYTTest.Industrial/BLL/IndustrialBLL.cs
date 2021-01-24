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
using System.Windows;

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
                var MonitorValues = da.GetMonitorValues();

                List<DeviceMode> deviceList = new List<DeviceMode>();
                foreach(var dr in devices.AsEnumerable())
                {
                    DeviceMode dModel = new DeviceMode();
                    deviceList.Add(dModel);

                    dModel.DeviceID = dr.Field<string>("station_id");
                    dModel.DeviceName = dr.Field<string>("station_name");
                    dModel.DeviceConnType = dr.Field<Int32>("station_conn_type");

                    foreach (var mv in MonitorValues.AsEnumerable().Where(m => m.Field<string>("id") == dModel.DeviceID))
                    {
                        MonitorValueModel mvm = new MonitorValueModel();
                        dModel.MonitorValueList.Add(mvm);

                        mvm.ValueID = mv.Field<string>("value_id");
                        mvm.ValueName = mv.Field<string>("value_name");
                        mvm.StorageAreaID = mv.Field<string>("s_srea_id");
                        mvm.StartAddress = mv.Field<Int32>("address");
                        mvm.DataType = mv.Field<string>("data_type");
                        mvm.IsAlarm = mv.Field<Int32>("is_alarm") == 1;
                        mvm.ValueDesc = mv.Field<string>("description");
                        mvm.Unit = mv.Field<string>("unit");


                        // 警戒值
                        var column = mv.Field<string>("alarm_lolo");
                        mvm.LoLoAlarm = column == null ? 0.0 : double.Parse(column);
                        column = mv.Field<string>("alarm_low");
                        mvm.LowAlarm = column == null ? 0.0 : double.Parse(column);
                        column = mv.Field<string>("alarm_high");
                        mvm.HighAlarm = column == null ? 0.0 : double.Parse(column);
                        column = mv.Field<string>("alarm_hihi");
                        mvm.HiHiAlarm = column == null ? 0.0 : double.Parse(column);
                        mvm.ValueStateChanged = (state, msg, value_id) =>
                        {
                            try
                            {
                                Application.Current?.Dispatcher.Invoke(() =>
                                {
                                    var index = dModel.WarningMessageList.ToList().FindIndex(w => w.ValueID == value_id);
                                    if (index > -1)
                                        dModel.WarningMessageList.RemoveAt(index);

                                    if (state != Base.MonitorValueState.OK)
                                    {
                                        dModel.IsWarning = true;
                                        dModel.WarningMessageList.Add(new WarningMessageModel { ValueID = value_id, Message = msg });
                                    }
                                });

                                var ss = dModel.WarningMessageList.Count > 0;
                                if (dModel.IsWarning != ss)
                                {
                                    dModel.IsWarning = ss;
                                }
                            }
                            catch { }
                        };
                    }       
                }

            }
            catch(Exception ex)
            {
                result.Mseeage = ex.Message;
            }



            return result;
        }
        /// <summary>
        /// 获取CAN通信协议
        /// </summary>

        public DataResult<List<CanMode>> InitCANMode()
        {
            DataResult<List<CanMode>> result = new DataResult<List<CanMode>>();
            try
            {
                var devices = da.GetTestderive();
                var Monitor_Values = da.GetMonitorValues();

                List<CanMode> deviceList = new List<CanMode>();
                foreach (var dr in devices.AsEnumerable())
                {
                    CanMode dModel = new CanMode();
                    deviceList.Add(dModel);
                    //TODO:完成CAN报文

                }

            }
            catch (Exception ex)
            {
                result.Mseeage = ex.Message;
            }



            return result;
        }
    }


}
