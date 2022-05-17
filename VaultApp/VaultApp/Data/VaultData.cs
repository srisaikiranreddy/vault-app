using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaultApp.Data
{
    public static class VaultData
    {
        //this class can be connected to the database to pull data from dataTable, just mocked for local host settings. 
        public static List<string> Data()
        {
            return new List<string>()
            {
                "APPLEDB",
                "MACDB"
            };
        }

    }
}
