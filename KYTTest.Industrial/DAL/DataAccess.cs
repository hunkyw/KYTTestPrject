using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;


namespace KYTTest.Industrial.DAL
{
    class DataAccess
    {
        string TestDataSaveConfig = ConfigurationManager.ConnectionStrings["TestDataSave_config"].ToString();

        MySqlConnection conn;//连接数据库
        MySqlCommand cmd;//数据库操作函数

        private void Dispose()
        {

        }

    }
}
