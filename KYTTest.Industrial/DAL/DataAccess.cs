using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;


namespace KYTTest.Industrial.DAL
{
    class DataAccess
    {
        /// <summary>
        /// 测试数据sql数据库参数
        /// </summary>
        string TestDataSaveConfig = ConfigurationManager.ConnectionStrings["TestDataSave_config"].ToString();

        MySqlConnection conn;//连接数据库
        MySqlCommand cmd;//数据库操作函数
        MySqlDataAdapter adapter;//建立虚拟表



        /// <summary>
        /// 查询数据库 （sqlcondig sql配置 sqlstr sql语句 ）返回datatable表
        /// </summary>
        public DataTable ExecuteQuery(string sqlconfig, string sqlstr)
        {

            DataTable dt = new DataTable();
            conn = new MySqlConnection(sqlconfig);//创建连接类
            try
            {
                conn.Open();//打开连接
                cmd = new MySqlCommand(sqlstr, conn);//执行查询语句
                cmd.CommandType = CommandType.Text; //以文本的凡是解释命令行字符串
                adapter = new MySqlDataAdapter(cmd);//数据更新参数
                adapter.Fill(dt);

            }
            catch(Exception ex)
            {
                throw ex;//抛出异常
            }
            finally
            {
                conn.Close();//最后都要关闭连接
            }
            return dt;
        }
        /// <summary>
        /// 获取测试数据 返回datatable表
        /// </summary>
        public DataTable GetTestData()
        {
            string strsql = "select * from test_data";
            return this.ExecuteQuery(TestDataSaveConfig,strsql);
        }
        /// <summary>
        /// 获取当前测试数据 返回datatable表
        /// </summary>
        public DataTable GetTestConfig()
        {
            string strsql = "select * from test_config";
            return this.ExecuteQuery(TestDataSaveConfig, strsql);
        }
        /// <summary>
        /// 获取设备通信参数 返回datatable表
        /// </summary>
        public DataTable GetTestderive()
        {
            string strsql = "select * from test_derive_config";
            return this.ExecuteQuery(TestDataSaveConfig, strsql);
        }
        /// <summary>
        /// 获取modebus通信协议 返回datatable表
        /// </summary>
        public DataTable GetmodebudAgr()
        {
            string strsql = "select * from storage_area_modebus";
            return this.ExecuteQuery(TestDataSaveConfig, strsql);
        }
        /// <summary>
        /// 获取siemens通信协议 返回datatable表
        /// </summary>
        public DataTable GetSiemensAgr()
        {
            string strsql = "select * from storage_area_siemens";
            return this.ExecuteQuery(TestDataSaveConfig, strsql);
        }
        /// <summary>
        /// 获取can通信协议 返回datatable表
        /// </summary>
        public DataTable GetCanAgr()
        {
            string strsql = "select * from storage_area_can";
            return this.ExecuteQuery(TestDataSaveConfig, strsql);
        }
        /// <summary>
        /// 获取can通信配置 返回datatable表
        /// </summary>
        public DataTable GetCanConfig()
        {
            string strsql = "select * from can_config";
            return this.ExecuteQuery(TestDataSaveConfig, strsql);
        }
        /// <summary>
        /// 获取当前测试值 返回datatable表
        /// </summary>
        public DataTable GetMonitorValues()
        {
            string strsql = "select * from monitor_values";
            return this.ExecuteQuery(TestDataSaveConfig, strsql);
        }

    }
}
