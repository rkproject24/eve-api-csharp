using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
using EVE_API.API;
using EVE_API.API.EVE;
using EVE_API.API.Account;
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
        public static API_Base GetResponse(string url)
        {
            API_Base data = null;

            data = getCache(url);
            if (data == null)
            {
                data = getEVE(url);

                saveCache(url, data);

                return data;
            }
            else
            {
                if (data.CachedUntil < DateTime.UtcNow)
                {
                    data = getEVE(url);

                    saveCache(url, data);
                }

                return data;
            }
        }

        private static API_Base getCache(string url)
        {
            ICacheManager cache = CacheFactory.GetCacheManager();

            if (url.Contains(Constants.ServerStatus))
                return (ServerStatus)cache.GetData(url);
            else if (url.Contains(Constants.ErrorList))
                return (ErrorList)cache.GetData(url);
            else
                return (Characters)cache.GetData(url);
        }

        private static API_Base getEVE(string url)
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
                if(url.Contains(Constants.ServerStatus))
                    data = wc.DownloadString(url);
                else if(url.Contains(Constants.ErrorList))
                    data = wc.DownloadString(url);
                else
                    data = wc.DownloadString(url);
            }
            catch (WebException e)
            {
                throw new WebException(e.ToString());
            }
            finally
            {
                stream.Close();
            }

            if (stream != null)
                return null;
                // figure out what xml was returned and return that, else return an image

            if (url.Contains(Constants.ServerStatus))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ServerStatus));
                return serializer.Deserialize(new StringReader(data)) as ServerStatus;
            }
            else if (url.Contains(Constants.ErrorList))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ErrorList));
                return serializer.Deserialize(new StringReader(data)) as ErrorList;
            }
            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Characters));
                return serializer.Deserialize(new StringReader(data)) as Characters;
            }
        }

        private static void saveCache(string url, API_Base data)
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
