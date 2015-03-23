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

    public class Contexts
    {
        private Connector _connector;

        public Contexts(Connector connector)
        {
            this._connector = connector;
        }
        public void GetProperties(string names, string index)
        {

            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Contexts.GetProperties");
            this._connector.Push(names);
            this._connector.Push(index);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void SetProperties(string index, string[] props)
        {
            if (props == null || props.Length == 0)
            {
                return;
            }
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Contexts.Setproperties");
            this._connector.Push(index);
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
        public void New()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Contexts.New");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void Drop()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Contexts.Drop");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }

        public void Dup()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Contexts.Dup");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void Swap(string range)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Contexts.Swap");
            this._connector.Push(range);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void OnTop(string selection)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Contexts.OnTop");
            this._connector.Push(selection);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void Intersection(string range, string consolidation)
        {
            if (consolidation == null)
            {
                consolidation = "simple";
            }
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Contexts.Intersection");
            this._connector.Push(range);
            this._connector.Push(consolidation);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void Union(string range, string consolidation)
        {
            if (consolidation == null)
            {
                consolidation = "simple";
            }
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Contexts.Union");
            this._connector.Push(range);
            this._connector.Push(consolidation);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void Amplify(string a, string b)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Contexts.Amplify");
            this._connector.Push(a);
            this._connector.Push(b);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void Split()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Contexts.Split");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void Fetch(string size, string start, string index)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Contexts.Fetch");
            this._connector.Push(size);
            this._connector.Push(start);
            this._connector.Push(index);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void SortBy(string criterion, string order)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Contexts.SortBy");
            this._connector.Push(criterion);
            this._connector.Push(order);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void Learn()
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Contexts.Learn");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void Normalize(string behaviour)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Contexts.Normalize");
            this._connector.Push(behaviour);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
    }
}

