using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitch_Downloader_Console
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Auth
    {
        [JsonProperty("token", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        [JsonProperty("sig", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Sig { get; set; }

        [JsonProperty("expires_at", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? ExpiresAt { get; set; }
    }

    public partial class Auth
    {
        public static Auth FromJson(string json) => JsonConvert.DeserializeObject<Auth>(json, Twitch_Downloader_Console.Converter.Settings);
    }

    
}