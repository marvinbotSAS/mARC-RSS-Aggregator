using RSSRTReader.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace RSSRTReader
{
    public class Feed
    {


            public string Title 
            { 
                get { return this.Title; }
                set { Title = value; }
            }
            public string URL
            { 
                get { return this.URL; }
                set { URL = value; }
            }

            public Feed()
            {
            }

            public Feed(string title, string url)
            {

                Title = title;
                URL = url;
            }

            private bool TitleIsValid(string title)
            {
                try
                {
                    return Regex.IsMatch(title, @"^.+$");
                }
                catch (ArgumentNullException)
                {
                    Logger.Instance.Log("Feed", "feed title is null.");
                    return false;
                }
                catch (ArgumentOutOfRangeException)
                {
                    Logger.Instance.Log("Feed", "feed title is out of range for regex matching");
                    return false;
                }
            }

            /// <summary>
            /// This method will check a url to see that it does not return server or protocol errors
            /// </summary>
            /// <param name="url">The path to check</param>
            /// <returns>If <paramref name="url"/> is valid or not</returns>
            private bool URLIsValid(string url)
            {
                try
                {
                    HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                    request.Timeout = 5000; //set the timeout to 5 seconds to keep the user from waiting too long for the page to load
                    request.Method = "HEAD"; //Get only the header information -- no need to download any content

                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                    int statusCode = (int)response.StatusCode;
                    if (statusCode >= 100 && statusCode < 400) //Good requests
                    {
                        return true;
                    }
                    else if (statusCode >= 500 && statusCode <= 510) //Server Errors
                    {
                        Logger.Instance.Log("InputMessageBox",
                            String.Format("The remote server has thrown an internal error. Url is not valid: {0}", url));
                        return false;
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError) //400 errors
                    {
                        return false;
                    }
                    else
                    {
                        Logger.Instance.Log("InputMessageBox",
                            String.Format("Unhandled status [{0}] returned for url: {1}", ex.Status, url));
                    }
                }
                catch (Exception ex)
                {
                    Logger.Instance.Log("InputMessageBox", String.Format("Could not test url {0}.\n {1}", url, ex.Message));
                }
                return false;
            }
        }
    }

