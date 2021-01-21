using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KYTTest.Industrial.Model
{
    class CanMode
    {
        public class CanModel
        {

            public string id { get; set; }//站id
            public int SlaveAddress { get; set; }//地址
            public string FuncCode { get; set; }//功能码
            public int StartAddress { get; set; }//起始地址
            public int Length { get; set; }//长度
        }
    }
}
