using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Common.Ultil
{
    public class CodeMasterGen
    {
        public String AutoNumber(int autoNum)
        {
            if (autoNum < 10)
                return "0000" + autoNum;

            else if (autoNum >= 10 && autoNum < 100)
                return "000" + autoNum;

            else if (autoNum >= 100 && autoNum < 1000)
                return "00" + autoNum;

            else if (autoNum >= 1000 && autoNum < 10000)
                return "0" + autoNum;

            else if (autoNum >= 10000 && autoNum < 100000)
                return "" + autoNum;
            else
                return "";
        }
    }
}
