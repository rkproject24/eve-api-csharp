using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace EVE_API.API
{
    [XmlInclude(typeof(Account.Characters))]
    [XmlInclude(typeof(Character.AccountBalance))]
    [XmlInclude(typeof(Character.AssetList))]
    [XmlInclude(typeof(Character.CharacterSheet))]
    [XmlInclude(typeof(Character.FacWarStats))]
    [XmlInclude(typeof(Character.IndustryJobs))]
    [XmlInclude(typeof(Character.Killlog))]
    [XmlInclude(typeof(Character.MarketOrders))]
    [XmlInclude(typeof(Character.Medals))]
    [XmlInclude(typeof(Character.SkillInTraining))]
    [XmlInclude(typeof(Character.SkillQueue))]
    [XmlInclude(typeof(Character.Standings))]
    [XmlInclude(typeof(Character.WalletJournal))]
    [XmlInclude(typeof(Character.WalletTransactions))]
    [XmlInclude(typeof(Corporation.AccountBalance))]
    [XmlInclude(typeof(Corporation.AssetList))]
    [XmlInclude(typeof(Corporation.ContainerLog))]
    [XmlInclude(typeof(Corporation.CorporationSheet))]
    [XmlInclude(typeof(Corporation.FacWarStats))]
    [XmlInclude(typeof(Corporation.IndustryJobs))]
    [XmlInclude(typeof(Corporation.Killlog))]
    [XmlInclude(typeof(Corporation.MarketOrders))]
    [XmlInclude(typeof(Corporation.Medals))]
    [XmlInclude(typeof(Corporation.MemberMedals))]
    [XmlInclude(typeof(Corporation.MemberSecurity))]
    [XmlInclude(typeof(Corporation.MemberSecurityLog))]
    [XmlInclude(typeof(Corporation.MemberTracking))]
    [XmlInclude(typeof(Corporation.Shareholders))]
    [XmlInclude(typeof(Corporation.Standings))]
    [XmlInclude(typeof(Corporation.StarbaseDetail))]
    [XmlInclude(typeof(Corporation.StarbaseList))]
    [XmlInclude(typeof(Corporation.Titles))]
    [XmlInclude(typeof(Corporation.WalletJournal))]
    [XmlInclude(typeof(Corporation.WalletTransactions))]
    [XmlInclude(typeof(EVE.AllianceList))]
    [XmlInclude(typeof(EVE.CertificateTree))]
    [XmlInclude(typeof(EVE.CharacterID))]
    [XmlInclude(typeof(EVE.CharacterName))]
    [XmlInclude(typeof(EVE.ConquerableStationList))]
    [XmlInclude(typeof(EVE.ErrorList))]
    [XmlInclude(typeof(EVE.FacWarTopStats))]
    [XmlInclude(typeof(EVE.RefTypes))]
    [XmlInclude(typeof(EVE.SkillTree))]
    [XmlInclude(typeof(Map.FacWarSystems))]
    [XmlInclude(typeof(Map.Jumps))]
    [XmlInclude(typeof(Map.Kills))]
    [XmlInclude(typeof(Map.Sovereignty))]
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
        /// This is the current time on the local machine.
        /// </summary>
        [XmlElement]
        public DateTime CurrentTimeLocal { get; set; }

        /// <summary>
        /// This is what time the file should be cached to according to the local
        /// clock.  A timespan is created from the eve time, and added to CurrentTimeLocal
        /// </summary>
        [XmlElement]
        public DateTime CachedUntilLocal { get; set; }

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

            this.CurrentTime = TimeUtility.ConvertCCPTimeStringToDateTimeUTC(currentTimeCCPString);
            this.CachedUntil = TimeUtility.ConvertCCPTimeStringToDateTimeUTC(cachedUntilCCPString);

            this.CurrentTimeLocal = TimeUtility.ConvertCCPToLocalTime(this.CurrentTime);
            this.CachedUntilLocal = TimeUtility.ConvertCCPToLocalTime(this.CachedUntil);

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
