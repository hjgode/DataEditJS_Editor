
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
        string testJS = 
                "    //example for function use\r\n" +
                "    function stripFromEnd(inp,num){\r\n" +
                "        out=inp.substr(0,inp.length-num);\r\n" +
                "        return out;\r\n" +
                "    }\r\n" +
                "    \r\n" +
                "    function dataEdit(inStr,inCodeID){\r\n" +
                "        var outStr=\"\";\r\n" +
                "        //call our function that strips some char from end \r\n"+
                "        outStr=stripFromEnd(inStr,2);\r\n" +
                "        //a simple replace using hex value\r\n" +        
                "        outStr=outStr.replace('\x1d', '<GS>');" +
                "        //find the position of something \r\n" +
                "        var pos = outStr.indexOf('456');\r\n" +
                "        inStr=outStr;\r\n" +
                "        if(pos == -1){\r\n" +
                "            outStr=\"not found\";\r\n" +
                "        }else{\r\n" +
                "            //cut this pattern\r\n" +
                "            outStr=inStr.substr(0,pos) + inStr.substr(pos+3);\r\n" +
                "        }\r\n" +
                "        //append a hex defined value\r\n" +
                "        outStr=outStr + '\x1d'\r\n"+
                "        return outStr;\r\n" +
                "    }\r\n";
        bool _textChanged = false;
        bool btextChanged {
            get { return _textChanged; }
            set { _textChanged = value;
                if (_textChanged)
                    statusSaved.Text = "not saved";
                else
                    statusSaved.Text = "saved";
            }
        }
        string _savedFileName = "";
        String savedFileName
        {
            get { return _savedFileName; }
            set { _savedFileName = value;
                if(_savedFileName.Length>0)
                    statusFileName.Text = _savedFileName;
                else
                    statusFileName.Text = "no file";
            }
        }

        public Form1()
        {
            InitializeComponent();
            string a = util.getAppPath();
            jstext.Text = testJS;
            jstext.SelectionStart = 0;
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
            btextChanged = false;
        }

        void snippet_click(object sender, EventArgs e)
        {
            string what = sender.ToString();
            string js = snippets.getSnippet(what);
            jstext.Text = js;
            btextChanged = true;
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
            dlg.OverwritePrompt = false;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string s = jstext.Text;
                using (TextWriter tw = new StreamWriter(dlg.FileName))
                {
                    tw.Write(s);
                    tw.Flush();
                }
                savedFileName = dlg.FileName;
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
                savedFileName = dlg.FileName;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = util.getAppPath();
            dlg.RestoreDirectory = true;
            dlg.Filter = "JS file|*.js|All files|*.*";
            dlg.FilterIndex = 0;
            dlg.ValidateNames = true;
            dlg.DefaultExt = "js";
            dlg.OverwritePrompt = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string s = jstext.Text;
                using (TextWriter tw = new StreamWriter(dlg.FileName))
                {
                    tw.Write(s);
                    tw.Flush();
                }
                savedFileName = dlg.FileName;
            }

        }

        private void Jstext_TextChanged(object sender, EventArgs e)
        {
            btextChanged = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btextChanged)
            {
                DialogResult msgBoxResult = MessageBox.Show("Text changed. Do you want to save?", "Closing", MessageBoxButtons.YesNoCancel);
                switch (msgBoxResult)
                {
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    case DialogResult.Yes:
                        saveFile();
                        break;
                    case DialogResult.No:
                        break;

                }
            }
        }

        private void MnuFileNew_Click(object sender, EventArgs e)
        {
            if(btextChanged)
            {
                saveFile();
            }
            jstext.Text = "";
            savedFileName = "";
            btextChanged = false;
        }

        void saveFile()
        {
            saveFile(savedFileName);
        }
        void saveFile(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                string s = jstext.Text;
                using (TextWriter tw = new StreamWriter(fileName))
                {
                    tw.Write(s);
                    tw.Flush();
                }
            }
            else
            {
                SaveAsToolStripMenuItem_Click(this, new EventArgs());
            }

        }
    }
}
