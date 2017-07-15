using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VNCAssemblyViewer
{
    class ExternalProgram
    {
        private string m_sResult;
        Process m_oProc;

        public ExternalProgram()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Launches Program
        /// </summary>
        /// <param name="sFilePath"></param>
        /// <param name="sArgs"></param>
        /// <param name="sWorkingDir"></param>
        /// <returns></returns>
        public void Launch(string sFilePath, string sArgs, string sWorkingDir)
        {
            m_sResult = "";
            m_oProc = new Process();
            ProcessStartInfo oInfo;

            if (sArgs == null || sArgs == "")
                oInfo = new ProcessStartInfo(sFilePath);
            else
                oInfo = new ProcessStartInfo(sFilePath, sArgs);

            if (sWorkingDir != null && sWorkingDir != "")
            {
                oInfo.WorkingDirectory = sWorkingDir;
            }

            oInfo.UseShellExecute = true;
            //oInfo.RedirectStandardOutput = true;
            //oInfo.RedirectStandardError = true;
            //if (sInput != null && sInput != "")
            //{
            //    oInfo.RedirectStandardInput = true;
            //}
            m_oProc.StartInfo = oInfo;

            if (m_oProc.Start())
            {
                //if (sInput != null && sInput != "")
                //{
                //    m_oProc.StandardInput.Write(sInput);
                //    m_oProc.StandardInput.Close();
                //}
                //Thread oThread1 = new Thread(new ThreadStart(this.GetError));
                //Thread oThread2 = new Thread(new ThreadStart(this.GetOutput));
                //oThread1.Start();
                //oThread2.Start();
                //int nTotal = 0;
                //int nPause = 50;
                //while (oThread1.IsAlive || oThread2.IsAlive)
                //{
                //    Thread.Sleep(nPause);
                //    nTotal += nPause;
                //    if (nTotal > (nWaitTime > 0 ? nWaitTime : (1000 * 60 * 1)))
                //    {
                //        if (oThread1.IsAlive) oThread1.Abort();
                //        if (oThread2.IsAlive) oThread2.Abort();
                //        break;
                //    }
                //}
                //if (m_oProc.HasExited == false)
                //{
                //    m_oProc.Kill();
                //    m_sResult = "\r\nError: Hung process terminated ...\r\n";
                //}
                //m_oProc.Close();
                //m_oProc.Dispose();
                //return m_sResult;
            }

        }

        public string Run(string sFilePath, string sArgs, string sInput, string sWorkingDir, int nWaitTime)
        {
            m_sResult = "";
            m_oProc = new Process();
            ProcessStartInfo oInfo;
            if (sArgs == null || sArgs == "") oInfo = new ProcessStartInfo(sFilePath);
            else oInfo = new ProcessStartInfo(sFilePath, sArgs);
            if (sWorkingDir != null && sWorkingDir != "")
            {
                oInfo.WorkingDirectory = sWorkingDir;
            }
            oInfo.UseShellExecute = false;
            oInfo.RedirectStandardOutput = true;
            oInfo.RedirectStandardError = true;
            if (sInput != null && sInput != "")
            {
                oInfo.RedirectStandardInput = true;
            }
            m_oProc.StartInfo = oInfo;
            if (m_oProc.Start())
            {
                if (sInput != null && sInput != "")
                {
                    m_oProc.StandardInput.Write(sInput);
                    m_oProc.StandardInput.Close();
                }
                Thread oThread1 = new Thread(new ThreadStart(this.GetError));
                Thread oThread2 = new Thread(new ThreadStart(this.GetOutput));
                oThread1.Start();
                oThread2.Start();
                int nTotal = 0;
                int nPause = 50;
                while (oThread1.IsAlive || oThread2.IsAlive)
                {
                    Thread.Sleep(nPause);
                    nTotal += nPause;
                    if (nTotal > (nWaitTime > 0 ? nWaitTime : (1000 * 60 * 1)))
                    {
                        if (oThread1.IsAlive) oThread1.Abort();
                        if (oThread2.IsAlive) oThread2.Abort();
                        break;
                    }
                }
                if (m_oProc.HasExited == false)
                {
                    m_oProc.Kill();
                    m_sResult = "\r\nError: Hung process terminated ...\r\n";
                }
                m_oProc.Close();
                m_oProc.Dispose();
                return m_sResult;
            }
            else return null;
        }

        public static void SendFeedback()
        {
            var win1 = new User_Interface.Windows.ProvideFeedback();
            win1.Show();
        }

        #region Private Methods

        private void GetError()
        {
            if (m_oProc != null && m_oProc.StandardError != null)
            {
                string sError = "";
                lock (m_oProc.StandardError)
                {
                    sError = m_oProc.StandardError.ReadToEnd();
                }
                if (sError != "")
                {
                    lock (this)
                    {
                        m_sResult += "\r\nError:" + sError + "\r\n";
                    }
                }
            }
        }

        private void GetOutput()
        {
            if (m_oProc != null && m_oProc.StandardOutput != null)
            {
                string sOutput = "";
                lock (m_oProc.StandardOutput)
                {
                    sOutput = m_oProc.StandardOutput.ReadToEnd();
                }
                if (sOutput != "")
                {
                    lock (this)
                    {
                        m_sResult += "\r\n" + sOutput;
                    }
                }
            }
        }

        #endregion
    }
}
