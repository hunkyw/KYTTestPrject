using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KYTTest.Industrial.BLL
{
    /// <summary>
    /// 错误传输类
    /// </summary>
    public class DataResult<T>
    {
        public bool State { get; set; } = false;//状态

        public string Mseeage { get; set; }//错误 成功信息

        public T Data { get; set; }//读取的数据
    }
    public class DataResult : DataResult<string> { }
}
