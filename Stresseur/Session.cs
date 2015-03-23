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

    public class Session
    {
        private Connector _connector;

        public Session(Connector connector)
        {
            this._connector = connector;
        }

        public void Connect()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.Connect");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void GetInstances()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.GetInstances");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void Clear(string option)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.Clear");
            if ( !string.IsNullOrEmpty(option))
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
        public void GetProperties(string names)
        {

            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.GetProperties");
            this._connector.Push(names);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void SetProperties(string[] props)
        {
            if (props == null || props.Length == 0)
            {
                return;
            }
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.Setproperties");
            for (int i = 0; i < props.Length; i++)
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
        public void contextToInhibitor()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.contextToInhibitor");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }

        public void contextToProfiler()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.contextToProfiler");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void InhibitorToContext()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.InhibitorToContext");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void ProfilerToContext()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.ProfilerToContext");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void GetLastDBInfo()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.GetLastDBInfo");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void MarcSave()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.MarcSave");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void MarcReload()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.MarcReload");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void MarcClear()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.MarcClear");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void MarcRebuild(string fields, string db_id_begin, string db_id_end, string refs)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.MarcRebuild");
            this._connector.Push(fields);
            this._connector.Push(db_id_begin);
            this._connector.Push(db_id_end);
            this._connector.Push(refs);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void MarcPublish()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.MarcPublish");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void DocToContext(string rowId, string boolSpectrum)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.DocToContext");
            this._connector.Push(rowId);
            this._connector.Push(boolSpectrum);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void ContextToDoc(string rowId, string boolSpectrum)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.ContextToDoc");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void StringToContext(string signal, string boolLearn)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.DocToContext");
            this._connector.Push(signal);
            this._connector.Push(boolLearn);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void ContextToContext(string rowId, string boolSpectrum)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.ContextToContext");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void Store(string text, string mode, string rowid)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.Store");
            this._connector.Push(text);
            this._connector.Push(mode);
            this._connector.Push(rowid);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void Index( string rowid)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.Index");
            this._connector.Push(rowid);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void GetSpectrum()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.GetSpectrum");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }

        public void SetSpectrum(string properties)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.SetSpectrum");
            this._connector.Push(properties);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void ApplySpectrum()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Session.ApplySpectrum");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }

    }
}

