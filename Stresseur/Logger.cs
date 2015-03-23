/**
*
*  RSSRTReader is a lightweight RSS client written in C#
*
*  Copyright 2012 Ashot Aslanyan <ashot.aslanian@gmail.com>
*
*  This file is part of RSSRTReader.
*
*  RSSRTReader is free software: you can redistribute it and/or modify
*  it under the terms of the GNU General Public License as published by
*  the Free Software Foundation, either version 3 of the License, or
*  (at your option) any later version.
*
*  RSSRTReader is distributed in the hope that it will be useful,
*  but WITHOUT ANY WARRANTY; without even the implied warranty of
*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*  GNU General Public License for more details.
*
*  You should have received a copy of the GNU General Public License
*  along with servmonitor.  If not, see <http://www.gnu.org/licenses/>.
*
*/

using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Build.Framework;

namespace RSSRTReader.Misc
{

    class LoggerEventArgs : EventArgs
    {
        public string[] buffer;
        int bufferSize;
        public LoggerEventArgs(int size)
        {
            bufferSize = size;
            buffer = new string[size];
        }


    }

    /// <summary>
    /// Basic logger class
    /// </summary>
    /// <example>
    /// <code>
    /// // Get instance for first
    /// Logger l = Logger.GetInstance("MyClass", true, true)
    /// l.LogMessage("my message");
    /// </code>
    /// </example>
    ///
    class Logger : Microsoft.Build.Utilities.Logger
    {
        /// <summary>
        /// Is debugging on or off
        /// </summary>
        private bool Debug;

        /// <summary>
        /// Send debug messages to file or console
        /// </summary>
        private bool File;

        /// <summary>
        /// Name of log file
        /// </summary>
        private TextWriter LogFilename;

        /// <summary>
        /// Current instance of logger
        /// </summary>
        public static Logger Instance;

        public string[] bufferLines;

        /// <summary>
        /// Class, from where logger was called
        /// </summary>
        private string From;

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private LoggerWindow wnd;
        private StreamWriter sW;
        private int Count;

        private LoggerEventArgs args;

        /// <summary>
        /// Creates instance of <see cref="Logger"/>
        /// </summary>
        /// <param name="inFile">If true then log to file, otherwise to console</param>
        /// <param name="debugOn">If true then displan log messages</param>
        private Logger(bool inFile, bool debugOn)
        {
            this.File = inFile;
            this.Debug = debugOn;
            this.From = "Logger";

            this.Verbosity = LoggerVerbosity.Detailed;

            wnd = null;
            sW = null;

            bufferLines = new string[5];

            if (this.File == false && this.Debug == true)
            {
                wnd = new LoggerWindow();
                wnd.Show();
                sW = null;
            }
            else if (this.File == true && this.Debug == true)
            {
                sW = new StreamWriter("log.txt", true);
                wnd = null;
            }

            this.Log("Logger", "created.");

            args = new LoggerEventArgs(bufferLines.Length);

        }

        /// <summary>
		/// Initialize is guaranteed to be called by MSBuild at the start of the build
		/// before any events are raised.
		/// </summary>
        public override void Initialize(IEventSource eventSource)
        {

        }

        /// <summary>
        /// Hides window with specified <paramref name="title"/>
        /// </summary>
        /// <param name="visible">Set visibility</param>
        /// <param name="title">Window title</param>
        private void HideWindow(bool visible, string title)
        {
            IntPtr hWnd = Logger.FindWindow(null, title);

            if (hWnd != IntPtr.Zero)
            {
                if (!visible)
                    //Hide the window                    
                    Logger.ShowWindow(hWnd, 0); // 0 = SW_HIDE                
                else
                    //Show window again                    
                    Logger.ShowWindow(hWnd, 1); //1 = SW_SHOWNORMA
            }
        }

        /// <summary>
        /// Logs the <paramref name="message"/>
        /// </summary>
        /// <param name="from">From what class message comes</param>
        /// <param name="message">Message, that should be displayed.</param>
        public void Log(string from, string message)
        {
            if (Verbosity.Equals(LoggerVerbosity.Quiet))
                return;

            if (sW != null)
            {
                if (this.Debug)
                    this.LogFilename.WriteLine("[{0}] {1}: {2}", DateTime.Now.ToString(), from, message);
            }

            if (wnd != null)
            {
                if (this.Debug)
                {
                    if (Count < bufferLines.Length)
                    {
                        bufferLines[Count++] = "[" + DateTime.Now.ToString() + "] : " + from + ": " + message;
                    }
                    else
                    {
                        
                        for (int i = 0; i < bufferLines.Length; i++)
                        {
                            args.buffer[i] = bufferLines[i];
                        }


                        IAsyncResult result = wnd.BeginInvoke(wnd.writeTowindow, new object[] { args.buffer });
                        Count = 0;
                        bufferLines[Count++] = "[" + DateTime.Now.ToString() + "] : " + from + ": " + message;
                        wnd.EndInvoke(result);
                        result.AsyncWaitHandle.Close();
                    }
                }

            }
        }



        /// <summary>
        /// Gets instance of <see cref="Logger"/>
        /// </summary>
        /// <param name="from">Class, from where logger was called</param>
        /// <param name="inFile">Send debug messages to file or console</param>
        /// <param name="debugOn">Is debugging on or off</param>
        /// <returns>A <see cref="Logger"/> instance</returns>
        public static Logger GetInstance(string from, bool inFile, bool debugOn)
        {
            if (Logger.Instance == null)
                Logger.Instance = new Logger(inFile, debugOn);

            Logger.Instance.From = from;
            return Logger.Instance;
        }
    }
}
