using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace EVE_API.API
{
    [XmlInclude(typeof(Account.Characters))]
    [XmlInclude(typeof(EVE.ErrorList))]
    [XmlInclude(typeof(ServerStatus))]
    class API_Base
    {
        private string hashedUrl;
        private DateTime currentTime;
        private DateTime cachedUntil;
        private DateTime currentTimeLocal;
        private DateTime cachedUntilLocal;
        private XmlDocument responseXml;
        private bool fromCache = false;

        /// <summary>
        /// This is a hashed version of the url that is sent to CCP to request the file
        /// </summary>
        [XmlElement]
        public string HashedUrl { get; set; }

        /// <summary>
        /// This is the current time that CCP sends to us on the file.
        /// </summary>
        [XmlElement]
        public DateTime CurrentTime { get; set; }

        /// <summary>
        /// This is the time that the file says it is cacheable till in CCP time.  We use
        /// the currentTime that is sent along with the file to calculate how long this
        /// is till.
        /// </summary>
        [XmlElement]
        public DateTime CachedUntil { get; set; }

        /// <summary>
        /// The raw xml response from the api
        /// </summary>
        [XmlElement]
        public XmlDocument ResponseXml { get; set; }

        /// <summary>
        /// True if this data came from the cache
        /// False if this data came directly from the eve api
        /// </summary>
        [XmlElement]
        public bool FromCache { get; set; }

        /// <summary>
        /// This parses out all of the elements that are common to each one of the xml files,
        /// which mainly includes dates, or errors if they exist.
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public void ParseCommonElements(XmlDocument xmlDoc)
        {
            this.ResponseXml = xmlDoc;
            string currentTimeCCPString = xmlDoc.SelectSingleNode("/eveapi/currentTime").InnerText;
            string cachedUntilCCPString = xmlDoc.SelectSingleNode("/eveapi/cachedUntil").InnerText;

            XmlNodeList errors = xmlDoc.GetElementsByTagName("error");
            if (errors.Count > 0)
            {
                string code = errors[0].Attributes["code"].InnerText;
                string message = errors[0].InnerText;
                throw new ApiResponseErrorException(code, message);
            }
        }
    }
}
