using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KYTTest.Industrial.Model
{
    public class DeviceMode
    {
        public string DeviceID { get; set; }  //设备ID
        public string DeviceName { get; set; } //设备名称
        public int DeviceConnType { get; set; }//设备通信方式

        /// <summary>
        /// 初始化设备测量值内容
        /// </summary>
        public ObservableCollection<MonitorValueModel> MonitorValueList { get; set; } = new 
            ObservableCollection<MonitorValueModel>();
        /// <summary>
        /// 初始化报警内容
        /// </summary>
        public ObservableCollection<string> WarningMessageList { get; set; } = new 
            ObservableCollection<string>();

        private bool _isRunning;

        public bool IsRunning
        {
            get { return _isRunning; }
            set { _isRunning = value; }
        }

    }

}
