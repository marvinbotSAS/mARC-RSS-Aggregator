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

    public class Table
    {
        private Connector _connector;

        public Table(Connector connector)
        {
            this._connector = connector;
        }











        public void GetInstances(string start, string count)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Table.GetInstances");
            this._connector.Push(start);
            this._connector.Push(count);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }

        public void GetLines(string tbl)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            string s = "Table:" + tbl + ".GetLines";
            this._connector.Push(s);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void GetStructure(string tbl)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            this._connector.Push("Table:" + tbl + ".GetStructure");
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void GetBIndexes(string tbl)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            string s = "Table:" + tbl + ".GetBIndexes";
            this._connector.Push(s);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void GetKIndexes(string tbl)
        {
            if (this._connector._DirectExecute)
            {
                this._connector.OpenScript(null);
            }
            string s = "Table:" + tbl + ".GetKIndexes";
            this._connector.Push(s);
            this._connector.Push("endLine");
            this._connector.AddFunction();
            if (this._connector._DirectExecute)
            {
                this._connector.DoIt();
            }
        }
        public void Create(string name, string owner, string location, string previsional_size, string type, string structure)
        {
            if (name != null)
            {
                if (this._connector._DirectExecute)
                {
                    this._connector.OpenScript(null);
                }
                this._connector.Push("Table.Create");
                this._connector.Push(name);
                this._connector.Push(owner);
                this._connector.Push(location);
                this._connector.Push(previsional_size);
                this._connector.Push(type);
                this._connector.Push(structure);
                this._connector.Push("endLine");
                this._connector.AddFunction();
                if (this._connector._DirectExecute)
                {
                    this._connector.DoIt();
                }
            }
        }
        public void Kill(string name)
        {
            if (name != null)
            {
                if (this._connector._DirectExecute)
                {
                    this._connector.OpenScript(null);
                }
                this._connector.Push("Table.Kill");
                this._connector.Push(name);
                this._connector.Push("endLine");
                this._connector.AddFunction();
                if (this._connector._DirectExecute)
                {
                    this._connector.DoIt();
                }
            }
        }
        public void Insert(string tbl, string values)
        {
            if (tbl != null)
            {
                if (this._connector._DirectExecute)
                {
                    this._connector.OpenScript(null);
                }
                string s = "Table:" + tbl + ".Insert";
                this._connector.Push(s);
                foreach (string str2 in values.Split(new char[] { ';' }))
                {
                    string[] strArray = str2.Split(new char[] { ',' });
                    strArray[0] = KMString.trim(strArray[0].ToLower());
                    strArray[1] = KMString.trim(strArray[1]);
                    this._connector.Push(strArray[0]);
                    this._connector.Push(strArray[1]);
                }
                this._connector.Push("endLine");
                this._connector.AddFunction();
                if (this._connector._DirectExecute)
                {
                    this._connector.DoIt();
                }
            }
        }

        public void Insert(string tbl, string[] values)
        {
            if (tbl != null)
            {
                if (this._connector._DirectExecute)
                {
                    this._connector.OpenScript(null);
                }
                string s = "Table:" + tbl + ".Insert";
                this._connector.Push(s);
                for (int i = 0; i < (values.Length - 1); i += 2)
                {
                    string str2 = values[i].ToLower();
                    string str3 = values[i + 1];
                    str2 = KMString.trim(str2);
                    str3 = KMString.trim(str3);
                    this._connector.Push(str2);
                    this._connector.Push(str3);
                }
                this._connector.Push("endLine");
                this._connector.AddFunction();
                if (this._connector._DirectExecute)
                {
                    this._connector.DoIt();
                }
            }
        }
        public void Update(string tbl, string rowid, string[] field, string[] value)
        {
            if (tbl != null)
            {
                if (this._connector._DirectExecute)
                {
                    this._connector.OpenScript(null);
                }
                string s = "Table:" + tbl + ".Update";
                this._connector.Push(s);
                this._connector.Push(rowid);
                for (int i = 0; i < field.Length; i++)
                {
                    this._connector.Push(field[i].ToLower());
                    this._connector.Push(value[i]);
                }
                this._connector.Push("endLine");
                this._connector.AddFunction();
                if (this._connector._DirectExecute)
                {
                    this._connector.DoIt();
                }
            }
        }
        public void Delete(string tbl, string[] rowids)
        {
            if (tbl != null)
            {
                if (this._connector._DirectExecute)
                {
                    this._connector.OpenScript(null);
                }
                string s = "Table:" + tbl + ".Delete";
                this._connector.Push(s);
                foreach (string str2 in rowids)
                {
                    this._connector.Push(str2);
                }
                this._connector.Push("endLine");
                this._connector.AddFunction();
                if (this._connector._DirectExecute)
                {
                    this._connector.DoIt();
                }
            }
        }
        public void DataAdd(string tbl, string rowid, string column_name, string value)
        {
            if (tbl != null)
            {
                if (this._connector._DirectExecute)
                {
                    this._connector.OpenScript(null);
                }
                string s = "Table:" + tbl + ".DataAdd";
                this._connector.Push(s);
                this._connector.Push(rowid);
                this._connector.Push(column_name.ToLower());
                this._connector.Push(value);
                this._connector.Push("endLine");
                this._connector.AddFunction();
                if (this._connector._DirectExecute)
                {
                    this._connector.DoIt();
                }
            }
        }
        public void Select(string tbl, string mode, string colname, string comparison, string operand1, string operand2)
        {
            if (tbl != null)
            {
                if (this._connector._DirectExecute)
                {
                    this._connector.OpenScript(null);
                }
                string s = "Table:" + tbl + ".Select";
                this._connector.Push(s);
                this._connector.Push(mode);
                this._connector.Push(colname.ToLower());
                this._connector.Push(comparison);
                this._connector.Push(operand1);
                this._connector.Push(operand2);
                this._connector.Push("endLine");
                this._connector.AddFunction();
                if (this._connector._DirectExecute)
                {
                    this._connector.DoIt();
                }
            }
        }
        public void ReadLine(string tbl, string rowid, string[] colnames)
        {
            if (tbl != null)
            {
                if (this._connector._DirectExecute)
                {
                    this._connector.OpenScript(null);
                }
                string s = "Table:";
                s = s + tbl + ".ReadLine";
                this._connector.Push(s);
                this._connector.Push(rowid);
                foreach (string str2 in colnames)
                {
                    this._connector.Push(str2.ToLower());
                }
                this._connector.Push("endLine");
                this._connector.AddFunction();
                if (this._connector._DirectExecute)
                {
                    this._connector.DoIt();
                }
            }
        }
        public void ReadFirstLine(string tbl, string[] colnames)
        {
            if (tbl != null)
            {
                if (this._connector._DirectExecute)
                {
                    this._connector.OpenScript(null);
                }
                string s = "Table:" + tbl + ".ReadFirstLine";
                this._connector.Push(s);
                foreach (string str2 in colnames)
                {
                    this._connector.Push(str2.ToLower());
                }
                this._connector.Push("endLine");
                this._connector.AddFunction();
                if (this._connector._DirectExecute)
                {
                    this._connector.DoIt();
                }
            }
        }

        public void ReadNextLine(string tbl, string[] colnames)
        {
            if (tbl != null)
            {
                if (this._connector._DirectExecute)
                {
                    this._connector.OpenScript(null);
                }
                string s = "Table:" + tbl + ".ReadNextLine";
                this._connector.Push(s);
                foreach (string str2 in colnames)
                {
                    this._connector.Push(str2.ToLower());
                }
                this._connector.Push("endLine");
                this._connector.AddFunction();
                if (this._connector._DirectExecute)
                {
                    this._connector.DoIt();
                }
            }
        }
        public void ReadBlock(string tbl, string rowid, string colname, string start, string count)
        {
            if (tbl != null)
            {
                if (this._connector._DirectExecute)
                {
                    this._connector.OpenScript(null);
                }
                string s = "Table:" + tbl + ".ReadBlock";
                this._connector.Push(s);
                this._connector.Push(rowid);
                this._connector.Push(colname.ToLower());
                this._connector.Push(start);
                this._connector.Push(count);
                this._connector.Push("endLine");
                this._connector.AddFunction();
                if (this._connector._DirectExecute)
                {
                    this._connector.DoIt();
                }
            }
        }
        public void BIndexCreate(string tbl, string colname, string boolUnique)
        {
            if (tbl != null)
            {
                if (this._connector._DirectExecute)
                {
                    this._connector.OpenScript(null);
                }
                string s = "Table:" + tbl + ".BIndexCreate";
                this._connector.Push(s);
                this._connector.Push(colname.ToLower());
                this._connector.Push(boolUnique);
                this._connector.Push("endLine");
                this._connector.AddFunction();
                if (this._connector._DirectExecute)
                {
                    this._connector.DoIt();
                }
            }
        }

        public void ReadContext(string tbl, string rowid)
        {
            if (tbl != null)
            {
                if (this._connector._DirectExecute)
                {
                    this._connector.OpenScript(null);
                }
                string s = "Table:" + tbl + ".ReadContext";
                this._connector.Push(s);
                this._connector.Push(rowid);
                this._connector.Push("endLine");
                this._connector.AddFunction();
                if (this._connector._DirectExecute)
                {
                    this._connector.DoIt();
                }
            }
        }

        public void BIndexDelete(string tbl, string indexName)
        {
            if (tbl != null)
            {
                if (this._connector._DirectExecute)
                {
                    this._connector.OpenScript(null);
                }
                string s = "Table:" + tbl + ".BIndexDelete";
                this._connector.Push(s);
                this._connector.Push(indexName.ToLower());
                this._connector.Push("endLine");
                this._connector.AddFunction();
                if (this._connector._DirectExecute)
                {
                    this._connector.DoIt();
                }
            }
        }
        public void BIndexRebuild(string tbl, string indexName)
        {
            if (tbl != null)
            {
                if (this._connector._DirectExecute)
                {
                    this._connector.OpenScript(null);
                }
                string s = "Table:" + tbl + ".BIndexRebuild";
                this._connector.Push(s);
                this._connector.Push(indexName.ToLower());
                this._connector.Push("endLine");
                this._connector.AddFunction();
                if (this._connector._DirectExecute)
                {
                    this._connector.DoIt();
                }
            }
        }

        public void KIndexCreate(string tbl, string colname)
        {
            if (tbl != null)
            {
                if (this._connector._DirectExecute)
                {
                    this._connector.OpenScript(null);
                }
                string s = "Table:" + tbl + ".KIndexCreate";
                this._connector.Push(s);
                this._connector.Push(colname.ToLower());
                this._connector.Push("endLine");
                this._connector.AddFunction();
                if (this._connector._DirectExecute)
                {
                    this._connector.DoIt();
                }
            }
        }

        public void KIndexDelete(string tbl, string indexName)
        {
            if (tbl != null)
            {
                if (this._connector._DirectExecute)
                {
                    this._connector.OpenScript(null);
                }
                string s = "Table:" + tbl + ".KIndexDelete";
                this._connector.Push(s);
                this._connector.Push(indexName.ToLower());
                this._connector.Push("endLine");
                this._connector.AddFunction();
                if (this._connector._DirectExecute)
                {
                    this._connector.DoIt();
                }
            }
        }
        public void KIndexRebuild(string tbl, string indexName, string boolInterrupt)
        {
            if (tbl != null)
            {
                if (this._connector._DirectExecute)
                {
                    this._connector.OpenScript(null);
                }
                string s = "Table:" + tbl + ".KIndexRebuild";
                this._connector.Push(s);
                this._connector.Push(indexName.ToLower());
                this._connector.Push(boolInterrupt);
                this._connector.Push("endLine");
                this._connector.AddFunction();
                if (this._connector._DirectExecute)
                {
                    this._connector.DoIt();
                }
            }
        }
    }
}

