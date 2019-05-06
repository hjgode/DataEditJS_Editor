using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataEditJS_Editor
{
    class util
    {
        public static string getAppPath()
        {
            string exePath = System.IO.Path.GetDirectoryName( System.Reflection.Assembly.GetEntryAssembly().Location);
            if (exePath.EndsWith(@"\")) 
                exePath+=@"\";
            return exePath;
        }

        /// <summary>
        /// for output formatting inclusing control codes as hex
        /// </summary>
        /// <param name="sIn"></param>
        /// <returns></returns>
        public static string encodeWithHex(string sIn)
        {
            StringBuilder sb = new StringBuilder();
            foreach(char c in sIn)
            {
                if (c < ' ')
                {
                    //get hex value of char c (converted to ISO-8859-1 byte array using first byte)
                    sb.Append("\\x" + String.Format("{0:x2}", System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(new char[] { c })[0]));
                }
                else
                    sb.Append(c);
            }
            return sb.ToString();
        }
        /// <summary>
        /// for input of hex values to be replaced a control codes
        /// </summary>
        /// <param name="sIn"></param>
        /// <returns></returns>
        public static string decodeWithHex(string sIn)
        {
            string s = sIn;
            Regex regex = new Regex(@"\\x[0-9A-z]{2}");
            var matches = regex.Matches(s);
            foreach (Match match in matches)
            {
                s = s.Replace(match.Value, ((char)Convert.ToByte(match.Value.Replace(@"\x", ""), 16)).ToString());
            }
            return s;
        }
    }
}
