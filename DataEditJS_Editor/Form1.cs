
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataEditJS_Editor
{
    public partial class Form1 : Form
    {
        string testJS = "    function stripFromEnd(inp,num){\r\n" +
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

        public Form1()
        {
            InitializeComponent();
            string a = util.getAppPath();
            jstext.Text = testJS;
            selectedCodeID.Items.AddRange(codeids.codeID);
            selectedCodeID.SelectedIndex = 0;
            List<snippet> snipps = snippets.SampleSnippes;

            List<ToolStripMenuItem> items = new List<ToolStripMenuItem>();
            foreach (snippet sn in snipps) {
                ToolStripMenuItem ti = new ToolStripMenuItem(sn.name,null,snippet_click);
                items.Add(ti);
            }
            ToolStripMenuItem[] menuItems = items.ToArray();
            mnuSnippet.DropDownItems.AddRange(menuItems);
        }

        void snippet_click(object sender, EventArgs e)
        {
            string what = sender.ToString();
            string js = snippets.getSnippet(what);
            jstext.Text = js;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtInput.Text.Length == 0)
            {
                txtError.Text = "Input is empty!";
                txtError.Visible = true;
                return;
            }
            using (ScriptEngine engine = new ScriptEngine("jscript"))
            {
                ParsedScript parsed = engine.Parse(jstext.Text);
                txtError.Text = ""; txtError.Visible = false;
                try
                {
                    String sC = selectedCodeID.SelectedItem.ToString().Substring(selectedCodeID.SelectedItem.ToString().Length - 1);
                    String sI = txtInput.Text;
                    String result = (parsed.CallMethod("dataEdit", new object[] { util.decodeWithHex(sI), sC })).ToString();
                    System.Diagnostics.Debug.WriteLine("result=" + util.encodeWithHex(result));
                    txtResult.Text = util.encodeWithHex(result);
                    txtError.Visible = false;
                }
                catch (ScriptException ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception: " + ex.Line + ":" + ex.Column + ": " + ex.Description + "\n" + ex.Message);
                    txtError.Visible = true;
                    txtError.Text = ex.Message;
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuSend_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.RestoreDirectory = true;
            dlg.Multiselect = false;
            dlg.InitialDirectory = util.getAppPath();
            dlg.Filter = "JS file|*.js|All files|*.*";
            dlg.FilterIndex = 0;
            dlg.CheckFileExists = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                frmSend sendForm = new frmSend(dlg.FileName);
                sendForm.ShowDialog();
            }
        }

        private void mnuSaveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = util.getAppPath();
            dlg.RestoreDirectory = true;
            dlg.Filter = "JS file|*.js|All files|*.*";
            dlg.FilterIndex = 0;
            dlg.ValidateNames = true;
            dlg.DefaultExt = "js";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string s = jstext.Text;
                using (TextWriter tw = new StreamWriter(dlg.FileName))
                {
                    tw.Write(s);
                    tw.Flush();
                }
            }
        }

        private void mnuOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.RestoreDirectory = true;
            dlg.Multiselect = false;
            dlg.InitialDirectory = util.getAppPath();
            dlg.Filter = "JS file|*.js|All files|*.*";
            dlg.FilterIndex = 0;
            dlg.CheckFileExists = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                using (TextReader tr = new StreamReader(dlg.FileName))
                {
                    string s = tr.ReadToEnd();
                    jstext.Text = s;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
