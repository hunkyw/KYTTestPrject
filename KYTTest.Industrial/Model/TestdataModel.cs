using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KYTTest.Industrial.Model
{
    public class TestdataModel
    {
        public string id { get; set; }
        public int SlaveAddress { get; set; }
        public string FuncCode { get; set; }
        public int StartAddress { get; set; }
        public int Length { get; set; }
    }
}
