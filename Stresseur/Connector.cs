/*
 * Copyright (C) 2015 Marvinbot S.A.S
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

namespace mARC
{
    using System;
    using System.Collections;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    public class Connector
    {
        static readonly object _locker = new object(); // lock object

        public bool _analyse;
        public string[] _properties;
        public string[] _propertieValue;
        private mARC.Contexts _contexts;
        public bool _DirectExecute;
        public string _name;
        public ServerResponse _result;
        private mARC.Results _results;
        private mARC.Server _server;
        private mARC.Table _table;
        private mARC.Session _session;

        private Mutex mutex = new Mutex();

        public string ErrorMsg;
        public static int ID = 0;
        public int idx;
        public string IP;
        public bool isBlocking;
        public bool isConnected;
        public bool isError;
        public bool isValid;
        public int KMCurrentId;
        public int KMError;
        public string KMErrorMsg;
        public string KMFunction;
        public string KMId;
        public string KMParams;
        public string KMScriptSession;
        public int[] KMSessions;
        public KMString kmstring;
        public static string[] KMTypeLabel = new string[15];
        public ArrayList localParams;
        public ArrayList Params;
        public int Port;
        public string Received;
        public string ServerBuild;
        public string ServerName;
        public Socket sock;
        public int TimeLimit;
        public string toReceive;
        public string toSend;
        public string challengeMessage;

        public Connector(bool log)
        {
            this.kmstring = new KMString();
            this.localParams = new ArrayList();
            this._name = "Connector#" + ID++;
            this.Initialize(log);
        }

        public Connector(string aName, bool log)
        {
            this.kmstring = new KMString();
            this.localParams = new ArrayList();
            this._name = aName;
            this.Initialize(log);
        }

        public void Lock()
        {
            mutex.WaitOne();
        }

        public void UnLock()
        {
            mutex.ReleaseMutex();
        }
        public void AddFunction()
        {
            string[] param = new string[this.localParams.Count];
            object[] objArray = this.localParams.ToArray();
            int num = 0;
            foreach (object obj2 in objArray)
            {
                param[num++] = (string) obj2;
            }
            this.AddFunction(param);
            this.localParams.Clear();
        }

        public void AddFunction(string[] param)
        {
            int index = 0;


            while (index < param.Length)
            {
                if (index == param.Length)
                {
                    return;
                }
                this.toSend = this.toSend + param[index] + "(";
                index++;
                while (!param[index].Equals("endLine"))
                {
                    if (index >= param.Length)
                    {
                        break;
                    }
                    string str = param[index]; //.ToLower();
                    if (!str.Equals("null") && !str.Equals("default"))
                    {
                        str = KMString.ToGPBinary(str);
                    }
                    this.toSend = this.toSend + str;
                    index++;
                    if (!param[index].Equals("endLine"))
                    {
                        this.toSend = this.toSend + ", ";
                    }
                }
                index++;
                this.toSend = this.toSend + "); ";
            }
        }

        public void Analyse()
        {
            if (!string.IsNullOrEmpty(this.Received) && this._analyse)
            {
                this._result.Clear();
                this._result._analyse = this._analyse;
                this._result.Analyse(this.Received);
                this.KMErrorMsg = "OK";
                this.KMError = 1;
                if (this._result.mError)
                {
                    this.KMErrorMsg = this._result.mErrorMessage;
                    this.KMError = 0;
                }
            }
        }

        public void clearResults()
        {
            this._result.Clear();
        }

        public void CloseScript()
        {
            this.KMScriptSession = "-1";
            if (this.toSend != null)
            {
                this.toSend = "";
            }
        }

        public bool Connect()
        {
            bool flag = this._analyse;
            this._analyse = true;
            if (this.isValid)
            {
                this.ErrorMsg = "socket already exists : ";
                this.isError = true;
                this.isValid = false;
                this._analyse = flag;
                return false;
            }
            try
            {
                this.sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(this.IP), this.Port);
                this.sock.Connect(remoteEP);
               // sock.Blocking = false;
            }
            catch (Exception exception)
            {
                this.ErrorMsg = "socket creation failure : " + exception.ToString();
                this.isError = true;
                this.isValid = false;
                this._analyse = flag;
                System.Windows.Forms.MessageBox.Show(this.ErrorMsg);
                return false;
            }
            if (this.sock == null)
            {
                try
                {
                    this.sock.Close();
                }
                catch (Exception)
                {
                }
                this.sock = null;
                this.isError = true;
                this.isValid = false;
                this._analyse = flag;
                return false;
            }
            this.isValid = true;
            this.isError = false;
            this.ErrorMsg = "ok";
            this.toSend = "-1 CONNECT (NULL);";

            this.Send();
            this.Receive();
            this.Analyse();
            this.KMScriptSession = this._result.session_id;   // this._result.session_id.ToString();
            this.KMId = this.KMScriptSession;
            //
            String[] sss;

            challengeMessage = Received;

            _properties = new String[18];
            _propertieValue = new String[18];

            Server().GetProperties("port;name;build;type;model;version;command_threads;time_local;idle_time;cache_size;cache_used;cache_hits;exec_timeout_default;session_timeout_default;time_gmt;up_time");

            sss = _result.GetDataByName("prop_value", -1);
            _properties[0] = "Port";
            _propertieValue[0] = sss[0];


            _properties[1] = "Name";
            _propertieValue[1] = sss[1];
            this.ServerName = sss[1];


            _properties[2] = "Build";
            _propertieValue[2] = sss[2];            
            this.ServerBuild = sss[2];


            _properties[3] = "Type";
            _propertieValue[3] = sss[3];

            _properties[4] = "Model";
            _propertieValue[4] = sss[4];


            _properties[5] = "Version";
            _propertieValue[5] = sss[5];


            _properties[6] = "Command Threads";
            _propertieValue[6] = sss[6];

            _properties[7] = "Local Time";
            _propertieValue[7] = sss[7];



            _properties[8] = "Idle Time";
            _propertieValue[8] = sss[8];



            _properties[9] = "Cache Size";
            _propertieValue[9] = sss[9];


            _properties[10] = "Cache Used";
            _propertieValue[10] = sss[10];



            _properties[11] = "Cache Hits";
            _propertieValue[11] = sss[11];


            _properties[12] = "Exec TimeOut Default";
            _propertieValue[12] = sss[12];



            _properties[13] = "Session TimeOut Default";
            _propertieValue[13] = sss[13];


            _properties[14] = "GMT Time";
            _propertieValue[14] = sss[14];


            _properties[15] = "Up Time";
            _propertieValue[15] = sss[15];

            this._analyse = flag;
            if (this.KMError == 0)
            {
                return false;
            }
            this.isConnected = true;


            return true;
        }

        public void GetProperties()
        {
            if ( sock == null || !sock.Connected)
                return;


            bool flag = this._analyse;
            this._analyse = true;

            String[] sss;

            this.KMScriptSession = this._result.session_id;   // this._result.session_id.ToString();
            this.KMId = this.KMScriptSession;
            this.toSend = this.KMScriptSession + " Server.GetPort();";
            this.Send();
            this.Receive();
            this.Analyse();
            sss = _result.GetDataByName("Port", -1);
            _properties[0] = "Port";
            _propertieValue[0] = sss[0];

            this.toSend = this.KMScriptSession + " Server.GetIP();";
            this.Send();
            this.Receive();
            this.Analyse();
            sss = _result.GetDataByName("IP", -1);
            _properties[1] = "IP";
            _propertieValue[1] = sss[0];

            this.toSend = this.KMScriptSession + " Server.GetName();";
            this.Send();
            this.Receive();
            this.Analyse();
            sss = _result.GetDataByName("Name", -1);
            _properties[2] = "Name";
            _propertieValue[2] = sss[0];
            this.ServerName = sss[0];
            //
            this.toSend = this.KMScriptSession + " Server.GetBuild();";
            this.Send();
            this.Receive();
            this.Analyse();
            sss = _result.GetDataByName("Build", -1);
            _properties[3] = "Build";
            _propertieValue[3] = sss[0];
            this.ServerBuild = sss[0];

            this.toSend = this.KMScriptSession + " Server.GetType();";
            this.Send();
            this.Receive();
            this.Analyse();
            sss = _result.GetDataByName("Type", -1);
            _properties[4] = "Type";
            _propertieValue[4] = sss[0];

            this.toSend = this.KMScriptSession + " Server.GetModel();";
            this.Send();
            this.Receive();
            this.Analyse();
            sss = _result.GetDataByName("Model", -1);
            _properties[5] = "Model";
            _propertieValue[5] = sss[0];

            this.toSend = this.KMScriptSession + " Server.GetVersion();";
            this.Send();
            this.Receive();
            this.Analyse();
            sss = _result.GetDataByName("Version", -1);
            _properties[6] = "Version";
            _propertieValue[6] = sss[0];

            this.toSend = this.KMScriptSession + " Server.GetCommandThreads();";
            this.Send();
            this.Receive();
            this.Analyse();
            sss = _result.GetDataByName("CommandThreads", -1);
            _properties[7] = "GetCommandThreads";
            _propertieValue[7] = sss[0];


            this.toSend = this.KMScriptSession + " Server.GetLocalDate();";
            this.Send();
            this.Receive();
            this.Analyse();
            sss = _result.GetDataByName("LocalTime", -1);
            _properties[8] = "LocalTime";
            _propertieValue[8] = sss[0];

            this.toSend = this.KMScriptSession + " Server.GetIdleTime();";
            this.Send();
            this.Receive();
            this.Analyse();
            sss = _result.GetDataByName("IdleTime", -1);
            _properties[9] = "IdleTime";
            _propertieValue[9] = sss[0];

            this.toSend = this.KMScriptSession + " Server.GetCacheSize();";
            this.Send();
            this.Receive();
            this.Analyse();
            sss = _result.GetDataByName("CacheSize", -1);
            _properties[10] = "CacheSize";
            _propertieValue[10] = sss[0];


            this.toSend = this.KMScriptSession + " Server.GetCacheUsed();";
            this.Send();
            this.Receive();
            this.Analyse();
            sss = _result.GetDataByName("CacheUsed", -1);
            _properties[11] = "CacheUsed";
            _propertieValue[11] = sss[0];

            this.toSend = this.KMScriptSession + " Server.GetCacheHits();";
            this.Send();
            this.Receive();
            this.Analyse();
            sss = _result.GetDataByName("CacheHits", -1);
            _properties[12] = "CacheHits";
            _propertieValue[12] = sss[0];

            this.toSend = this.KMScriptSession + " Server.GetExecTimeOutDefault();";
            this.Send();
            this.Receive();
            this.Analyse();
            sss = _result.GetDataByName("ExecTimeOutDefault", -1);
            _properties[13] = "ExecTimeOutDefault";
            _propertieValue[13] = sss[0];

            this.toSend = this.KMScriptSession + " Server.GetSessionTimeOutDefault();";
            this.Send();
            this.Receive();
            this.Analyse();
            sss = _result.GetDataByName("SessionTimeOutDefault", -1);
            _properties[14] = "SessionTimeOutDefault";
            _propertieValue[14] = sss[0];


            this.toSend = this.KMScriptSession + " Server.GetLog();";
            this.Send();
            this.Receive();
            this.Analyse();
            sss = _result.GetDataByName("Log", -1);
            _properties[15] = "Log";
            _propertieValue[15] = sss[0];

            this.toSend = this.KMScriptSession + " Server.GetGMTTime();";
            this.Send();
            this.Receive();
            this.Analyse();
            sss = _result.GetDataByName("GMTTime", -1);
            _properties[16] = "GMTTime";
            _propertieValue[16] = sss[0];

            this.toSend = this.KMScriptSession + " Server.GetUpTime();";
            this.Send();
            this.Receive();
            this.Analyse();
            sss = _result.GetDataByName("UpTime", -1);
            _properties[17] = "UpTime";
            _propertieValue[17] = sss[0];

            /* pour la version 2 du serveur
             * 
            this.toSend = this.KMScriptSession + " Server.Properties();";
            this.Send();
            this.Receive();
            this.Analyse();
            _properties = _result.GetDataByName("Property", -1);
            _propertieValue = _result.GetDataByName("Value", -1);
            this.ServerName = _propertieValue[2];
            this.ServerBuild = _propertieValue[6];
             * 
             * */


            this._analyse = flag;

        }

        public mARC.Contexts Contexts()
        {
            return this._contexts;
        }

        public bool disConnect()
        {
            if (this.sock == null)
            {
                this.isConnected = false;
                this.isValid = false;
                this.isError = false;
                this.KMScriptSession = "-1";
                return true;
            }
            if (this.sock.Connected)
            {
                try
                {
                    this.sock.Shutdown(SocketShutdown.Both);
                    this.sock.Close();
                    this.isConnected = false;
                    this.isValid = false;
                    this.isError = false;
                    this.KMScriptSession = "-1";
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            this.KMScriptSession = "-1";
            this.isError = false;
            this.isConnected = false;
            this.isValid = false;
            return true;
        }

        public void DoIt()
        {
            if (this._DirectExecute)
            {
                this.ExecuteScript();
            }
        }

        public bool Execute(string[] param)
        {
            string kMId;
            this.toSend = "";
            int index = 0;
            string kMstring = param[index];
            if (this.isNumeric(kMstring))
            {
                kMId = kMstring;
                index++;
            }
            else
            {
                kMId = this.KMId;
            }
            string str3 = param[index];
            index++;
            this.toSend = kMId;
            this.toSend = this.toSend + ' ';
            this.toSend = this.toSend + str3;
            this.toSend = this.toSend + " ( ";
            for (int i = index; i < param.Length; i++)
            {
                kMstring = param[i]; //.ToLower();
                if (!kMstring.Equals("null") && !kMstring.Equals("default"))
                {
                    this.kmstring.SetKMstring(kMstring);
                    this.kmstring.ToGPBinary();
                    kMstring = this.kmstring.GetKMstring();
                }
                this.toSend = this.toSend + kMstring;
                this.toSend = this.toSend + " ";
                if (i < (param.Length - 1))
                {
                    this.toSend = this.toSend + ", ";
                }
            }
            this.toSend = this.toSend + ')';
            return this.ExecuteScript();
        }

        public bool ExecuteCommand(string str)
        {
            bool error = false;
            str = KMString.stringToGPBinary(str, error);
            this.toSend = this.KMScriptSession + " " + str;
            error = this.ExecuteScript();
            return error;
        }

        public bool ExecuteScript()
        {

            this.Send();
            this.Receive();
            this.Analyse();
            if (this.KMError == 0)
            {
                return false;
            }

            return true;
        }

        public bool ExecuteScript(string script)
        {
            while (script.Contains("\n") || script.Contains("\r"))
            {
                script = script.Replace('\r', ' ').Replace('\n', ' ');
            }
            string[] strArray = script.Split(new char[] { ';' });
            this.toSend = "";
            bool error = false;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (!string.IsNullOrEmpty(strArray[i]))
                {
                    this.toSend = this.toSend + KMString.stringToGPBinary(strArray[i] + "; ", error);
                }
            }
            this.toSend = this.KMScriptSession + " " + this.toSend;
            return this.ExecuteScript();
        }

        public void formatStringCommand(string command)
        {
        }

        public string[] GetDataByName(string name, int index)
        {
            return this._result.GetDataByName(name, index);
        }

        public string[] GetDataByLine(int row, int idx)
        {
            return this._result.GetDataByLine(row, idx);
        }

        public ServerResponse GetmARCAnswer()
        {
            return this._result;
        }

        public string getReceived()
        {
            return this.Received;
        }

        public void Initialize(bool log)
        {
            this._table = new mARC.Table(this);
            this._server = new mARC.Server(this);
            this._contexts = new mARC.Contexts(this);
            this._results = new mARC.Results(this);
            this._session = new mARC.Session(this);
            this._analyse = true;
            this.KMError = 1;
            this.IP = "127.0.0.1";
            this.Port = 0x4e6;
            this.sock = null;
            this.isConnected = false;
            this.isValid = false;
            this.isError = true;
            this.isBlocking = true;
            this.ErrorMsg = "TCP Socket not created";
            this.TimeLimit = 10;
            this.KMId = "-1";
            this.toSend = "";
            this.toReceive = "";
            this._DirectExecute = true;
            this._result = new ServerResponse();
            this.KMScriptSession = "-1";
            this.KMSessions = new int[10];
            KMTypeLabel[0] = "string";
            KMTypeLabel[1] = "int32";
            KMTypeLabel[2] = "uint32";
            KMTypeLabel[3] = "int8";
            KMTypeLabel[4] = "uint8";
            KMTypeLabel[5] = "char";
            KMTypeLabel[6] = "int64";
            KMTypeLabel[7] = "uint64";
            KMTypeLabel[8] = "string";
            KMTypeLabel[9] = "float";
            KMTypeLabel[10] = "double";
            KMTypeLabel[11] = "bool";
            KMTypeLabel[12] = "simpledate";
            KMTypeLabel[13] = "rowid";
            KMTypeLabel[14] = "sessionid";
        }

        public bool isNumeric(string str)
        {
            try
            {
                double.Parse(str);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }



        public void OpenScript(string session)
        {
            if (session == null)
            {
                session = this.KMId;
            }
            this.KMScriptSession = session;
            this.toSend = KMScriptSession+" ";
            this._result.Clear();
            this.localParams.Clear();
        }

        public void Push(string s)
        {
            this.localParams.Add(new string(s.ToCharArray()));
        }

        public bool Receive()
        {

            Encoding encoding = Encoding.GetEncoding("ISO-8859-15");
            this.Received = "";
            try
            {
                int num = 0x1000;
                int num2 = num;
                int num3 = 0;
                bool flag = false;
                this.Received = "";
                byte[] buffer = new byte[num2];
                while (num2 > 0)
                {
                    try
                    {
                        this.sock.Receive(buffer, 0, buffer.Length, SocketFlags.None);
                    }
                    catch (SocketException ex)
                    {
                        System.Windows.Forms.MessageBox.Show("Receive ERROR : la socket est déconnectée : code d'erreur : " + ex.ErrorCode);
                    }
                    string str = encoding.GetString(buffer);
                    if (str.IndexOf('\0') != -1)
                    {
                        str = str.Substring(0, str.IndexOf('\0') - 1);
                    }
                    this.Received = this.Received + str;
                    num3 += str.Length;
                    num2 -= str.Length;
                    if (!flag && (this.Received.Length > 2))
                    {
                        if (str[0] != '#')
                        {
                            return false;
                        }
                        int length = int.Parse(str.Substring(1, 1));
                        if (length <= 0)
                        {
                            return false;
                        }
                        if (this.Received.Length > (5 + length))
                        {
                            if (str[2] != '#')
                            {
                                return false;
                            }
                            if (str[3 + length] != ' ')
                            {
                                return false;
                            }
                            int num5 = int.Parse(str.Substring(3, length));
                            this.Received = this.Received.Substring(3 + length);
                            num3 = this.Received.Length;
                            num2 = num5 - this.Received.Length;
                            flag = true;
                            buffer = new byte[num];
                        }
                    }
                    if (flag && (num2 == 0))
                    {
                        return true;
                    }
                }
            }
            catch (SocketException e)
            {
                System.Windows.Forms.MessageBox.Show("Receive ERROR : la socket est déconnectée : code d'erreur : "+e.ErrorCode);
               // Environment.Exit(-1);
            }
            return false;
        }

        public mARC.Results Results()
        {
            return this._results;
        }

        public mARC.Session Session()
        {
            return _session;
        }

        public bool Send()
        {
            bool flag = false;
            if (!this.isValid)
            {
                return flag;
            }
            this.kmstring.SetKMstring(this.toSend);
            this.kmstring.ToProtocol();
            this.toSend = this.kmstring.GetKMstring();
            // Console.WriteLine("Send() msg toSend " + this.toSend);
            Encoding encoding = Encoding.GetEncoding(28605); // iso-8859-15
            byte[] buffer = encoding.GetBytes(this.toSend);   //Encoding.Convert(srcEncoding, encoding, bytes);
           // byte[] buffer = this.SuppNoISO(isobytes, isobytes.Length);
            try
            {
                this.sock.Send(buffer, 0, buffer.Length, SocketFlags.None);
            }
            catch (SocketException e)
            {
                System.Windows.Forms.MessageBox.Show("Send ERROR : la socket est déconnectée : code d'erreur : " + e.ErrorCode);
            }
            return true;
        }

        public mARC.Server Server()
        {
            return this._server;
        }



        public byte[] SuppNoISO(byte[] isobytes, int lenghtbuf)
        {
            int index = 0;
            byte[] buffer = new byte[lenghtbuf];
            for (int i = 0; i < isobytes.Length; i++)
            {
                if (isobytes[i] < 0xff)
                {
                    buffer[index] = isobytes[i];
                    index++;
                }
            }
            return buffer;
        }

        public mARC.Table Table()
        {
            return this._table;
        }

        public string UnicodetoIso(string _str)
        {
            Encoding dstEncoding = Encoding.GetEncoding("ISO-8859-1");
            Encoding srcEncoding = Encoding.UTF8;
            byte[] bytes = srcEncoding.GetBytes(_str);
            byte[] buffer2 = Encoding.Convert(srcEncoding, dstEncoding, bytes);
            return dstEncoding.GetString(buffer2);
        }
    }
}

