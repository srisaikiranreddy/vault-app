using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaultApp.Data
{
    public static class VaultData
    {
        //this class can be connected to the database to pull data from dataTable, just mocked for local host settings. 
        public static DataTable Data()
        {
            DataTable userInfo= new DataTable();
            userInfo.Columns.Add("user name", typeof(string));
            userInfo.Columns.Add("password", typeof(string));

            userInfo.Rows.Add(new object[] { "Mike", "12345678" });
            userInfo.Rows.Add(new object[] { "Mike", "123456" });
            userInfo.Rows.Add(new object[] { "Mike", "1234567" });
            return userInfo;
        }

    }
}
