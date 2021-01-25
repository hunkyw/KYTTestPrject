using KYTTest.communication;
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
        public static void Start()
        {
            Task.Run(() =>
            {
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
