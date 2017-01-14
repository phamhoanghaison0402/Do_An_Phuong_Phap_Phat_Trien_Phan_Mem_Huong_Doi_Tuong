using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Ultil
{
    public class FindController
    {
        /// <summary>
        /// Find controller name from Authority Name
        /// </summary>
        /// <param name="authorityName">authority Name</param>
        /// <returns></returns>
        public static string Controller(string authorityName)
        {        
            return authorityName.Replace(" ", "");
        }
    }
}
