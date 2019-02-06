using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEditJS_Editor
{
    public class snippets
    {
        public static string getSnippet(string s)
        {
            foreach(snippet sn in SampleSnippes)
            {
                if (sn.name == s)
                    return sn.code;
            }
            return "";
        }

        static string testJS = "    function stripFromEnd(inp,num){\r\n" +
        "        out=inp.substr(0,inp.length-num);\r\n" +
        "        return out;\r\n" +
        "    }\r\n" +
        "    \r\n" +
        "    function dataEdit(inStr,inCodeID){\r\n" +
        "        var outStr=\"\";\r\n" +
        "        outStr=stripFromEnd(inStr,2);\r\n" +
        "        var pos = outStr.indexOf('456');\r\n" +
        "        inStr=outStr;\r\n" +
        "        if(pos == -1){\r\n" +
        "            outStr=\"not found\";\r\n" +
        "        }else{\r\n" +
        "            //cut this pattern\r\n" +
        "            outStr=inStr.substr(0,pos) + inStr.substr(pos+3);\r\n" +
        "        }\r\n" +
        "        return outStr;\r\n" +
        "    }\r\n";

        public static List<snippet> SampleSnippes=new List<snippet>() {
            new snippet("cut some digits", testJS),
            new snippet(
                "FNC1 conversion",
@"function dataEdit(inStr, sAimID) { 
  var v2 = inStr.replace(/\x1D/g,'@');
  return v2;
}
"
            ),
            new snippet("Add CrLf",
@"function addCRLF(str){
  var s = str + '\n\r';
  return s;
}

function dataEdit(inStr, sCodeID)
{
    v2 = addCRLF(inStr);
    return v2;
}
")
        };
    }
    public class snippet
    {
        public string name { get; set; }
        public string code { get; set; }

        public snippet(string n, string c)
        {
            name = n;
            code = c;
        }
    }
}
