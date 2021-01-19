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
        public bool State { get; set; } = false;

        public string Mseeage { get; set; }

        public T Data { get; set; }
    }
    public class DataResult : DataResult<string> { }
}
