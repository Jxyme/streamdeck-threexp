using Newtonsoft.Json;
using System.Collections.Generic;

namespace Codjumper.Wrappers
{
    public partial class CodjumperMainData
    {
        [JsonProperty(PropertyName = "rcon")]
        public Rcon Rcon { get; set; }
    }

    public partial class Rcon
    {
        [JsonProperty(PropertyName = "serv")]
        public Serv Serv { get; set; }

        [JsonProperty(PropertyName = "clients")]
        public List<Client> Clients { get; set; }
    }

    public partial class Serv
    {
        [JsonProperty(PropertyName = "mapname")]
        public string MapName { get; set; }

        [JsonProperty(PropertyName = "sv_maxclients")]
        public long SvMaxClients { get; set; }

        [JsonProperty(PropertyName = "uptime")]
        public string Uptime { get; set; }
    }

    public partial class Client { }
}