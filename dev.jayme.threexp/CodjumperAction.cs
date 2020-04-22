using BarRaider.SdTools;
using Codjumper.Backend;
using Codjumper.Wrappers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Codjumper
{
    [PluginActionId("dev.jayme.threexp.codjumper")]
    public class CodjumperAction : PluginBase
    {
        public enum CycleMethods
        {
            Static,
            Dynamic
        }

        private class PluginSettings
        {
            public static PluginSettings CreateDefaultSettings()
            {
                PluginSettings instance = new PluginSettings
                {
                    Application = String.Empty,
                    AppArguments = String.Empty,
                    AppStartIn = String.Empty,
                    LimitInstances = false,
                    KeyCycle = CycleMethods.Static,
                    LengthOfCycle = 5.ToString(),
                };
                return instance;
            }

            [FilenameProperty]
            [JsonProperty(PropertyName = "application")]
            public string Application { get; set; }

            [JsonProperty(PropertyName = "appStartIn")]
            public string AppStartIn { get; set; }

            [JsonProperty(PropertyName = "appArguments")]
            public string AppArguments { get; set; }

            [JsonProperty(PropertyName = "limitInstances")]
            public bool LimitInstances { get; set; }

            [JsonProperty(PropertyName = "keyCycle")]
            public CycleMethods KeyCycle { get; set; }

            [JsonProperty(PropertyName = "lengthOfCycle")]
            public string LengthOfCycle { get; set; }
        }

        #region Private Members

        private readonly PluginSettings settings;

        private DateTime lastCycleChange = DateTime.MinValue;

        private const string youtubeQuery = "https://www.youtube.com/results?search_query=";

        private const int longKeyPressLength = 1000;
        private const int singleInstance = 1;

        private int currentStage;

        private bool keyPressed = false;
        private bool longKeyPress = false;

        private DateTime keyPressStartAt;

        #endregion

        public CodjumperAction(SDConnection connection, InitialPayload payload) : base(connection, payload)
        {
            if (payload.Settings == null || payload.Settings.Count == 0)
            {
                this.settings = PluginSettings.CreateDefaultSettings();
            }
            else
            {
                this.settings = payload.Settings.ToObject<PluginSettings>();
            }
        }

        public override void Dispose()
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, $"Destructor called");
        }

        public override void KeyPressed(KeyPayload payload)
        {
            keyPressed = true;
            Logger.Instance.LogMessage(TracingLevel.INFO, "Key Pressed");
            longKeyPress = false;
            keyPressStartAt = DateTime.Now;
        }

        public async override void KeyReleased(KeyPayload payload)
        {
            keyPressed = false;
            if (!longKeyPress)
            {
                Logger.Instance.LogMessage(TracingLevel.INFO, "Detected Short Key Press");
                await HandleApplicationRun();
            }
        }

        public async override void OnTick()
        {
            if (keyPressed)
            {
                int keyWasPressedAt = (int)(DateTime.Now - keyPressStartAt).TotalMilliseconds;
                if (keyWasPressedAt >= longKeyPressLength)
                {
                    LongKeyPress();
                }
            }
            CodjumperMainData mainData = await CodjumperDataManager.Instance.GetMainData();
            if (mainData == null)
                return;
            try
            {
                string mapName = mainData.Rcon.Serv.MapName;

                if (settings.KeyCycle == CycleMethods.Static)
                {
                    Regex regex = new Regex("mp_");
                    mapName = regex.Replace(mapName, "", 1);
                    mapName = mapName.First().ToString().ToUpper() + mapName.Substring(1);

                    switch (mapName)
                    {
                        case "Aerodynamics":
                            await Connection.SetTitleAsync("Aero-\ndynamics");
                            break;

                        case "Achromatic":
                            await Connection.SetTitleAsync("Achro-\nmatic");
                            break;

                        case "Architektur":
                            await Connection.SetTitleAsync("Archi-\ntektur");
                            break;

                        case "Dependence":
                            await Connection.SetTitleAsync("Depen-\ndence");
                            break;

                        case "Dependence_v2":
                            await Connection.SetTitleAsync("Depen-\ndence\nv2");
                            break;

                        case "Lavajump_v2_nos":
                            await Connection.SetTitleAsync("Lavajump\nv2 nos");
                            break;

                        case "Millennium":
                            await Connection.SetTitleAsync("Millen-\nnium");
                            break;

                        case "Morningstar":
                            await Connection.SetTitleAsync("Morning-\nstar");
                            break;

                        default:
                            mapName = mapName.Replace("_", "\n");
                            await Connection.SetTitleAsync(mapName);
                            break;
                    }
                }

                if (settings.KeyCycle == CycleMethods.Dynamic)
                {
                    if (String.IsNullOrEmpty(settings.LengthOfCycle))
                    {
                        await Connection.SetTitleAsync("Please\nspecify\na valid\nduration.");
                        return;
                    }
                    if ((DateTime.Now - lastCycleChange).TotalSeconds >= Convert.ToDouble(settings.LengthOfCycle))
                    {
                        lastCycleChange = DateTime.Now;
                        currentStage = (currentStage + 1) % 2;
                    }
                    switch (currentStage)
                    {
                        case 0:
                            Regex regex = new Regex("mp_");
                            mapName = regex.Replace(mapName, "", 1);
                            mapName = mapName.First().ToString().ToUpper() + mapName.Substring(1);
                            {
                                switch (mapName)
                                {
                                    case "Aerodynamics":
                                        await Connection.SetTitleAsync("Aero-\ndynamics");
                                        break;

                                    case "Achromatic":
                                        await Connection.SetTitleAsync("Achro-\nmatic");
                                        break;

                                    case "Architektur":
                                        await Connection.SetTitleAsync("Archi-\ntektur");
                                        break;

                                    case "Dependence":
                                        await Connection.SetTitleAsync("Depen-\ndence");
                                        break;

                                    case "Dependence_v2":
                                        await Connection.SetTitleAsync("Depen-\ndence\nv2");
                                        break;

                                    case "Lavajump_v2_nos":
                                        await Connection.SetTitleAsync("Lavajump\nv2 nos");
                                        break;

                                    case "Millennium":
                                        await Connection.SetTitleAsync("Millen-\nnium");
                                        break;

                                    case "Morningstar":
                                        await Connection.SetTitleAsync("Morning-\nstar");
                                        break;

                                    default:
                                        mapName = mapName.Replace("_", "\n");
                                        await Connection.SetTitleAsync(mapName);
                                        break;
                                }
                                break;
                            }
                        case 1:
                            string currentPlayers = mainData.Rcon.Clients.Count.ToString();
                            string maxPlayers = mainData.Rcon.Serv.SvMaxClients.ToString();
                            await Connection.SetTitleAsync("Players:\n" + "(" + currentPlayers + "/" + maxPlayers + ")");
                            break;
                    }
                }
            }
            catch
            {
                if ((DateTime.Now - lastCycleChange).TotalSeconds >= 5.0)
                {
                    lastCycleChange = DateTime.Now;
                    currentStage = (currentStage + 1) % 2;
                }
                switch (currentStage)
                {
                    case 0:
                        await Connection.SetTitleAsync("Invalid\nresponse\nfrom 3xP'\nAPI...");
                        break;
                    case 1:
                        await Connection.SetTitleAsync("Fetching\ndata from\n3xP' API\nawait...");
                        break;
                }
            }
        }

        private async void LongKeyPress()
        {
            CodjumperMainData mainData = await CodjumperDataManager.Instance.GetMainData();
            if (mainData == null)
                return;
            longKeyPress = true;
            Logger.Instance.LogMessage(TracingLevel.INFO, "Detected Long Key Press");
            Process.Start(youtubeQuery + mainData.Rcon.Serv.MapName + " walkthrough");
            keyPressed = false;
        }

        public override void ReceivedSettings(ReceivedSettingsPayload payload)
        {
            string appOld = settings.Application;
            Tools.AutoPopulateSettings(settings, payload.Settings);
            if (appOld != settings.Application)
            {
                InitializeStartInDirectory();
            }
            SaveSettings();
        }
        
        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload) { }

        #region Private Methods

        private Task SaveSettings()
        {
            return Connection.SetSettingsAsync(JObject.FromObject(settings));
        }

        private void InitializeStartInDirectory()
        {
            if (!File.Exists(settings.Application))
            {
                return;
            }
            settings.AppStartIn = new FileInfo(settings.Application).Directory.FullName;
        }

        private async Task HandleApplicationRun()
        {
            try
            {
                if (String.IsNullOrEmpty(settings.Application))
                {
                    Logger.Instance.LogMessage(TracingLevel.WARN, "HandleApplicationRun was called, but no Application is configured.");
                    await Connection.ShowAlert();
                    return;
                }
                if (!File.Exists(settings.Application))
                {
                    Logger.Instance.LogMessage(TracingLevel.WARN, $"HandleApplicationRun was called, but file doesn't exist. Application: {settings.Application}");
                    await Connection.ShowAlert();
                    return;
                }
                FileInfo fileInfo = new FileInfo(settings.Application);
                string fileName = fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf('.'));
                if (settings.LimitInstances)
                {
                    int count = Process.GetProcessesByName(fileName).ToList().Count;
                    if (count >= singleInstance)
                    {
                        Logger.Instance.LogMessage(TracingLevel.INFO, $"Max instances reached. {count}/{singleInstance} instances are currently running.");
                        return;
                    }
                }
                RunApplication();
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(TracingLevel.ERROR, $"HandleApplicationRun called an Exception for {settings.Application}. Exception: {ex}");
                await Connection.ShowAlert();
            }
        }

        private void RunApplication()
        {
            ProcessStartInfo start = new ProcessStartInfo();
            if (!string.IsNullOrEmpty(settings.AppArguments))
            {
                start.Arguments = settings.AppArguments;
            }
            if (Directory.Exists(settings.AppStartIn))
            {
                start.WorkingDirectory = settings.AppStartIn;
                start.FileName = settings.Application;
            }
            Process.Start(start);
        }
        #endregion
    }
}