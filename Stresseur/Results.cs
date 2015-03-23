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

    public class Results
    {
        public Connector _connector;

        public Results(Connector _connector)
        {
            this._connector = _connector;
        }
        public void GetProperties(string names, string index)
        {

            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Results.GetProperties");
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
            this._connector.Push("Results.Setproperties");
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
            this._connector.Push("Results.New");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void Drop(string range)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Results.Drop");
            this._connector.Push(range);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }

        public void Dup(string range)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Results.Dup");
            this._connector.Push(range);
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
            this._connector.Push("Results.Swap");
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
            this._connector.Push("Results.OnTop");
            this._connector.Push(selection);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void Intersection(string range)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Results.Intersection");
            this._connector.Push(range);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void Union(int range)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Results.Union");
            this._connector.Push(range.ToString());
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void SelectBy(string column, string Operator, string operand1, string operand2)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Results.SelectBy");
            this._connector.Push(column.ToLower());
            this._connector.Push(Operator);
            this._connector.Push(operand1);
            this._connector.Push(operand2);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void DeleteBy(string column, string Operator, string operand1, string operand2)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Results.DeleteBy");
            this._connector.Push(column.ToLower());
            this._connector.Push(Operator);
            this._connector.Push(operand1);
            this._connector.Push(operand2);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void SortBy(string column, string order)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Results.SortBy");
            this._connector.Push(column);
            this._connector.Push(order);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void UniqueBy(string column)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Results.UniqueBy");
            this._connector.Push(column);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void SelectToTable(string column, string table, string boolUnique)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Results.SelectToTable");
            this._connector.Push(column.ToLower());
            this._connector.Push(table);
            this._connector.Push(boolUnique);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void Fetch(string size, string start, string index) // version API 1.0
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Results.Fetch");
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
        public void Normalize(string behaviour)
        {

            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Results.Normalize");
            this._connector.Push(behaviour);
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
            this._connector.localParams.Clear();
            this._connector.Push("Results.Amplify");
            this._connector.Push(a);
            this._connector.Push(b);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }

    }
}

