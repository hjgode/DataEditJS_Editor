﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpAdbClient;
using System.IO;
using System.Net;
using System.Threading;

namespace DataEditJS_Editor
{
    public partial class frmSend : Form 
    {
        SharpAdbClient.AdbServer server = null;
        SharpAdbClient.AdbClient client = null;
        string _filename = "";
        public frmSend(string filename)
        {
            InitializeComponent();
            _filename = filename;
            server = new SharpAdbClient.AdbServer();
            var adbPath = @"C:\android - sdk\platform - tools\adb.exe";
            StartServerResult result;
            if (!System.IO.File.Exists(adbPath))
                adbPath = @"C:\ProgramData\Microsoft\AndroidSDK\25\platform-tools\adb.exe";
            if (!System.IO.File.Exists(adbPath))
            {
                MessageBox.Show("Unable to find adb.exe", "Error");
                return;
            }
            result = server.StartServer(adbPath, restartServerIfNewer: false);
            client = new SharpAdbClient.AdbClient();
            List<SharpAdbClient.DeviceData> devices = client.GetDevices();
            foreach (SharpAdbClient.DeviceData device in devices)
                cboListDevices.Items.Add(device);
            if (cboListDevices.Items.Count > 0)
                cboListDevices.SelectedIndex = 0;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SharpAdbClient.DeviceData device = (SharpAdbClient.DeviceData) cboListDevices.SelectedItem;
            UploadFile(device, _filename, " / storage/emulated/0/Documents/dataEdit.js");
        }

        void DownloadFile(SharpAdbClient.DeviceData device, string fileLocal, string fileRemote)
        {
            //var device = AdbClient.Instance.GetDevices().First();
            try {
                using (SyncService service = new SyncService(new AdbSocket(new IPEndPoint(IPAddress.Loopback, AdbClient.AdbServerPort)), device))
                using (Stream stream = File.OpenWrite(fileLocal))
                {
                    service.Pull(fileRemote, stream, null, CancellationToken.None);
                }
                textBox1.Text = "File loaded OK";
            }catch(Exception ex)
            {
                textBox1.Text = ex.Message;
            }
        }

        void UploadFile(SharpAdbClient.DeviceData device, string fileLocal, string fileRemote)
        {
            //var device = AdbClient.Instance.GetDevices().First();
            try
            {
                using (SyncService service = new SyncService(new AdbSocket(new IPEndPoint(IPAddress.Loopback, AdbClient.AdbServerPort)), device))
                using (Stream stream = File.OpenRead(fileLocal))
                {
                    service.Push(stream, fileRemote /* "/storage/emulated/0/Documents/dataedit.js" */, 444, DateTime.Now, null, CancellationToken.None);
                }
                textBox1.Text = "File send successfully";
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message;
            }
        }

        private void frmSend_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}
