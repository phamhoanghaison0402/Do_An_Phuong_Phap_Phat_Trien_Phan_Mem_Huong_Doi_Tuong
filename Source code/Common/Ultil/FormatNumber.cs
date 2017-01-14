using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Ultil
{
    public class FormatNumber
    {
        /// <summary>
        /// Function format number
        /// </summary>
        /// <param name="strInput">string strInput</param>
        /// <returns>Example 1,500,000</returns>
        public string FormatNumbers(string _strInput)
        {
            string strInput = _strInput;
            int Length = 0;
            if (strInput.IndexOf('.') > 0)
                Length = strInput.Length - (strInput.Length - strInput.IndexOf('.'));
            else
                Length = strInput.Length;
            string afterFormat = "";
            if (Length <= 3)
                afterFormat = strInput;
            else if (Length > 3)
            {
                afterFormat = strInput.Insert(Length - 3, ",");
                Length = afterFormat.IndexOf(",");
                while (Length > 3)
                {
                    afterFormat = afterFormat.Insert(Length - 3, ",");
                    Length = Length - 3;
                }
            }
            return afterFormat;
        }
    }
}
