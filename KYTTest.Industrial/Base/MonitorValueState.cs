using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KYTTest.Industrial.Base
{
    /// <summary>
    /// 值对应状态
    /// </summary>
    public enum MonitorValueState
    {
        OK = 0,
        LoLo = 1,
        Low = 2,
        High = 3,
        HiHi = 4
    }
}
