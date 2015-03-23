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
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    public class KMString
    {
        private int idx1;
        private string str;
        public  int idxS;

        public KMString()
        {
        }

        public KMString(string _kmstring)
        {
            this.str = _kmstring;
        }

        public String FromGPBinary(int idx, String src)
        {
            bool ok;
            int taille, pos, i;
            String no;

            if (src == null) { return null; }
            if (src.Length <= 5) { return null; }
            pos = idx;
            idx = 0;
            for (i = pos; i < src.Length; i++)
            {
                if (src[i] > 32) break;
            }
            pos = i;

            if (pos > src.Length ) return src;

            ok = (src[pos] == '<');
            if (ok == false) return null;
            pos++;
            no = ServerResponse.GetWord(pos, src);
            pos = ServerResponse.current;
            int n = 0;
            if (Int32.TryParse(no, out n) == false) return null;
            taille = n;
            if ((pos + taille + 1) > src.Length ) return null;
            ok &= (src[pos + taille + 1] == '>');
            ok &= (src[pos + taille] == '/');
            if (!ok) return null;

            String retour = "";
            for (i = pos; i < pos + taille; i++)
            {
                retour += src[i];
            }
            idxS = pos + taille + 2;
            return retour;
        }

        public void FromGPBinary()
        {
            // Console.WriteLine("str = " + this.str);
            int index = this.str.IndexOf(" ");
            // Console.WriteLine("idx" + indexBegin);
            int num2 = int.Parse(this.str.Substring(1, index));
            this.str = this.str.Substring(index + 1, (index + 1) + num2);
        }

        public void FromProtocol()
        {
            int num = int.Parse(this.str.Substring(1, 2));
            int num2 = int.Parse(this.str.Substring(3, 3 + num));
            this.str = this.str.Substring(4 + num, (4 + num) + num2);
        }

        public int GetIdx()
        {
            return this.idx1;
        }

        public string GetKMstring()
        {
            return this.str;
        }

        public string GetNextstring()
        {
            int num;
            int length = this.str.Length;
            if (this.idx1 < 0)
            {
                this.idx1 = 0;
            }
            if (this.idx1 >= length)
            {
                this.idx1 = -1;
                return "";
            }
            char[] chArray = this.str.ToArray<char>();
            while (chArray[this.idx1] == ' ')
            {
                this.idx1++;
                if (this.idx1 >= length)
                {
                    this.idx1 = -1;
                    return "";
                }
            }
            if (chArray[this.idx1] == '<')
            {
                this.idx1++;
                num = this.idx1;
                while (chArray[num] != ' ')
                {
                    num++;
                }
                int num3 = int.Parse(this.str.Substring(this.idx1, num));
                this.idx1 = (num + num3) + 3;
                if (this.idx1 >= length)
                {
                    this.idx1 = -1;
                }
                if (num3 == 0)
                {
                    return "";
                }
                num++;
                return this.str.Substring(num, num + num3);
            }
            num = this.idx1;
            while (chArray[num] != ' ')
            {
                num++;
            }
            int startIndex = this.idx1;
            this.idx1 = num;
            return this.str.Substring(startIndex, num);
        }

        public void SetKMstring(string _str)
        {
            this.idx1 = 0;
            this.str = _str;
        }

        public string[] Split()
        {
            ArrayList list = new ArrayList();
            this.idx1 = 0;
            int index = this.str.IndexOf("<");
            int indexNull = str.ToLower().IndexOf("null");
            if (index != -1 || indexNull != -1)
            {
                if (indexNull > 0 && indexNull < index)
                {
                    list.Add("NULL");
                    indexNull = str.ToLower().IndexOf("null", indexNull + 5);
                }
                int num2 = this.str.IndexOf("/>");
                if (num2 != -1)
                {
                    while ((num2 != -1) && (index != -1) || indexNull != -1)
                    {
                        if (indexNull > 0 && indexNull < index || ( index == -1 && indexNull != -1 ) )
                        {
                            list.Add("NULL");
                            this.str = this.str.Substring(indexNull + 4);
                            goto nextPattern;
                        }
                        if (this.str[index + 1] == '0')
                        {
                            list.Add("");
                        }
                        else
                        {
                            string tmp = this.str.Substring(index + 1, num2 - (index + 1));

                            index = tmp.IndexOf(" ");
//                            while ((indexBegin < tmp.Length) && ( indexBegin > 0 && indexBegin < tmp.Length && tmp[indexBegin] == ' '))
//                            {
                                index++;
                                tmp = tmp.Substring(index);
//                            }

                            list.Add(tmp);
                        }
                        this.str = this.str.Substring(num2 + 2);
 nextPattern:                       
                        index = this.str.IndexOf("<");
                        num2 = this.str.IndexOf("/>");
                        indexNull = str.ToLower().IndexOf("null");
                    }
                    object[] objArray = list.ToArray();
                    string[] strArray = new string[objArray.Length];
                    int num3 = 0;
                    foreach (object obj2 in objArray)
                    {
                        strArray[num3++] = (string) obj2;
                    }
                    return strArray;
                }
            }
            return null;
        }

        public static string stringToGPBinary(string str, bool error)
        {
            error = false;
            string str2 = "";
            string str4 = str.Replace("[\r\n]+", "");
            string[] strArray = null;
            int index = str4.IndexOf("(");
            int num2 = str4.LastIndexOf(")");
            int length = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            str2 = str4.Substring(0, index + 1);
            while ((index != -1) && (num2 != -1))
            {
                string str3;
                if (num2 != (index + 1))
                {
                    str3 = trim(str4.Substring(index + 1, (num2 - index) - 1));
                    length = str3.IndexOf("\"");
                    num4 = str3.IndexOf("\"", (int) (length + 1));
                    num5 = str3.IndexOf(",");
                    num6 = str3.IndexOf(",", (int) (num5 + 1));
                    if (length != -1)
                    {
                        goto Label_04DC;
                    }
                    str3 = trim(str3);
                    if (str3.EndsWith(","))
                    {
                        str3 = str3.Substring(0, str3.Length - 1);
                    }
                    strArray = str3.Split(new char[] { ',' });
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        strArray[i] = trim(strArray[i]);
                        if (!string.IsNullOrEmpty(strArray[i]))
                        {
                            if (strArray[i].ToLower().Equals("null"))
                            {
                                str2 = str2 + strArray[i];
                            }
                            else
                            {
                                str2 = str2 + ToGPBinary(strArray[i]);
                            }
                            if (i != (strArray.Length - 1))
                            {
                                str2 = str2 + ", ";
                            }
                        }
                    }
                    index = -1;
                    num2 = -1;
                }
                break;
            Label_0176:
                if (length != -1)
                {
                    string str5;
                    if ((num5 != -1) && (length < num5))
                    {
                        num4 = str3.IndexOf("\"", (int) (length + 1));
                        str5 = trim(str3.Substring(length + 1, (num4 - 1) - length));
                        str2 = str2 + ToGPBinary(str5);
                        num6 = str3.IndexOf(",", (int) (num4 + 1));
                        if (num6 != -1)
                        {
                            str3 = str3.Substring(num6 + 1);
                        }
                        else if (num4 == -1)
                        {
                            break;
                        }
                    }
                    else if (num5 != -1)
                    {
                        str5 = str3.Substring(0, length);
                        strArray = trim(str5.Substring(0, str5.LastIndexOf(','))).Split(new char[] { ',' });
                        for (int j = 0; j < strArray.Length; j++)
                        {
                            strArray[j] = trim(strArray[j]);
                            if (!string.IsNullOrEmpty(strArray[j]))
                            {
                                if (strArray[j].ToLower().Equals("null"))
                                {
                                    str2 = str2 + strArray[j];
                                }
                                else
                                {
                                    str2 = str2 + ToGPBinary(strArray[j]);
                                }
                                if (j != (strArray.Length - 1))
                                {
                                    str2 = str2 + ", ";
                                }
                            }
                        }
                        str3 = str3.Substring(length);
                    }
                    str3 = trim(str3);
                    if (str3.StartsWith("\"") && str3.EndsWith("\""))
                    {
                        int num9 = str3.LastIndexOf("\"");
                        if (str3.IndexOf("\"", 1) == num9)
                        {
                            str3 = trim(str3.Substring(1, num9 - 1));
                            if (!string.IsNullOrEmpty(str3))
                            {
                                if (!str2.EndsWith("("))
                                {
                                    str2 = str2 + ", " + ToGPBinary(str3);
                                }
                                else
                                {
                                    str2 = str2 + ToGPBinary(str3);
                                }
                            }
                            break;
                        }
                    }
                    length = str3.IndexOf("\"");
                    num4 = str3.IndexOf("\"", (int) (length + 1));
                    num5 = str3.IndexOf(",");
                    num6 = str3.IndexOf(",", (int) (num5 + 1));
                    if (((length != -1) && (num4 != -1)) || ((num5 != -1) && (num6 != -1)))
                    {
                        str2 = str2 + ", ";
                    }
                    else
                    {
                        str3 = trim(str3);
                        if ((((length == -1) && (num4 != -1)) || ((length != -1) && (num4 == -1))) || (((num5 == -1) && (num6 != -1)) || ((num5 != -1) && (num6 == -1))))
                        {
                            str2 = str2 + ToGPBinary(str3);
                            error = true;
                            break;
                        }
                        if ((num5 == -1) && (num6 == -1))
                        {
                            str2 = str2 + ", " + str3;
                            break;
                        }
                        if (!string.IsNullOrEmpty(str3))
                        {
                            str2 = str2 + ", " + ToGPBinary(str3);
                        }
                    }
                }
                else
                {
                    strArray = str3.Split(new char[] { ',' });
                    for (int k = 0; k < strArray.Length; k++)
                    {
                        strArray[k] = trim(strArray[k]);
                        if (strArray[k].ToLower().Equals("NULL"))
                        {
                            str2 = str2 + strArray[k];
                        }
                        else
                        {
                            str2 = str2 + ToGPBinary(strArray[k]);
                        }
                        if (k != (strArray.Length - 1))
                        {
                            str2 = str2 + ", ";
                        }
                    }
                    str3 = trim(str3);
                    if ((strArray.Length == 0) && !string.IsNullOrEmpty(str3))
                    {
                        str2 = str2 + ToGPBinary(str3);
                    }
                    break;
                }
            Label_04DC:
                if ((length != -1) || (num5 != -1))
                {
                    goto Label_0176;
                }
                break;
            }
            return (str2 + "); ");
        }

        public void ToGPBinary()
        {
            while (str.StartsWith(" ") || str.EndsWith(" "))
            {
                str = str.Trim();
            }
            Encoding encoding = Encoding.GetEncoding(28605); //"ISO-8859-15"
            this.str = string.Concat(new object[] { "<", encoding.GetByteCount(str), " ", this.str, "/>" });
        }

        public static string ToGPBinary(string str)
        {
            while (str.StartsWith(" ") || str.EndsWith(" "))
            {
                str = str.Trim();
            }
            Encoding encoding = Encoding.GetEncoding(28605); // "ISO-8859-1"
            byte[] bytes = encoding.GetBytes(str);
            return string.Concat(new object[] { "<", encoding.GetByteCount(str), " ", encoding.GetString(bytes), "/>" });
        }

        public void ToProtocol()
        {
            Encoding encoding = Encoding.GetEncoding(28605); // "ISO-8859-1"
            byte[] bytes = encoding.GetBytes(str);
            int length = System.Text.Encoding.GetEncoding(28605).GetByteCount(str);
            int num2 = length.ToString().Length;
            this.str = string.Concat(new object[] { "#", num2, "#", length, " ", encoding.GetString(bytes) });
        }

        public string tostring()
        {
            return this.str;
        }

        public string Tostring()
        {
            return this.str;
        }

        public static string trim(string s)
        {
            string str = s;
            while (str.StartsWith(" ") || str.EndsWith(" "))
            {
                str = str.Trim();
            }
            return str;
        }

        public static string NormalizeString(string _str, ref Int32 buffersize)
        {
            Encoding encoding = Encoding.GetEncoding(28605); // ISO-8859-15
            Encoding SrcEncoding = Encoding.GetEncoding("UTF-8");
            byte[] srcbytes = SrcEncoding.GetBytes(_str);
            string ascii_str = SrcEncoding.GetString(srcbytes);
            byte[] isobytes = Encoding.Convert(SrcEncoding, encoding, srcbytes );
            byte[] bytes = encoding.GetBytes(_str);
            List<byte> l = new List<byte>();
            /*
             * ISO 8859-15  attention !!!!!
             * important : on remplace "oe" en "o""e" idem pour "ae"
             */
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0xBD) // oe
                {
                    l.Add((byte)'o');
                    l.Add((byte)'e');
                }
                else if (bytes[i] == 0xBC) // OE
                {
                    l.Add((byte)'O');
                    l.Add((byte)'E');
                }
                else if (bytes[i] == 0xE6) // ae
                {
                    l.Add((byte)'a');
                    l.Add((byte)'e');
                }
                else if (bytes[i] == 0xC6) // AE
                {
                    l.Add((byte)'A');
                    l.Add((byte)'E');
                }
                else
                    l.Add(bytes[i]);

                

            }

           string str = encoding.GetString( l.ToArray() );
            buffersize = encoding.GetByteCount(str);

            return str;
        }

        public static string NormalizeWikiString(string s, ref Int32 buffersize)
        {
            Encoding encoding = Encoding.GetEncoding(28605); // ISO-8859-15
            Encoding SrcEncoding = Encoding.GetEncoding("UTF-8");
            byte[] utf8bytes = SrcEncoding.GetBytes(s);
            byte[] bytes = Encoding.Convert(SrcEncoding, encoding, utf8bytes);
            List<byte> l = new List<byte>();
            /*
             * ISO 8859-15  attention !!!!!
             * important : on remplace "oe" en "o""e" idem pour "ae"
             */
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0xBD) // oe
                {
                    l.Add((byte)'o');
                    l.Add((byte)'e');
                }
                else if (bytes[i] == 0xBC) // OE
                {
                    l.Add((byte)'O');
                    l.Add((byte)'E');
                }
                else if (bytes[i] == 0xE6) // ae
                {
                    l.Add((byte)'a');
                    l.Add((byte)'e');
                }
                else if (bytes[i] == 0xC6) // AE
                {
                    l.Add((byte)'A');
                    l.Add((byte)'E');
                }
                else
                    l.Add(bytes[i]);
            }

            string str = encoding.GetString(l.ToArray());
            buffersize = encoding.GetByteCount(str);
            return str;
        }

        public static String cleantext(String s)
        {
            //s = suppBaliseFast("{|","|}", s);
            //s = suppBaliseFast("{{","}}", s);
            // s = s.replace("#REDIRECTION","");
            //   s = s.replace("#REDIRECT","");
            //( "(<my:string>).*?(</my:string>)" , "$1whatever$2" );
            s = s.Replace("\n", "");

            s = s.Replace("{|", "<clean>");
            s = s.Replace("|}", "<cleanend>");

            s = s.Replace("{{", "<clean2>");
            s = s.Replace("}}", "<cleanend2>");

            string pattern = "(&lt;).*?(&gt;)";
            s = System.Text.RegularExpressions.Regex.Replace(s,pattern, "$1 $2");
            pattern = "(<clean>).*?(<cleanend>)";
            s = System.Text.RegularExpressions.Regex.Replace(s, pattern, "$1 $2");
            pattern = "(<clean2>).*?(<cleanend2>)";
            s = System.Text.RegularExpressions.Regex.Replace(s, pattern, "$1 $2");
            //s = s.replaceAll("(\\{|).*?(\\|})", "$1 $2");
            s = s.Replace("<clean>", " ");
            s = s.Replace("<cleanend>", " ");
            s = s.Replace("<clean2>", " ");
            s = s.Replace("<cleanend2>", " ");
            //s = s.replaceAll("({|).*?(|})", "$1 $2");

            //s = s.replaceAll("({).*?(})", "$1 $2");
            //s = s.replaceAll("({).*?(\\})", "$1+$2");
            // s = s.replaceAll("(\\{\\{).*?(\\}\\})", "$1 $2");
            pattern = "(&lt;).*?(&gt;)";
            s = System.Text.RegularExpressions.Regex.Replace(s, pattern, " ");
            //s = s.replaceAll("(\\{).*?(\\})", " ");

            //s = s.replaceAll("(\\{|).*?(|\\})", " ");
            //s = s.replaceAll("({{).*?(}})", " ");
            //s = s.replaceAll("(&quot;).*?(&quot;)", "beurkbeurk");
            s = s.Replace("'''", " ");
            s = s.Replace("’", "'");

            //ÀàÁáÂâÃãÄäÅåÆæÇçÈèÉéÊêËëÌìÍíÎîÏïÐðÑñÒòÓóÔôÕõÖöØøÙùÚúÛûÜ üÝýÞþ                


            s = s.Replace("image:", " ");
            s = s.Replace("Image:", " ");
            s = s.Replace("thumb ", " ");
            s = s.Replace("class=", " ");
            s = s.Replace("style=", " ");
            s = s.Replace("align=", " ");
            s = s.Replace("id=", " ");
            s = s.Replace("class=", " ");
            s = s.Replace("amp;lt;", "");
            s = s.Replace("|", " ");
            s = s.Replace("*", " ");
            s = s.Replace("[[", " ");
            s = s.Replace("]]", " ");
            s = s.Replace("]", " ");
            s = s.Replace("[", " ");
            s = s.Replace("????", " ");
            s = s.Replace("???", " ");
            s = s.Replace("??", " ");
            s = s.Replace(" #39;", " ");
            s = s.Replace(" #91;", " ");
            s = s.Replace(" amp;gt", " ");
            s = s.Replace("&amp;", " ");
            s = s.Replace("< revision>", " ");
            s = s.Replace("&quot;", " ");
            s = s.Replace("</text>", " ");
            s = s.Replace("Modéle:", " ");
            s = s.Replace("Catégorie:", " ");
            s = s.Replace("Wikipédia:", " ");
            s = s.Replace("== Définition ==", " ");
            s = s.Replace("{{Infobox", " ");
            s = s.Replace("{{homonymie", " ");
            s = s.Replace("{{Homonymie", " ");
            s = s.Replace("Lumière sur", " ");
            s = s.Replace("Voir homonymes", " ");
            s = s.Replace("voir homonymes", " ");
            s = s.Replace("Voir famille", " ");
            s = s.Replace("Taxobox Début", " ");
            s = s.Replace("Taxobox Fin", " ");
            s = s.Replace("Taxobox ", " ");
            s = s.Replace("Fichier:", " ");
            s = s.Replace("{{", " ");
            s = s.Replace("}}", " ");
            s = s.Replace("/", " ");
            s = s.Replace("\\", " ");
            s = KMString.trim(s);
            return s;
        }
        public static string GetUnicodeString( string input)
        {
            string output = "";
            for (var i = 0; i < input.Length; i += char.IsSurrogatePair(input, i) ? 2 : 1)
            {
                int codepoint = char.ConvertToUtf32(input, i);

                output += "U+"+codepoint.ToString("X");
            }
            return output;
        }

        /*
         * 
         * en entree une chaine unicode du type U+XXU+XX...
         * 
         * en sortie la chaine de caracteres
         */
        public static string UnicodeToString(string input)
        {
            string output = "";
            int i = 0;
            int length =  input.Length;
            byte c;
            string unicode = " ";
            c = (byte)input[0];
            while ( i < length )
            {
                while ( c == 'U'  && i < length )
                {
                    c = (byte) input[++i];
                }
                if (c != '+')
                {
                    return null;
                }
                i++;
                c = (byte)input[i];
                while ( c != 'U' && i < length )
                {
                    unicode += (char) c;
                    i++;
                    if ( i == length )
                    {
                        break;
                    }
                    c = (byte)input[i];

                }
                // unicode represente le code en hexadecimal !!!!
                output += char.ConvertFromUtf32( int.Parse( unicode,System.Globalization.NumberStyles.HexNumber) );
                unicode = "";
            }
            return output;
        }


       public static void RawStringToGPBinaryString(string inStr, ref string outStr)
        {
            int idx = 0;
            //inStr = Encoding.UTF32.GetString(Encoding.Unicode.GetBytes(inStr));
            string command, p="";
            System.Char c;
            outStr = "";
            int start = 0, end  = 0;
            inStr = inStr.Replace("\n", " ");
            inStr = inStr.Replace("\r", " ");

           while (idx < inStr.Length )
           {
                while (idx < inStr.Length && (c = inStr[idx] ) == ' ')
                {
                   idx++;
                }
                if (idx >= inStr.Length)
                    break;
                start = idx;
                while ( idx < inStr.Length && (c = inStr[idx] ) != '(' )
                {
                    idx++;
                }
                if (idx >= inStr.Length - 1)
                    idx--;
                end = idx;
                command = inStr.Substring(start, end-start + 1);
                outStr += command;
                idx++;
                while ( idx < inStr.Length && inStr[idx] == ' ' )
                {
                    idx++;
                }
               // les paramètres
                while (idx < inStr.Length)
                {
                    if (inStr[idx] == '"') // on commence un texte
                    {
                        idx++;
                        p = "";
                        while (idx < inStr.Length && (c = inStr[idx]) != '"')
                        {
                            p += c;
                            idx++;
                        }
                        if (idx >= inStr.Length - 1)
                            break;
                        p = GetUnicodeString(p);
                        p = KMString.ToGPBinary(p);
                        idx++;
                        outStr += p; // "\"" + p + "\"";

                        while (idx < inStr.Length && (c = inStr[idx]) == ' ')
                        {
                            idx++;
                        }
                        // premier cas on a terminé la ligne de commande
                        if ((c = inStr[idx]) == ')')
                        {
                            outStr += "); ";
                            while (idx < inStr.Length && (c = inStr[idx]) != ';') // on cherche la fin de la ligne
                            {
                                idx++;
                            }
                            if (idx >= inStr.Length - 1)
                                break;
                        }
                        outStr += ", ";
                    }
                    //sinon on process le prochain paramètre
                    p = "";
                    while ( idx < inStr.Length && (c = inStr[idx]) != ',')
                    {
                        if ( c == ')' )
                        {

                            break;
                        }
                        p += c;
                        idx++;
                    }
                    if ( !string.IsNullOrEmpty(p) )
                    {
                        p = KMString.ToGPBinary(p);
                    }
                    if ( (c = inStr[idx]) == ')' )
                    {
                        outStr += p;
                        idx--;
                    }
                    else
                    {
                        if ( !string.IsNullOrEmpty(p) )
                            outStr += p + ", ";
                    }
                    idx++;
                    if (idx >= inStr.Length - 1)
                        break;
                    while (idx < inStr.Length && inStr[idx] == ' ' )
                    {
                        idx++;
                    }
                    if (idx >= inStr.Length - 1)
                        break;
                    if (inStr[idx] == ')')
                    {
                        outStr += "); ";
                        idx++;
                    }
                    if (idx >= inStr.Length - 1)
                        break;
                    while (idx < inStr.Length && inStr[idx] == ' ')
                    {
                        idx++;
                    }
                    if (idx >= inStr.Length - 1)
                        break;
                    if (inStr[idx] == ';')
                    {
                        //outStr += "; ";
                        idx++;
                        break;
                    }
                }
                while (idx < inStr.Length && inStr[idx] == ' ')
                {
                    idx++;
                }
                if (idx >= inStr.Length - 1)
                    break;
           }
        }

    }
}

