using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace EVE_API
{
    class Network
    {
        /// <summary>
        /// This function takes in a url, and will download the data from that
        /// URL and create an xml document from it
        /// </summary>
        /// <param name="url">The url of the XML file to retrieve</param>
        /// <returns></returns>
        public static Object GetResponse(string url)
        {
            string data = null;

            data = getCache(url);
            if (data == null) // Cache is empty, try to pull from EVE and store info in it
            {
                data = getEVE(url);

                saveCache(url, data);
            }
            else // Cache was not empty, is it old data? If so, try to pull from EVE
            {
                // Perform a regular expression on the string. If Cache Until is earlier than now
                // , pull from EVE (insert a try, catch so that if it fails you still used the currently cached data)
                // and save. If Cache Until is later than now, serve it up.
                Regex exp = new Regex("<cachedUntil>|</cachedUntil>");
                string[] cachedUntilstring = exp.Split(data);

                DateTime cachedUntil = DateTime.Parse(cachedUntilstring[1]);
                cachedUntil = DateTime.SpecifyKind(cachedUntil, DateTimeKind.Utc);
                DateTime utcNow = DateTime.UtcNow;
                int result = DateTime.Compare(cachedUntil, utcNow);

                // Old Cache, fetch new data if possible
                if (result < 0 || result == 0)
                {
                    string tempData = data;
                    try
                    {
                        data = getEVE(url);
                        saveCache(url, data);
                    }
                    catch (WebException)
                    {
                        data = tempData;
                    }
                    catch (ApiResponseErrorException)
                    {
                        data = tempData;
                    }
                }
                // Cache must still be used, serve it up!
            }

            if (data.Contains("<error code=") && !url.Contains(Constants.ErrorList))
            {
                // Returned an error and was not the error list, throw exception with error
                throw new ApiResponseErrorException(new Error(data));
            }else if (url.Contains(Constants.ErrorList))
            {
                return new ErrorList(data);
            }
            else if (url.Contains(Constants.ServerStatus))
            {
                return new ServerStatus(data);
            }
            else if (url.Contains(Constants.CharacterList))
            {
                return new Characters(data);
            }

            return data;
        }

        private static string getCache(string url)
        {
            ICacheManager cache = CacheFactory.GetCacheManager();

            return (string)cache.GetData(url);
        }

        private static string getEVE(string url)
        {
            WebClient wc = new WebClient();
            Stream stream = null;
            String data = null;
            wc.Headers.Add("user-agent", Network.eveNetworkClientSettings.userAgent);
            if (Network.eveNetworkClientSettings.proxy != null)
            {
                wc.Proxy = Network.eveNetworkClientSettings.proxy;
            }

            try
            {
                if(url.Contains(Constants.ImageFullURL))
                    stream = wc.OpenRead(url);
                else
                    data = wc.DownloadString(url);
            }
            catch (WebException e)
            {
                throw new WebException(e.ToString());
            }
            finally
            {
                if(stream != null)
                    stream.Close();
            }

            if (stream != null)
                return null;
                // figure out what xml was returned and return that, else return an image
            if (data.Contains("<error code=") && !url.Contains(Constants.ErrorList))
            {
                // Returned an error and was not the error list, throw exception with error
                throw new ApiResponseErrorException(new Error(data));
            }
            else
            {
                return data;
            }
        }

        private static void saveCache(string url, string data)
        {
            ICacheManager cache = CacheFactory.GetCacheManager();

            cache.Add(url, data, CacheItemPriority.NotRemovable, null, null);
        }

        /// <summary>
        /// All advanced network settings go here
        /// </summary>
        public class eveNetworkClientSettings
        {
            /// <summary>
            /// The default proxy is null, meaning no proxy in use
            /// </summary>
            public static WebProxy proxy = null;

            /// <summary>
            /// The base userAgent string to be used by the program
            /// </summary>
            public static string userAgent = "EveApi/1";
        }
    }
}
