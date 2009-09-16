using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using EVE_API.API;

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
        /// Returns a list of all characters on an account
        /// </summary>
        /// <param name="userId">userId of the account for authentication</param>
        /// <param name="apiKey">limited or full access api key of account</param>
        /// <returns></returns>
        public static CharacterList GetAccountCharacters(int userId, string apiKey)
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
        public static CharacterList GetAccountCharacters(int userId, string apiKey, bool ignoreCacheUntil)
        {
            string url = String.Format("{0}{1}?userID={2}&apiKey={3}", Constants.ApiPrefix, Constants.CharacterList, userId, apiKey);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as API.Account.Characters;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            API.Account.Characters characterList = API.Account.Characters.FromXmlDocument(xmlDoc);
            Cache.Set(url, characterList);

            return characterList;
        }

        /// <summary>
        /// Returns the ISK balance of a corporation or character
        /// </summary>
        /// <param name="accountBalanceType">retrieve balance for character or corporation</param>
        /// <param name="userId">user ID of account for authentication</param>
        /// <param name="characterId">
        /// For character balance: The character you are requesting data for
        /// For corporation balance: Character Id of a char with director/CEO access in the corp you want the balance for
        /// </param>
        /// <param name="fullApiKey">Full access api key of account</param>
        /// <returns></returns>
        public static AccountBalance GetAccountBalance(AccountBalanceType accountBalanceType, int userId, int characterId, string fullApiKey)
        {
            return GetAccountBalance(accountBalanceType, userId, characterId, fullApiKey, false);
        }

        /// <summary>
        /// Returns the ISK balance of a corporation or character
        /// </summary>
        /// <param name="accountBalanceType">retrieve balance for character or corporation</param>
        /// <param name="userId">user ID of account for authentication</param>
        /// <param name="characterId">
        /// For character balance: The character you are requesting data for
        /// For corporation balance: Character Id of a char with director/CEO access in the corp you want the balance for
        /// </param>
        /// <param name="fullApiKey">Full access api key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static AccountBalance GetAccountBalance(AccountBalanceType accountBalanceType, int userId, int characterId, string fullApiKey, bool ignoreCacheUntil)
        {
            string apiPath = string.Empty;
            switch (accountBalanceType)
            {
                case AccountBalanceType.Character:
                    apiPath = Constants.CharacterAccountBalance;
                    break;
                case AccountBalanceType.Corporation:
                    apiPath = Constants.CorpAccountBalance;
                    break;
            }

            string url = String.Format("{0}{1}?userID={2}&characterID={3}&apiKey={4}", Constants.ApiPrefix, apiPath, userId, characterId, fullApiKey);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as AccountBalance;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            AccountBalance accountBalance = AccountBalance.FromXmlDocument(xmlDoc);
            Cache.Set(url, accountBalance);

            return accountBalance;
        }

        /// <summary>
        /// Returns a list of starbases owned by a corporation
        /// </summary>
        /// <param name="userId">user Id of account for authentication</param>
        /// <param name="characterId">Character Id of a char with director/CEO access in the corp you want the starbases for</param>
        /// <param name="fullApiKey">Full access api key of account</param>
        /// <returns></returns>
        public static StarbaseList GetStarbaseList(int userId, int characterId, string fullApiKey)
        {
            return GetStarbaseList(userId, characterId, fullApiKey, false);
        }

        /// <summary>
        /// Returns a list of starbases owned by a corporation
        /// </summary>
        /// <param name="userId">user Id of account for authentication</param>
        /// <param name="characterId">Character Id of a char with director/CEO access in the corp you want the starbases for</param>
        /// <param name="fullApiKey">Full access api key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static StarbaseList GetStarbaseList(int userId, int characterId, string fullApiKey, bool ignoreCacheUntil)
        {
            string url = String.Format("{0}{1}?userID={2}&characterID={3}&apiKey={4}&version=2", Constants.ApiPrefix, Constants.StarbaseList, userId, characterId, fullApiKey);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as StarbaseList;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            StarbaseList starbaseList = StarbaseList.FromXmlDocument(xmlDoc);
            Cache.Set(url, starbaseList);

            return starbaseList;
        }

        /// <summary>
        /// Returns the character id and character name, given the one or the other
        /// </summary>
        /// <param name="charactername">character name string, use to look up character id</param>
        /// <returns></returns>
        public static CharacterIdName GetCharacterIdName(string charactername)
        {
            return GetCharacterIdName(charactername, false);
        }

        /// <summary>
        /// Returns the character id and character name, given the one or the other
        /// </summary>
        /// <param name="charactername">character name string, use to look up character id</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static CharacterIdName GetCharacterIdName(string charactername, bool ignoreCacheUntil)
        {
            string url = String.Format("{0}{1}?names={2}", Constants.ApiPrefix, Constants.CharacterIdName, charactername);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as CharacterIdName;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            CharacterIdName charId = CharacterIdName.FromXmlDocument(xmlDoc);
            Cache.Set(url, charId);

            return charId;
        }

        /// <summary>
        /// Returns the character id and character name, given the one or the other
        /// </summary>
        /// <param name="characterId">characterId used to look up character name</param>
        /// <returns></returns>
        public static CharacterIdName GetCharacterIdName(int characterId)
        {
            return GetCharacterIdName(characterId, false);
        }


        /// <summary>
        /// Returns the character id and character name, given the one or the other
        /// </summary>
        /// <param name="characterId">characterId used to look up character name</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static CharacterIdName GetCharacterIdName(int characterId, bool ignoreCacheUntil)
        {
            string url = String.Format("{0}{1}?ids={2}", Constants.ApiPrefix, Constants.CharacterIdName, characterId);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as CharacterIdName;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            CharacterIdName charId = CharacterIdName.FromXmlDocument(xmlDoc);
            Cache.Set(url, charId);

            return charId;
        }

        /// <summary>
        /// Returns the settings and fuel status of a starbase
        /// </summary>
        /// <param name="userId">user Id of account for authentication</param>
        /// <param name="characterId">Character Id of a char with director/CEO access in the corp that owns the starbase</param>
        /// <param name="fullApiKey">Full access api key of account</param>
        /// <param name="itemId">Item Id of the starbase as given in the starbase list</param>
        /// <returns></returns>
        public static StarbaseDetail GetStarbaseDetail(int userId, int characterId, string fullApiKey, int itemId)
        {
            return GetStarbaseDetail(userId, characterId, fullApiKey, itemId, false);
        }

        /// <summary>
        /// Returns the settings and fuel status of a starbase
        /// </summary>
        /// <param name="userId">user Id of account for authentication</param>
        /// <param name="characterId">Character Id of a char with director/CEO access in the corp that owns the starbase</param>
        /// <param name="fullApiKey">Full access api key of account</param>
        /// <param name="itemId">Item Id of the starbase as given in the starbase list</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static StarbaseDetail GetStarbaseDetail(int userId, int characterId, string fullApiKey, int itemId, bool ignoreCacheUntil)
        {
            string url = String.Format("{0}{1}?userID={2}&characterID={3}&apiKey={4}&itemID={5}&version=2", Constants.ApiPrefix, Constants.StarbaseDetails, userId, characterId, fullApiKey, itemId);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as StarbaseDetail;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            StarbaseDetail starbaseDetail = StarbaseDetail.FromXmlDocument(xmlDoc);
            Cache.Set(url, starbaseDetail);

            return starbaseDetail;
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
            string url = String.Format("{0}{1}?version=2", Constants.ApiPrefix, Constants.ErrorList);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as ErrorList;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            ErrorList errorList = ErrorList.FromXmlDocument(xmlDoc);
            Cache.Set(url, errorList);

            return errorList;
        }

        /// <summary>
        /// Returns a list of assets owned by a character or corporation.
        /// </summary>
        /// <param name="assetListType"><see cref="AssetListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <returns></returns>
        public static AssetList GetAssetList(AssetListType assetListType, int userId, int characterId, string fullApiKey)
        {
            return GetAssetList(assetListType, userId, characterId, fullApiKey, false);
        }

        /// <summary>
        /// Returns a list of assets owned by a character or corporation.
        /// </summary>
        /// <param name="assetListType"><see cref="AssetListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static AssetList GetAssetList(AssetListType assetListType, int userId, int characterId, string fullApiKey, bool ignoreCacheUntil)
        {
            string apiPath = string.Empty;
            switch (assetListType)
            {
                case AssetListType.Character:
                    apiPath = Constants.CharAssetList;
                    break;
                case AssetListType.Corporation:
                    apiPath = Constants.CorpAssetList;
                    break;
            }

            string url = String.Format("{0}{1}?userID={2}&characterID={3}&apiKey={4}&version=2", Constants.ApiPrefix, apiPath, userId, characterId, fullApiKey);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as AssetList;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            AssetList assetList = AssetList.FromXmlDocument(xmlDoc);
            Cache.Set(url, assetList);

            return assetList;
        }

        /// <summary>
        /// Retrieves the Kill Log for a character or corporation
        /// </summary>
        /// <param name="killLogType">KillLogType -- Character/Corporation which kill log do you want to retrieve</param>
        /// <param name="userId">User ID for authentication</param>
        /// <param name="characterId">The character your requesting data for</param>
        /// <param name="fullApiKey">Full Api Key for the account</param>
        /// <param name="beforeKillID">Returns the most recent kills before the specified Kill ID - used for scrolling back through the log</param>
        /// <returns>Kill Log object containing the array of kills</returns>
        public static KillLog GetKillLog(KillLogType killLogType, int userId, int characterId, string fullApiKey, int beforeKillID)
        {
            return GetKillLog(killLogType, userId, characterId, fullApiKey, beforeKillID, false);
        }

        /// <summary>
        /// Retrieves the Kill Log for a character or corporation
        /// </summary>
        /// <param name="killLogType">KillLogType -- Character/Corporation which kill log do you want to retrieve</param>
        /// <param name="userId">User ID for authentication</param>
        /// <param name="characterId">The character your requesting data for</param>
        /// <param name="fullApiKey">Full Api Key for the account</param>
        /// <param name="beforeKillID">Returns the most recent kills before the specified Kill ID - used for scrolling back through the log</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns>Kill Log object containing the array of kills</returns>
        public static KillLog GetKillLog(KillLogType killLogType, int userId, int characterId, string fullApiKey, int beforeKillID, bool ignoreCacheUntil)
        {
            string apiPath = string.Empty;
            switch (killLogType)
            {
                case KillLogType.Character:
                    apiPath = Constants.CharKillLog;
                    break;
                case KillLogType.Corporation:
                    apiPath = Constants.CorpKillLog;
                    break;
            }

            string url = string.Empty;
            if (beforeKillID == 0)
            {
                url = String.Format("{0}{1}?userID={2}&characterID={3}&apiKey={4}&version=2", Constants.ApiPrefix, apiPath, userId, characterId, fullApiKey);
            }
            else
            {
                url = String.Format("{0}{1}?userID={2}&characterID={3}&apiKey={4}&beforeKillID={5}&version=2", Constants.ApiPrefix, apiPath, userId, characterId, fullApiKey, beforeKillID);
            }

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as KillLog;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            KillLog killLog = KillLog.FromXmlDocument(xmlDoc);
            Cache.Set(url, killLog);

            return killLog;
        }

        /// <summary>
        /// Retrieves the Kill Log for a character or corporation
        /// </summary>
        /// <param name="killLogType">KillLogType -- Character/Corporation which kill log do you want to retrieve</param>
        /// <param name="userId">User ID for authentication</param>
        /// <param name="characterId">The character your requesting data for</param>
        /// <param name="fullApiKey">Full Api Key for the account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns>Kill Log object containing the array of kills</returns>
        public static KillLog GetKillLog(KillLogType killLogType, int userId, int characterId, string fullApiKey, bool ignoreCacheUntil)
        {
            return GetKillLog(killLogType, userId, characterId, fullApiKey, 0, ignoreCacheUntil);
        }

        /// <summary>
        /// Retrieves the Kill Log for a character or corporation
        /// </summary>
        /// <param name="killLogType">KillLogType -- Character/Corporation which kill log do you want to retrieve</param>
        /// <param name="userId">User ID for authentication</param>
        /// <param name="characterId">The character your requesting data for</param>
        /// <param name="fullApiKey">Full Api Key for the account</param>
        /// <returns>Kill Log object containing the array of kills</returns>
        public static KillLog GetKillLog(KillLogType killLogType, int userId, int characterId, string fullApiKey)
        {
            return GetKillLog(killLogType, userId, characterId, fullApiKey, 0, false);
        }

        /// <summary>
        /// Returns a list of industrial jobs owned by a character or corporation.
        /// </summary>
        /// <param name="industryJobListType"><see cref="IndustryJobListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <returns></returns>
        public static IndustryJobList GetIndustryJobList(IndustryJobListType industryJobListType, int userId, int characterId, string fullApiKey)
        {
            return GetIndustryJobList(industryJobListType, userId, characterId, fullApiKey, false);
        }

        /// <summary>
        /// Returns a list of industrial jobs owned by a character or corporation.
        /// </summary>
        /// <param name="industryJobListType"><see cref="IndustryJobListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static IndustryJobList GetIndustryJobList(IndustryJobListType industryJobListType, int userId, int characterId, string fullApiKey, bool ignoreCacheUntil)
        {
            string apiPath = string.Empty;
            switch (industryJobListType)
            {
                case IndustryJobListType.Character:
                    apiPath = Constants.CharIndustryJobs;
                    break;
                case IndustryJobListType.Corporation:
                    apiPath = Constants.CorpIndustryJobs;
                    break;
            }

            string url = String.Format("{0}{1}?userID={2}&characterID={3}&apiKey={4}&version=2", Constants.ApiPrefix, apiPath, userId, characterId, fullApiKey);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as IndustryJobList;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            IndustryJobList industryJobList = IndustryJobList.FromXmlDocument(xmlDoc);
            Cache.Set(url, industryJobList);

            return industryJobList;
        }

        /// <summary>
        /// Returns a list of journal entries owned by a character or corporation.
        /// </summary>
        /// <param name="journalEntriesType"><see cref="JournalEntryType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <returns></returns>
        public static JournalEntries GetJournalEntryList(JournalEntryType journalEntriesType, int userId, int characterId, string fullApiKey)
        {
            return GetJournalEntryList(journalEntriesType, userId, characterId, fullApiKey, 0, false);
        }

        /// <summary>
        /// Returns a list of journal entries owned by a character or corporation.
        /// </summary>
        /// <param name="journalEntriesType"><see cref="JournalEntryType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static JournalEntries GetJournalEntryList(JournalEntryType journalEntriesType, int userId, int characterId, string fullApiKey, bool ignoreCacheUntil)
        {
            return GetJournalEntryList(journalEntriesType, userId, characterId, fullApiKey, 0, ignoreCacheUntil);
        }

        /// <summary>
        /// Returns a list of journal entries owned by a character or corporation.
        /// </summary>
        /// <param name="journalEntriesType"><see cref="JournalEntryType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="beforeRefId">Retrieve entries after this refId</param>
        /// <returns></returns>
        public static JournalEntries GetJournalEntryList(JournalEntryType journalEntriesType, int userId, int characterId, string fullApiKey, int beforeRefId)
        {
            return GetJournalEntryList(journalEntriesType, userId, characterId, fullApiKey, beforeRefId, false);
        }

        /// <summary>
        /// Returns a list of journal entries owned by a character or corporation.
        /// </summary>
        /// <param name="journalEntriesType"><see cref="JournalEntryType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="beforeRefId">Retrieve entries after this refId</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static JournalEntries GetJournalEntryList(JournalEntryType journalEntriesType, int userId, int characterId, string fullApiKey, int beforeRefId, bool ignoreCacheUntil)
        {
            string apiPath = string.Empty;
            switch (journalEntriesType)
            {
                case JournalEntryType.Character:
                    apiPath = Constants.CharJournalEntries;
                    break;
                case JournalEntryType.Corporation:
                    apiPath = Constants.CorpJournalEntries;
                    break;
            }

            string url = String.Format("{0}{1}?userID={2}&characterID={3}&apiKey={4}", Constants.ApiPrefix, apiPath, userId, characterId, fullApiKey);
            if (beforeRefId != 0)
            {
                url += String.Format("&beforeRefID={0}", beforeRefId);
            }

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as JournalEntries;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            JournalEntries journalEntriesList = JournalEntries.FromXmlDocument(xmlDoc);
            Cache.Set(url, journalEntriesList);

            return journalEntriesList;
        }

        /// <summary>
        /// Returns a list of market orders owned by a character or corporation.
        /// </summary>
        /// <param name="marketOrdersListType"><see cref="MarketOrdersListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <returns></returns>
        public static MarketOrders GetMarketOrderList(MarketOrdersListType marketOrdersListType, int userId, int characterId, string fullApiKey)
        {
            return GetMarketOrderList(marketOrdersListType, userId, characterId, fullApiKey, false);
        }

        /// <summary>
        /// Returns a list of market orders owned by a character or corporation.
        /// </summary>
        /// <param name="marketOrdersListType"><see cref="MarketOrdersListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static MarketOrders GetMarketOrderList(MarketOrdersListType marketOrdersListType, int userId, int characterId, string fullApiKey, bool ignoreCacheUntil)
        {
            string apiPath = string.Empty;
            switch (marketOrdersListType)
            {
                case MarketOrdersListType.Character:
                    apiPath = Constants.CharMarketOrders;
                    break;
                case MarketOrdersListType.Corporation:
                    apiPath = Constants.CorpMarketOrders;
                    break;
            }

            string url = String.Format("{0}{1}?userID={2}&characterID={3}&apiKey={4}&version=2", Constants.ApiPrefix, apiPath, userId, characterId, fullApiKey);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as MarketOrders;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            MarketOrders marketOrderList = MarketOrders.FromXmlDocument(xmlDoc);
            Cache.Set(url, marketOrderList);

            return marketOrderList;
        }

        /// <summary>
        /// Returns a list of market transactions (wallet transactions) owned by a character or corporation.
        /// </summary>
        /// <param name="walletTransactionType"><see cref="WalletTransactionListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <returns></returns>
        public static WalletTransactions GetWalletTransactionsList(WalletTransactionListType walletTransactionType, int userId, int characterId, string fullApiKey)
        {
            return GetWalletTransactionsList(walletTransactionType, userId, characterId, fullApiKey, 0, false);
        }

        /// <summary>
        /// Returns a list of market transactions (wallet transactions) owned by a character or corporation.
        /// </summary>
        /// <param name="walletTransactionType"><see cref="WalletTransactionListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static WalletTransactions GetWalletTransactionsList(WalletTransactionListType walletTransactionType, int userId, int characterId, string fullApiKey, bool ignoreCacheUntil)
        {
            return GetWalletTransactionsList(walletTransactionType, userId, characterId, fullApiKey, 0, ignoreCacheUntil);
        }

        /// <summary>
        /// Returns a list of market transactions (wallet transactions) owned by a character or corporation.
        /// </summary>
        /// <param name="walletTransactionType"><see cref="WalletTransactionListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="beforeTransId">retrieve up to 1000 entries after this transactionId</param>
        /// <returns></returns>
        public static WalletTransactions GetWalletTransactionsList(WalletTransactionListType walletTransactionType, int userId, int characterId, string fullApiKey, int beforeTransId)
        {
            return GetWalletTransactionsList(walletTransactionType, userId, characterId, fullApiKey, beforeTransId, false);
        }

        /// <summary>
        /// Returns a list of market transactions (wallet transactions) owned by a character or corporation.
        /// </summary>
        /// <param name="walletTransactionType"><see cref="WalletTransactionListType" /></param>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="fullApiKey">Full access API key of account</param>
        /// <param name="beforeTransId">retrieve up to 1000 entries after this transactionId</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static WalletTransactions GetWalletTransactionsList(WalletTransactionListType walletTransactionType, int userId, int characterId, string fullApiKey, int beforeTransId, bool ignoreCacheUntil)
        {
            string apiPath = string.Empty;
            switch (walletTransactionType)
            {
                case WalletTransactionListType.Character:
                    apiPath = Constants.CharWalletTransactions;
                    break;
                case WalletTransactionListType.Corporation:
                    apiPath = Constants.CorpWalletTransactions;
                    break;
            }

            string url = String.Format("{0}{1}?userID={2}&characterID={3}&apiKey={4}&version=2", Constants.ApiPrefix, apiPath, userId, characterId, fullApiKey);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as WalletTransactions;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            WalletTransactions walletTransactionList = WalletTransactions.FromXmlDocument(xmlDoc);
            Cache.Set(url, walletTransactionList);

            return walletTransactionList;
        }

        /// <summary>
        /// Returns a list of RefTypes that are used by certain API Calls
        /// </summary>
        /// <returns></returns>
        public static RefTypes GetRefTypesList()
        {
            return GetRefTypesList(false);
        }

        /// <summary>
        /// Returns a list of RefTypes that are used by certain API Calls
        /// </summary>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static RefTypes GetRefTypesList(bool ignoreCacheUntil)
        {
            string url = String.Format("{0}{1}?version=2", Constants.ApiPrefix, Constants.RefTypesList);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as RefTypes;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            RefTypes refTypeList = RefTypes.FromXmlDocument(xmlDoc);
            Cache.Set(url, refTypeList);

            return refTypeList;
        }

        /// <summary>
        /// Returns a list solar systems that have more than 0 jumps with the jump count
        /// </summary>
        /// <returns></returns>
        public static MapJumps GetMapJumps()
        {
            return GetMapJumps(false);
        }

        /// <summary>
        /// Returns a list solar systems that have more than 0 jumps with the jump count
        /// </summary>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static MapJumps GetMapJumps(bool ignoreCacheUntil)
        {
            string url = String.Format("{0}{1}?version=2", Constants.ApiPrefix, Constants.MapJumps);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as MapJumps;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            MapJumps mapJumps = MapJumps.FromXmlDocument(xmlDoc);
            Cache.Set(url, mapJumps);

            return mapJumps;
        }

        /// <summary>
        /// Returns a list solar systems that have sovereignty
        /// </summary>
        /// <returns></returns>
        public static MapSovereignty GetMapSovereignty()
        {
            return GetMapSovereignty(false);
        }

        /// <summary>
        /// Returns a list solar systems that have sovereignty
        /// </summary>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static MapSovereignty GetMapSovereignty(bool ignoreCacheUntil)
        {
            string url = String.Format("{0}{1}?version=2", Constants.ApiPrefix, Constants.MapSoveignty);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as MapSovereignty;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            MapSovereignty mapSovereignty = MapSovereignty.FromXmlDocument(xmlDoc);
            Cache.Set(url, mapSovereignty);

            return mapSovereignty;
        }

        /// <summary>
        /// Returns a list kills in solar systems with more than 0 kills
        /// </summary>
        /// <returns></returns>
        public static MapKills GetMapKills()
        {
            return GetMapKills(false);
        }


        /// <summary>
        /// Returns a list kills in solar systems with more than 0 kills
        /// </summary>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static MapKills GetMapKills(bool ignoreCacheUntil)
        {
            string url = String.Format("{0}{1}?version=2", Constants.ApiPrefix, Constants.MapKills);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as MapKills;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            MapKills mapKills = MapKills.FromXmlDocument(xmlDoc);
            Cache.Set(url, mapKills);

            return mapKills;
        }

        /// <summary>
        /// Returns information on every member in the corporation. Information retrieved
        /// varies on your roles without within the corporation. Not valid for NPC corps.
        /// </summary>
        /// <param name="userId">user Id of account for authentication</param>
        /// <param name="characterId">Character Id of a char with director/CEO access in the corp that owns the starbase</param>
        /// <param name="fullApiKey">Full access api key of account</param>
        /// <returns></returns>
        public static MemberTracking GetMemberTracking(int userId, int characterId, string fullApiKey)
        {
            return GetMemberTracking(userId, characterId, fullApiKey, false);
        }

        /// <summary>
        /// Returns information on every member in the corporation. Information retrieved
        /// varies on your roles without within the corporation. Not valid for NPC corps.
        /// </summary>
        /// <param name="userId">user Id of account for authentication</param>
        /// <param name="characterId">Character Id of a char with director/CEO access in the corp that owns the starbase</param>
        /// <param name="fullApiKey">Full access api key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static MemberTracking GetMemberTracking(int userId, int characterId, string fullApiKey, bool ignoreCacheUntil)
        {
            string url = String.Format("{0}{1}?userID={2}&characterID={3}&apiKey={4}&version=1", Constants.ApiPrefix, Constants.MemberTracking, userId, characterId, fullApiKey);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as MemberTracking;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            MemberTracking memberTracking = MemberTracking.FromXmlDocument(xmlDoc);
            Cache.Set(url, memberTracking);

            return memberTracking;
        }

        /// <summary>
        /// Returns a detailed description of a character
        /// </summary>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="apiKey">Limited access API key of account</param>
        /// <returns></returns>
        public static CharacterSheet GetCharacterSheet(int userId, int characterId, string apiKey)
        {
            return GetCharacterSheet(userId, characterId, apiKey, false);
        }

        /// <summary>
        /// Returns a detailed description of a character
        /// </summary>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="apiKey">Limited access API key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static CharacterSheet GetCharacterSheet(int userId, int characterId, string apiKey, bool ignoreCacheUntil)
        {
            string url = String.Format("{0}{1}?userID={2}&characterID={3}&apiKey={4}", Constants.ApiPrefix, Constants.CharacterSheet, userId, characterId, apiKey);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as CharacterSheet;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            CharacterSheet characterSheet = CharacterSheet.FromXmlDocument(xmlDoc);
            Cache.Set(url, characterSheet);

            return characterSheet;
        }

        /// <summary>
        /// Returns a detailed description of a corporation
        /// </summary>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="apiKey">Limited access API key of account</param>
        /// <returns></returns>
        public static CorporationSheet GetCorporationSheet(int userId, int characterId, string apiKey)
        {
            return GetCorporationSheet(userId, characterId, apiKey, 0, false);
        }

        /// <summary>
        /// Returns a detailed description of a corporation
        /// </summary>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="apiKey">Limited access API key of account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static CorporationSheet GetCorporationSheet(int userId, int characterId, string apiKey, bool ignoreCacheUntil)
        {
            return GetCorporationSheet(userId, characterId, apiKey, 0, ignoreCacheUntil);
        }

        /// <summary>
        /// Returns a detailed description of a corporation
        /// </summary>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="apiKey">Limited access API key of account</param>
        /// <param name="corporationId">retrieve information on the corporation with this id</param>
        /// <returns></returns>
        public static CorporationSheet GetCorporationSheet(int userId, int characterId, string apiKey, int corporationId)
        {
            return GetCorporationSheet(userId, characterId, apiKey, corporationId, false);
        }

        /// <summary>
        /// Returns a detailed description of a corporation
        /// </summary>
        /// <param name="userId">userId of account for authentication</param>
        /// <param name="characterId">CharacterId of character for authentication</param>
        /// <param name="apiKey">Limited access API key of account</param>
        /// <param name="corporationId">retrieve information on the corporation with this id</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static CorporationSheet GetCorporationSheet(int userId, int characterId, string apiKey, int corporationId, bool ignoreCacheUntil)
        {
            string url = String.Format("{0}{1}?userID={2}&characterID={3}&apiKey={4}", Constants.ApiPrefix, Constants.CorporationSheet, userId, characterId, apiKey);

            if (corporationId != 0)
            {
                url = String.Format("{0}&corporationID={1}", url, corporationId);
            }

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as CorporationSheet;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            CorporationSheet corporationSheet = CorporationSheet.FromXmlDocument(xmlDoc);
            Cache.Set(url, corporationSheet);

            return corporationSheet;
        }

        /// <summary>
        /// Gets a list of conquerable stations from the api
        /// </summary>
        /// <returns></returns>
        public static ConquerableStationList GetConquerableStationList()
        {
            return GetConquerableStationList(false);
        }

        /// <summary>
        /// Gets a list of conquerable stations from the api
        /// </summary>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static ConquerableStationList GetConquerableStationList(bool ignoreCacheUntil)
        {
            string url = String.Format("{0}{1}?version=2", Constants.ApiPrefix, Constants.ConquerableStationOutpost);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as ConquerableStationList;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            ConquerableStationList outpostList = ConquerableStationList.FromXmlDocument(xmlDoc);
            Cache.Set(url, outpostList);

            return outpostList;
        }

        /// <summary>
        /// Gets a data structure containing information on every skill in the game.
        /// </summary>
        /// <returns></returns>
        public static SkillTree GetSkillTree()
        {
            return GetSkillTree(false);
        }

        /// <summary>
        /// Gets a data structure containing information on every skill in the game.
        /// </summary>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static SkillTree GetSkillTree(bool ignoreCacheUntil)
        {
            string url = String.Format("{0}{1}?version=1", Constants.ApiPrefix, Constants.SkillTree);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as SkillTree;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            SkillTree skillTree = SkillTree.FromXmlDocument(xmlDoc);
            Cache.Set(url, skillTree);

            return skillTree;
        }

        /// <summary>
        /// Gets a list of all alliances and their member corporations
        /// </summary>
        /// <returns></returns>
        public static AllianceList GetAllianceList()
        {
            return GetAllianceList(false);
        }

        /// <summary>
        /// Gets a list of all alliances and their member corporations
        /// </summary>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static AllianceList GetAllianceList(bool ignoreCacheUntil)
        {
            string url = String.Format("{0}{1}?version=1", Constants.ApiPrefix, Constants.AllianceList);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as AllianceList;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            AllianceList allianceList = AllianceList.FromXmlDocument(xmlDoc);
            Cache.Set(url, allianceList);

            return allianceList;
        }

        /// <summary>
        /// Get the currently training Skill for a character
        /// </summary>
        /// <param name="userId">User Id of account for authentication</param>
        /// <param name="characterId">Character Id of the character to get skill info for</param>
        /// <param name="apiKey">limited access API key of Account</param>
        /// <returns></returns>
        public static SkillInTraining GetSkillInTraining(int userId, int characterId, string apiKey)
        {
            return GetSkillInTraining(userId, characterId, apiKey, false);
        }

        /// <summary>
        /// Get the currently training Skill for a character
        /// </summary>
        /// <param name="userId">User Id of account for authentication</param>
        /// <param name="characterId">Character Id of the character to get skill info for</param>
        /// <param name="apiKey">limited access API key of Account</param>
        /// <param name="ignoreCacheUntil">Ignores the cacheUntil and will return the cache even if expired</param>
        /// <returns></returns>
        public static SkillInTraining GetSkillInTraining(int userId, int characterId, string apiKey, bool ignoreCacheUntil)
        {
            string url = String.Format("{0}{1}?userID={2}&characterID={3}&apiKey={4}", Constants.ApiPrefix, Constants.SkillInTraining, userId, characterId, apiKey);

            API_Base cachedResponse = Cache.Get(url, ignoreCacheUntil);
            if (cachedResponse != null)
            {
                return cachedResponse as SkillInTraining;
            }

            XmlDocument xmlDoc = Network.GetXml(url);
            SkillInTraining skillintraining = SkillInTraining.FromXmlDocument(xmlDoc);
            Cache.Set(url, skillintraining);

            return skillintraining;
        }

        /// <summary>
        /// Retrieve the portrait for a character
        /// </summary>
        /// <param name="characterId">Retrieve the portrait of the character with this id</param>
        /// <param name="portraitSize">Small (64) or Large (256)</param>
        /// <returns></returns>
        public static Image GetCharacterPortrait(int characterId, PortraitSize portraitSize)
        {
            int imageSize;
            if (portraitSize == PortraitSize.Large)
                imageSize = 256;
            else if (portraitSize == PortraitSize.Small)
                imageSize = 64;
            else
                imageSize = 64;

            string url = String.Format("{0}?c={1}&s={2}", Constants.ImageFullURL, characterId.ToString(), imageSize.ToString());
            return Network.GetImage(url);
        }
    }

    /// <summary>
    /// Raised when an error reponse is received from an eve api request
    /// </summary>
    public class ApiResponseErrorException : Exception
    {
        /// <summary>
        /// The error code
        /// </summary>
        public string Code;

        /// <summary>
        /// Sets the current error code to the code recieved
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public ApiResponseErrorException(string code, string message)
            : base(message)
        {
            this.Code = code;
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
