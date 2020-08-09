using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace BaykonIntegrationApp.Service
{
    public sealed class ScaleMachine
    {
        public bool IsConnected { get; private set; }

        public SerialPort serialPort;
        public List<string> AvailablePorts { get; }


        public BackgroundWorker worker;

        //Random random;

        public ScaleMachine()
        {
            // Init worker
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += DoWork;

            // Init helper varables
            AvailablePorts = new List<string>();

            // Create a new SerialPort object with default settings.
            serialPort = new SerialPort();
            //serialPort.ReadTimeout = 3000;
            serialPort.DataReceived += DataReceived;

            serialPort.Encoding = Encoding.UTF8;


            SetPortName();

            // Set the read/write timeouts
            serialPort.ReadTimeout = 500;
            serialPort.WriteTimeout = 500;
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string str = serialPort.ReadLine();
                str = str.Remove(str.Length - 1, 1);
                str = str.Replace(" ", "");
                //MessageBox.Show("=" + str + "=");

                worker.ReportProgress(0, str);
            }
            catch
            {
                serialPort.Close();
            }
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            while (IsConnected)
            {
                //Thread.Sleep(500);
                //scale = random.NextDouble() * 100;
                //string str = serialPort.ReadExisting();
                //if (string.IsNullOrEmpty(str))
                //{
                //    IsConnected = false;
                //}
                //else worker.ReportProgress(0, str);
            }
        }

        // Display Port values and prompt user to enter a port.
        public void SetPortName()
        {
            AvailablePorts.Clear();
            foreach (string s in SerialPort.GetPortNames())
            {
                AvailablePorts.Add(s);
            }
        }

        public void OpenAndRun(string portName)
        {
            serialPort.PortName = portName;
            if (!serialPort.IsOpen)
            {
                try
                {
                    serialPort.Open();
                    if (!string.IsNullOrEmpty(serialPort.ReadExisting()))
                    {
                        IsConnected = true;
                        if (worker.IsBusy)
                            worker.CancelAsync();
                        worker.RunWorkerAsync();
                        MessageBox.Show(CommonLibrary.Resources.Localization.Language.Connected);
                    }
                    else
                    {
                        serialPort.Close();
                        MessageBox.Show(CommonLibrary.Resources.Localization.Language.NotСonnected);
                    }
                }
                catch
                {
                    MessageBox.Show(CommonLibrary.Resources.Localization.Language.NotСonnected);
                }
            }
        }
    }
}