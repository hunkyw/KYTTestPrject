using KYTTest.Industrial.Base;
using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KYTTest.Industrial.Model
{
    public class MonitorValueModel
    {
        public Action<MonitorValueState, string, string> ValueStateChanged;
        public string ValueID { get; set; } //站号
        public string ValueName { get; set; }//站名
        public string StorageAreaID { get; set; }//存储区ID
        public int StartAddress { get; set; } //地址
        public string DataType { get; set; }
        public bool IsAlarm { get; set; }
        public double LoLoAlarm { get; set; }//低报警值
        public double LowAlarm { get; set; }//低报警值
        public double HighAlarm { get; set; }//高报警值
        public double HiHiAlarm { get; set; }//高报警值
        public string ValueDesc { get; set; }//状态描述
        public string Unit { get; set; } //单位

        private double _currentValue;

        public ChartValues<ObservableValue> Values { get; set; } = new ChartValues<ObservableValue>();

        public double CurrentValue
        {
            get { return _currentValue; }
            set
            {
                _currentValue = value;

                if (IsAlarm)
                {
                    string msg = ValueDesc;
                    MonitorValueState state = MonitorValueState.OK;

                    if (value < LoLoAlarm)
                    { msg += "极低"; state = MonitorValueState.LoLo; }
                    else if (value < LowAlarm)
                    { msg += "过低"; state = MonitorValueState.Low; }
                    else if (value > HiHiAlarm)
                    { msg += "极高"; state = MonitorValueState.HiHi; }
                    else if (value > HighAlarm)
                    { msg += "过高"; state = MonitorValueState.High; }

                    ValueStateChanged(state, msg + "。当前值：" + value.ToString(), ValueID);

                }
                Values.Add(new ObservableValue(value));
                if (Values.Count > 60) Values.RemoveAt(0);

                //this.RaisePropertyChanged();
            }
        }
    }
}
