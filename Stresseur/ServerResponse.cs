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
    using System.Collections.Generic;

    public class ServerResponse
    {
        public bool _analyse;
        public ArrayList _columns;
        public ArrayList _data;
        public ArrayList _lines;
        public ArrayList _names;
        public ArrayList _sizes;
        public ArrayList _types;
        public bool mError;
        public string mErrorMessage;
        public int mScriptSize;

        public string session_name;

        public string session_id;
        public string toReceive;

        public static int current;

        public KMString kmstring;

        public ServerResponse()
        {
            this._lines = new ArrayList();
            this._columns = new ArrayList();
            this._data = new ArrayList();
            this._names = new ArrayList();
            this._types = new ArrayList();
            this._sizes = new ArrayList();
            this.session_id = "-1";
            this.kmstring = new KMString();
        }

        public ServerResponse(string ret)
        {
            this._lines = new ArrayList();
            this._columns = new ArrayList();
            this._data = new ArrayList();
            this._names = new ArrayList();
            this._types = new ArrayList();
            this._sizes = new ArrayList();
            this.Analyze(ret);
        }

        public void AnalyseLine(string line)
        {
            if (!string.IsNullOrEmpty(line) && (line.Length != 0))
            {
                ArrayList list = new ArrayList();
                ArrayList list2 = new ArrayList();
                ArrayList list3 = new ArrayList();
                Dictionary<string, ArrayList> dictionary = new Dictionary<string, ArrayList>();
                string[] strArray;
                int num3 = -1;
                int num4 = -1;
                while (line.StartsWith(" ") || line.EndsWith(" "))
                {
                    line = line.Trim();
                }
                int index = line.IndexOf('<');
                // attention au cas où une colonne contient "null" !!!!
                int indexNull = line.ToLower().IndexOf("null");
                if (indexNull != -1)
                {

                    if (indexNull < index)
                        index = indexNull;
                }
                if (index == -1)
                {

                    strArray = line.Split(new char[] { ' ' });
                    num3 = int.Parse(strArray[0]);
                    num4 = int.Parse(strArray[1]);
                    this._lines.Add(num3);
                    this._columns.Add(num4);
                    this._types.Add(list);
                    this._names.Add(list2);
                    this._sizes.Add(list3);
                    
                    if (num3 == 0 && num4 == 0)
                    {
                        this._data.Add(dictionary);
                        this.mScriptSize++;
                        return;
                    }
                    strArray = line.Split(new char[] { ' ' });
                }
                else
                {
                    index--;
                    strArray = line.Substring(0, index).Split(new char[] { ' ' });
                }

                if (num3 == -1 && num4 == -1)
                {
                    num3 = int.Parse(strArray[0]);
                    num4 = int.Parse(strArray[1]);
                    this._lines.Add(num3);
                    this._columns.Add(num4);
                    this._types.Add(list);
                    this._names.Add(list2);
                    this._sizes.Add(list3);
                }

                int num5 = 2;
                // on récupère les infos de type et de noms des variables de la ligne courante
                for (int i = 0; i < num4; i++)
                {
                    int num7 = int.Parse(strArray[num5]);
                    num5++;
                    list.Add(Connector.KMTypeLabel[num7]);
                    num7 = int.Parse(strArray[num5]);
                    list3.Add(num7);
                    num5++;
                    list2.Add(strArray[num5]);
                    num5++;
                }

                if (index != -1)
                {
                    strArray = new KMString(line.Substring(index)).Split();

                    for (index = 0; index < list2.Count; index++)
                    {
                        ArrayList list4 = new ArrayList();
                        for (int j = 0; j < num3; j++)
                        {
                            num5 = index + (j * num4);
                            list4.Add(strArray[num5]);
                        }
                        dictionary.Add((string)list2[index], list4);
                    }
                }

                this._data.Add(dictionary);
                this.mScriptSize++;
            }
        }


        public static String GetWord(int idx, String val)
        {
            current = idx;
            String retour = "";
            int pos, i;

            int Count = val.Length;
            pos = idx;
            if (pos >= Count)
            {
                current = 0;
                return retour;
            }
            if (pos < 0)
            {
                current = 0;
                return retour;
            }
            for (i = pos; i < Count; i++)
            {
                if (val[i] > 32)
                {
                    break;
                }
            }
            pos = i;
            if (pos >= Count)
            {
                current = 0;
                return retour;
            }
            for (i = pos; i < Count; i++)
            {
                if (val[i] <= 32)
                {
                    break;
                }
                retour += val[i];
                //System.out.println( "i =" + i+" '"+ (char) val.charAt(i)+"'" );
            }
            current = i + 1;

            return retour;
        }


        public void Analyse(String ret)
        {
            //0 1 1 1 10 0 LastTime <5 4.418/> ;

            Clear();
            mError = false;
            mErrorMessage = "";
            mScriptSize = 0;

            String  session, erreur;
            current = 0;
            byte[] b;
            try
            {
                b = System.Text.Encoding.GetEncoding("ISO-8859-15").GetBytes(ret);
                toReceive = System.Text.Encoding.GetEncoding("ISO-8859-15").GetString(b);
            }
            catch (Exception e)
            {

            }


            int len = toReceive.Length;
            //session_name = GetWord(current, toReceive); //session name

           // session = GetWord(current, toReceive); //session id sous forme string

            session_id = GetWord(current, toReceive);

            erreur = GetWord(current, toReceive); //erreur
            if (erreur.Equals("0"))
            {
                //erreur de script, on récupère le message d'erreur
                mErrorMessage = "code ";
                erreur = GetWord(current, toReceive); //code d'erreur
                mErrorMessage += erreur;
                mErrorMessage += " in " + toReceive.Substring(current);
                mError = true;
                return;
            }
            //on analyse la première ligne de retour
            int idx = current;
            AnalyseLine(idx);
            if (idx == 0) return;
            if (idx == -1) return;
            if (idx > len) return;
            //on analyse les différentes lignes de script s'il y en a
            idx = current;
            while (idx <= len && idx != -1)
            {
                //espace suivant
                while (toReceive[idx] == 32)
                {
                    //System.out.println(  idx+" '"+ (char) toReceive.charAt(idx)+"'" );
                    idx++;
                    //System.out.println(  idx+" '"+ (char) toReceive.charAt(idx)+"'" );
                    if (idx > len) return;
                } //end while gpsk
                //on doit trouver un ";"

                //System.out.println(  idx+" '"+ (char) toReceive.charAt(idx)+"'" ); 
                if (toReceive[idx] != ';') return;
                idx++;
                if (idx >= len) return;
                AnalyseLine(idx);
                idx = current;
                if (idx == 0) return;

            }//end while idx
        }

        public void AnalyseLine(int idx)
        {

            ArrayList list = new ArrayList();
            ArrayList list2 = new ArrayList();
            ArrayList list3 = new ArrayList();
            this._types.Add(list);
            this._names.Add(list2);
            this._sizes.Add(list3);

            Dictionary<String, ArrayList> dictionary = new Dictionary<String, ArrayList>();

            String val, tmp;
            int i, j, idxtmp;

            String lines = GetWord(idx, toReceive);
            int rows = Int32.Parse(lines);
            _lines.Add(rows);

            String columns = GetWord(current, toReceive);
            int cols = Int32.Parse(columns);
            _columns.Add(cols);

            //on lit les types, tailles et noms des colonnes
            for (i = 0; i < cols; i++)
            {
                val = GetWord(current, toReceive);      //type
                list.Add(Connector.KMTypeLabel[Int32.Parse(val)]);
                //
                val = GetWord(current, toReceive);   //tailles
                list3.Add(Int32.Parse(val));
                val = GetWord(current, toReceive);   //noms
                list2.Add( new String( val.ToCharArray() ) );

            }
            //on lit les données
            ArrayList list4 = new ArrayList();
            for (j = 0; j < cols; j++)
            {

                dictionary.Add((String)list2[j], list4);
                if (j != cols - 1)
                {
                    list4 = new ArrayList();
                }
            }


            idx = current;
            for (i = 0; i < rows; i++)
            {
                for (j = 0; j < cols; j++)
                {
                    idxtmp = idx;
                    tmp = GetWord(idxtmp, toReceive);

                    if (tmp.Equals("NULL") || tmp.Equals("VOID"))
                    {
                        idx = current;
                    }
                    else
                    {
                        //tmp = mARC_Connector.KMString.FromString( idx, toReceive );

                        val = kmstring.FromGPBinary(idx, toReceive);

                        if (val != null)
                        {
                            tmp = val;
                        }
                        current = kmstring.idxS;

                        idx = current;
                    }

                    dictionary[ (String)list2[j] ].Add(tmp);
                   
                }

            }

            _data.Add(dictionary);
            mScriptSize++;
        }

        // obsolète !!!!
        public void Analyze(string ret)
        {
            string[] strArray;
            int num2 = 0;
            this.mError = false;
            this.mErrorMessage = "";
            this.mScriptSize = 0;
            this.toReceive = ret;
            while (this.toReceive.StartsWith(" ") || this.toReceive.EndsWith(" "))
            {
                this.toReceive = this.toReceive.Trim();
            }
            string toReceive = this.toReceive;
            // on élimine toutes les lignes 0 1 0 0  ; qui ne servent à rien



            // Console.WriteLine("Analyze toReceive = '" + this.toReceive + "'");

            int indexBegin = toReceive.IndexOf('<');
            int pointvirguleIndex = toReceive.IndexOf(';');
            int indexEnd = toReceive.IndexOf("/>");

            if (((pointvirguleIndex == -1) || (indexBegin == -1)) || (indexEnd == -1))
            {
                strArray = new string[] { this.toReceive };
            }
            else
            {
                bool flag = false;

                List<int> list = new List<int>();
                int jj = 0, kk = 0, ll = -1;

                int current = 0;
            // on élimine toutes les lignes vides

                while (!flag)
                {
                    jj = toReceive.IndexOf("0 1 0 0  ;", current);
                    kk = toReceive.IndexOf("0 0  ;", current);

                    if (jj == -1 && kk == -1 ) // il n'y a plus de lignes vides canoniques mais on cherche une ligne avec un tableau de 0 lignes
                    {
                        ll = toReceive.IndexOf("0 ", current);
                        char llm2 = ' ';
                        if ( ll != -1 && current > 2 )
                         llm2 = toReceive[ll - 2];
                        if ( ll != -1 && ll < pointvirguleIndex && llm2 == ';' )
                        {
                            current = toReceive.IndexOf(";", ll) + 1;
                            list.Add(current - 1);
                            goto nextPattern;
                        }
                    }

                    // existe t il un ';' après le ';' courant ?
                    int tmp = toReceive.IndexOf(";", pointvirguleIndex + 1);
                    if (tmp != -1)
                    {
                        // s'agit il de "; 0 0  ;" ou "; 0 1 0 0  ;" ???
                        if (kk - pointvirguleIndex > 4 || jj - pointvirguleIndex > 4 )
                        // non
                        {
                            int tmpb = toReceive.IndexOf("/>", tmp);
                            // si ce ';' est avant un "/>" on passe
                            if (tmp < tmpb)
                            {
                                // on cherche ts les ';' qui suive le ';' courant avant le "/>"
                                int t = -1;
                                while (tmp != -1 && tmp < tmpb)
                                {
                                    t = tmp;
                                    tmp = toReceive.IndexOf(";", tmp + 1);
                                }
                                if (t != -1)
                                    tmp = t;

                                current = tmp + 1;
                                goto nextPattern;
                            }
                        }
                    }

                    if ( jj != -1 && (pointvirguleIndex > 0 && jj > 0 && jj < pointvirguleIndex)) // on a une ligne qui ne sert à rien
                    {
                        list.Add( jj+10 );
                        current = jj + 11;
                        goto nextPattern;

                    }
                    else if (kk != -1 && (pointvirguleIndex > 0 && kk > 0 && kk < pointvirguleIndex))
                    {
                        list.Add(kk + 6);
                        current = kk + 7;
                        goto nextPattern;
                    }
                    else 
                    {

                        // 1er cas il existe un "/>" après le ';' courant
                        int tmp1 = toReceive.IndexOf("/>", pointvirguleIndex + 1);
                        if (tmp1 != -1)
                        {
                            tmp = toReceive.IndexOf(";", pointvirguleIndex + 1);
                            // on cherche ts les ';' qui suive le ';' courant avant le "/>"
                            int t = -1;
                            while (tmp != -1 && tmp < tmp1)
                            {
                                t = tmp;
                                tmp = toReceive.IndexOf(";", tmp + 1);
                            }
                            if (t != -1)
                                tmp = t;

                            if (kk != -1 || jj != -1)
                            {

                                if (kk > pointvirguleIndex && kk < tmp || jj > pointvirguleIndex && jj < tmp)
                                {
                                    // do nothing
                                }
                                else
                                {
                                    current = tmp + 1;
                                    goto nextPattern;
                                }
                            }

                        }
                        // 2eme cas  '<' ';' '/>'
                        if (indexBegin > 0 && indexEnd > 0 && pointvirguleIndex > 0 && (indexBegin < pointvirguleIndex && pointvirguleIndex < indexEnd))
                        {
                            // ';' est dans une chaine de caractères, on passe
                            current = indexEnd + 2;
                            goto nextPattern;
                        }
                        // 3eme cas '/>' ';' '<'
                        if (indexBegin > 0 && indexEnd > 0 && pointvirguleIndex > 0 && (indexEnd < pointvirguleIndex && pointvirguleIndex < indexBegin))
                        {
                            // ';' est dans une chaine de caractères, on passe
                            current = pointvirguleIndex + 1;
                            list.Add(pointvirguleIndex);
                            goto nextPattern;
                        }
                        // 4eme cas '<' '/>' ';' 
                        if (indexBegin > 0 && indexEnd > 0 && pointvirguleIndex > 0 && (indexBegin < indexEnd && indexEnd < pointvirguleIndex ))
                        {
                            // ';' est il dans une chaine de caractères ?
                            tmp1 = toReceive.IndexOf('<', indexBegin + 1);
                            // on cherche le '<' le plus proche de ';'
                            jj = -1;
                            while (tmp1 != -1 && tmp1 < pointvirguleIndex)
                            {
                                jj = tmp1;
                                tmp1 = toReceive.IndexOf('<', tmp1 + 1);
                            }
                            if (jj != -1)
                                tmp1 = jj;
                            int tmp2 = toReceive.IndexOf("/>", pointvirguleIndex);
                            int tmp3 = toReceive.IndexOf("/>", indexBegin);
                            int tmp4 = -1;
                            if ( tmp1 != -1)
                                tmp4 = toReceive.IndexOf("/>", tmp1);
                            // 1er cas
                            // '<'  '/>' ';'
                            if (  tmp4 != -1 && tmp4 < pointvirguleIndex) // oui on passe
                            {
                                current = pointvirguleIndex + 1;
                                list.Add(pointvirguleIndex);
                                goto nextPattern;
                            }
                            //else if ( tmp2 != -1 )
                            // 1er cas
                            // '<' ';' '/>'
                            if (tmp2 != -1 && tmp1 != -1 && tmp1 < tmp2 ) // && tmp3 >= tmp2) // oui on passe
                            {
                                current = tmp2 + 2;
                                goto nextPattern;
                            }

                            // non 

                            //if ( tmp1 * tmp2 != 1 && pointvirguleIndex < tmp1 && pointvirguleIndex < tmp2)
                            //{
                                current = pointvirguleIndex + 1;
                                list.Add(pointvirguleIndex);
                            //}
                        }
                    }

                nextPattern:
              
                    pointvirguleIndex = toReceive.IndexOf(';', current);
                    if (pointvirguleIndex == -1)
                    {
                        break;
                    }
                    indexBegin = toReceive.IndexOf('<', current);
                    if (indexBegin == -1)
                    {
                        break;
                    }
                    indexEnd = toReceive.IndexOf("/>", indexBegin);
                    if (((indexBegin == -1) && (pointvirguleIndex == -1)) && (indexEnd == -1))
                    {
                        flag = true;
                    }
                }
                strArray = new string[list.Count];
                int num8 = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    //Console.WriteLine("num8 " + num8 + " list[i] = " + (int) (list[i] - num8 + 1) );
                    strArray[i] = this.toReceive.Substring(num8, list[i] - num8 + 1);
                    num8 = list[i] + 1;
                }
            }
            if (strArray.Length == 0)
            {
                strArray = new string[1] { this.toReceive };
            }

            string[] strArray2 = strArray[0].Split(new char[] { ' ' });
            this.session_id = strArray2[0];
            // Console.WriteLine("session id received from mARC server : " + this.session_id);
            num2 = int.Parse(strArray2[1]);
            this.mErrorMessage = "Ok";
            if (num2 == 0)
            {
                this.mErrorMessage = " error code : ";
                this.mErrorMessage = this.mErrorMessage + strArray2[2];
                this.mErrorMessage = this.mErrorMessage + " '";
                toReceive = "";
                for (int j = 3; j < strArray2.Length; j++)
                {
                    toReceive = toReceive + strArray2[j] + " ";
                }
                strArray2 = new KMString(toReceive).Split();
                if (strArray2 != null)
                {
                    this.mErrorMessage = this.mErrorMessage + strArray2[0];
                }
                this.mError = true;
            }
            else if (this._analyse)
            {
                if (strArray[0].IndexOf('<') != -1)
                {
                    int num11 = strArray[0].IndexOf(' ');
                    strArray[0] = strArray[0].Substring(num11 + 1);
                    num11 = strArray[0].IndexOf(' ');
                    strArray[0] = strArray[0].Substring(num11 + 1);
                    // Console.WriteLine("on analyse la ligne 0 : '" + strArray[0] + "'");
                    this.AnalyseLine(strArray[0]);
                }
                else
                {
                    //ligne vide
                    this._lines.Add(0);
                    this._columns.Add(0);
                    ArrayList list = new ArrayList();
                    ArrayList list3 = new ArrayList();
                    ArrayList list2 = new ArrayList();
                    this._types.Add(list);
                    this._names.Add(list2);
                    this._sizes.Add(list3);
                    Dictionary<string, ArrayList> dictionary2 = new Dictionary<string, ArrayList>();
                    this._data.Add(dictionary2);
                    mScriptSize++;
                }
                for (int k = 1; k < strArray.Length; k++)
                {
                    this.AnalyseLine(strArray[k]);
                }
            }
        }

        public void Clear()
        {
            this.session_id = "-1";
            this._names.Clear();
            this._columns.Clear();
            this._lines.Clear();
            this._data.Clear();
            this._sizes.Clear();
        }

        public void CopyFrom(ServerResponse r)
        {
            this.session_id = r.session_id;
            this.mScriptSize = r.mScriptSize;
            this.mError = r.mError;
            this.mErrorMessage = r.mErrorMessage;
            for (int i = 0; i < r._lines.Count; i++)
            {
                this._lines.Add(r._lines[i]);
            }
            for (int j = 0; j < r._columns.Count; j++)
            {
                this._columns.Add(r._columns[j]);
            }
            for (int k = 0; k < r._sizes.Count; k++)
            {
                this._sizes.Add(r._sizes[k]);
            }
            for (int m = 0; m < r._names.Count; m++)
            {
                ArrayList list = new ArrayList();
                ArrayList list2 = (ArrayList) r._names[m];
                for (int num5 = 0; num5 < list2.Count; num5++)
                {
                    list.Add(list2[num5]);
                }
                this._names.Add(list);
            }
            for (int n = 0; n < r._types.Count; n++)
            {
                ArrayList list3 = new ArrayList();
                ArrayList list4 = (ArrayList) r._types[n];
                for (int num7 = 0; num7 < list4.Count; num7++)
                {
                    list3.Add(list4[num7]);
                }
                this._types.Add(list3);
            }
            for (int num8 = 0; num8 < r._sizes.Count; num8++)
            {
                ArrayList list5 = new ArrayList();
                ArrayList list6 = (ArrayList) r._sizes[num8];
                for (int num9 = 0; num9 < list6.Count; num9++)
                {
                    list5.Add(list6[num9]);
                }
                this._sizes.Add(list5);
            }
            for (int num10 = 0; num10 < r._data.Count; num10++)
            {
                Dictionary<string, ArrayList> dictionary = new Dictionary<string, ArrayList>();
                Dictionary<string, ArrayList> dictionary2 = (Dictionary<string, ArrayList>) r._data[num10];
                List<string> list7 = new List<string>(dictionary2.Keys);
                foreach (string str in list7)
                {
                    ArrayList list8 = dictionary2[str];
                    ArrayList list9 = new ArrayList();
                    for (int num11 = 0; num11 < list8.Count; num11++)
                    {
                        list9.Add((string) list8[num11]);
                    }
                    dictionary.Add(str, list9);
                }
                this._data.Add(dictionary);
            }
        }

        ~ServerResponse()
        {
            this.Clear();
        }

        public string GetDataAt(int line, int col, int idx)
        {
            if (idx > (this.mScriptSize - 1))
            {
                return null;
            }
            if (idx == -1)
            {
                idx = this.mScriptSize - 1;
            }
            if (line >= ((int) this._lines[idx]))
            {
                return null;
            }
            if (line < 0)
            {
                return null;
            }
            if (col > ((int) this._columns[idx]))
            {
                return null;
            }
            if (col < 0)
            {
                return null;
            }
            Dictionary<string, ArrayList> dictionary = (Dictionary<string, ArrayList>) this._data[idx];
            ArrayList list = (ArrayList) this._names[idx];
            string str = (string) list[col];
            ArrayList list2 = dictionary[str];
            return (string) list2[line];
        }

        public string[] GetDataByName(string name, int idx)
        {
            if (idx == -1)
            {
                idx = this.mScriptSize - 1;
            }
            if (this._names.Count == 0)
            {
                return null;
            }
            ArrayList list = (ArrayList) this._names[idx];
            if (!list.Contains(name))
            {
                return null;
            }
            Dictionary<string, ArrayList> dictionary = (Dictionary<string, ArrayList>) this._data[idx];
            if (!dictionary.ContainsKey(name))
            {
                return null;
            }
            object[] objArray = dictionary[name].ToArray();
            string[] strArray = new string[objArray.Length];
            int num = 0;
            foreach (object obj2 in objArray)
            {
                strArray[num++] = (string) obj2;
            }
            return strArray;
        }

        public string[] GetDataByLine(int row, int idx)
        {
            if (idx == -1)
            {
                idx = this.mScriptSize - 1;
            }
            if (this._names.Count == 0)
            {
                return null;
            }

            int rows = (int) this._lines[idx] - 1;

            if ( row > rows || rows < 0 )
                return null;

            string[] result = new string[ (int) this._columns[idx] ];

            Dictionary<string, ArrayList> dictionary = (Dictionary<string, ArrayList>)this._data[idx];

            ArrayList names = (ArrayList) this._names[idx]; // les noms de la ligne de réponse idx

            for (int i = 0; i < names.Count; i++)
            {
                result[i] = (string) dictionary[(string)names[i]][row];
                // on prend
            }
                return result;
        }

        public int rowsAtScriptLine(int idx)
        {
            if (idx > (this.mScriptSize - 1))
            {
                idx = this.mScriptSize - 1;
            }
            if (idx == -1)
            {
                idx = this.mScriptSize - 1;
            }
            return (int) this._lines[idx];
        }
    }
}

