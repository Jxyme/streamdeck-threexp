using BarRaider.SdTools;
using Codjumper.Wrappers;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Codjumper.Backend
{
    internal class CodjumperDataManager
    {
        private static CodjumperDataManager instance = (CodjumperDataManager) null;
        private static readonly object objLock = new object();

        private DateTime lastRefreshTime = DateTime.MinValue;

        private const string URL = "https://api.3xp-clan.com/v1/server/7";
        private const float RATE = 60.0f;
        
        private CodjumperMainData mainData;

        public static CodjumperDataManager Instance
        {
            get
            {
                if (CodjumperDataManager.instance != null)
                    return CodjumperDataManager.instance;
                lock (CodjumperDataManager.objLock)
                {
                    if (CodjumperDataManager.instance == null)
                        CodjumperDataManager.instance = new CodjumperDataManager();
                    return CodjumperDataManager.instance;
                }
            }
        }

        private CodjumperDataManager()
        {
            // Storage
        }

        public async Task<CodjumperMainData> GetMainData()
        {
            await this.LoadCodjumperData();
            return this.mainData;
        }

        private async Task LoadCodjumperData()
        {
            if ((DateTime.Now - this.lastRefreshTime).TotalSeconds < RATE)
                return;
            Logger.Instance.LogMessage(TracingLevel.INFO, "Fetching 3xP' Codjumper data...");
            this.lastRefreshTime = DateTime.Now;
            string str = await this.QueryAPI(URL);
            if (!String.IsNullOrEmpty(str))
                this.mainData = JsonConvert.DeserializeObject<CodjumperMainData>(str);
            try
            {
                Logger.Instance.LogMessage(TracingLevel.DEBUG, "Current Map: " + mainData.Rcon.Serv.MapName + " | " + "Players Online: " + mainData.Rcon.Clients.Count.ToString() + "/" + mainData.Rcon.Serv.SvMaxClients);
            }
            catch
            {
                Logger.Instance.LogMessage(TracingLevel.DEBUG, "Unable to fetch 3xP' Codjumper data. | Server may be offline, attempting to re-fetch...");
            }
        }

        private async Task<string> QueryAPI(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = new TimeSpan(0, 0, 30);
                    HttpResponseMessage async = await client.GetAsync(url);
                    if (async.IsSuccessStatusCode)
                        return await async.Content.ReadAsStringAsync();
                    Logger.Instance.LogMessage(TracingLevel.ERROR, string.Format("LoadCodjumperData faled. Response: {0} Status Code: {1}", (object)async.ReasonPhrase, (object)async.StatusCode));
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(TracingLevel.ERROR, string.Format("LoadCodjumperData called an exception. Response: {0}", (object)ex));
            }
            return (string)null;
        }
    }
}