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

    public class Server
    {
        private Connector _connector;

        public Server(Connector _connector)
        {
            this._connector = _connector;
        }

        public void ShutDown(string option)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Server.ShutDown");
            if ( !string.IsNullOrEmpty(option) )
            {
                this._connector.Push(option);
            }
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }


        public void GetAPI()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Server.GetAPI");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }

        public void SetProperties(string[] props)
        {
            if ( props == null || props.Length == 0 )
            {
                return;
            }
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Server.Setproperties");
            for (int i = 0; i < props.Length;i++ )
            {
                this._connector.Push(props[i]);
            }
                this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }

        public void GetProperties(string names)
        {

            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Server.GetProperties");
            this._connector.Push(names);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }

        public void GetConnected(string start, string count)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Server.GetConnected");
            if ( start == null )
                this._connector.Push("1");
            if (count == null)
                this._connector.Push("-1");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }


        public void GetTasks()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Server.GetTasks");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
    }
}

