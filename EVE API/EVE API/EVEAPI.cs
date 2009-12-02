using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;

namespace EVE_API
{
    public class EVEAPI
    {
        /// <summary>
        /// Sets a proxy server for the connection to run through
        /// </summary>
        /// <param name="url">The url for the proxy server</param>
        /// <param name="port">The port for the proxy server</param>
        /// <returns></returns>
        public static void SetProxy(string url, int port)
        {
            Network.eveNetworkClientSettings.proxy = new WebProxy(url, port);
        }

        /// <summary>
        /// Sets a proxy server for the connection to run through
        /// </summary>
        /// <param name="url">The url for the proxy server</param>
        /// <param name="port">The port for the proxy server</param>
        /// <param name="username">The username for the proxy server</param>
        /// <param name="password">The password for the proxy server</param>
        /// <returns></returns>
        public static void SetProxy(string url, int port, string username, string password)
        {
            Network.eveNetworkClientSettings.proxy = new WebProxy(url, port);
            Network.eveNetworkClientSettings.proxy.Credentials = new NetworkCredential(username, password);
        }

        /// <summary>
        /// Unsets the proxy server
        /// </summary>
        /// <returns></returns>
        public static void UnsetProxy()
        {
            Network.eveNetworkClientSettings.proxy = null;
        }

        /// <summary>
        /// Allows modification of the user agent to add the program's name into the request for tracking
        /// </summary>
        /// <param name="userAgent">The userAgent string to add to all outgoing webrequests</param>
        /// <returns></returns>
        public static void SetUserAgent(string userAgent)
        {
            Network.eveNetworkClientSettings.userAgent = "EveApi/1 (" + userAgent + ")";
        }

        /// <summary>
        /// Returns the server status of Tranquility
        /// </summary>
        /// <returns></returns>
        public static ServerStatus GetServerStatus()
        {
            string url = String.Format("{0}{1}", Constants.ApiPrefix, Constants.ServerStatus);

            return (ServerStatus)Network.GetResponse(url);
        }

        /// <summary>
        /// Returns a list of error codes that can be returned by the EVE API servers
        /// </summary>
        /// <returns></returns>
        public static ErrorList GetErrorList()
        {
            return GetErrorList(false);
        }

        /// <summary>
        /// Returns a list of error codes that can be returned by the EVE API servers
        /// </summary>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static ErrorList GetErrorList(bool ignoreCacheUntil)
        {
            string url = String.Format("{0}{1}", Constants.ApiPrefix, Constants.ErrorList);

            return (ErrorList)Network.GetResponse(url);
        }

        /// <summary>
        /// Returns a list of all characters on an account
        /// </summary>
        /// <param name="userId">userId of the account for authentication</param>
        /// <param name="apiKey">limited or full access api key of account</param>
        /// <returns></returns>
        public static Characters GetAccountCharacters(int userId, string apiKey)
        {
            return GetAccountCharacters(userId, apiKey, false);
        }

        /// <summary>
        /// Returns a list of all characters on an account
        /// </summary>
        /// <param name="userId">userId of the account for authentication</param>
        /// <param name="apiKey">limited or full access api key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static Characters GetAccountCharacters(int userId, string apiKey, bool ignoreCacheUntil)
        {
            string url = String.Format("{0}{1}?userID={2}&apiKey={3}", Constants.ApiPrefix, Constants.CharacterList, userId, apiKey);

            return (Characters)Network.GetResponse(url);
        }
    }
         
    /// <summary>
    /// Raised when an error response is received from an eve api request
    /// </summary>
    public class ApiResponseErrorException : Exception
    {
        /// <summary>
        /// The error code
        /// </summary>
        public Error error;

        /// <summary>
        /// Sets the current error code to the code recieved
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public ApiResponseErrorException(Error error)
        {
            this.error = error;
        }
    }

    /// <summary>
    /// Character portrait size
    /// </summary>
    public enum PortraitSize
    {
        /// <summary>
        /// A small portrait, 64x64 pixels
        /// </summary>
        Small,
        /// <summary>
        /// A large portrait, 256x256 pixels
        /// </summary>
        Large
    }
}
