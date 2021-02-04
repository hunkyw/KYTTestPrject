using KYTTest.communication;
using KYTTest.Industrial.BLL;
using KYTTest.Industrial.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KYTTest.Industrial.Base
{
    public class GlobalMonitor
    {
        public static List<ModebusModel> StorageList { get; set; }

        public static List<DeviceMode> DeviceList { get; set; } = new List<DeviceMode>();

        public static SerialInfo SerialInfo { get; set; }

        static bool isRunning = true;

        static Task mainTask = null;
        public static void Start(Action sucessAction, Action <string> faultAction)
        {
            Task.Run(() =>
            {
                IndustrialBLL bll = new IndustrialBLL();
                //获取串口配置信息
                var si = bll.InitSerialInfo();
                if (si.State)
                {
                    SerialInfo = si.Data;
                }
                else
                {
                    faultAction(si.Mseeage); return;//失败回调
                }

                //获取存储区信息
                var sa = bll.InitModeBusModel();

                if (sa.State)
                    StorageList = sa.Data;

                var dr = bll.InitDevices();
                if (dr.State)
                    DeviceList = dr.Data;

                sucessAction();//成功回调

                while(isRunning)
                {
                    
                }
            });
        }

        public static void Dispose()
        {
            isRunning = false;
            if (mainTask != null)
                mainTask.Wait();
        }

    }
}
