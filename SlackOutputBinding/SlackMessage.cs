using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.WebJobs.Sample
{
    public class SlackMessage
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("icon_emoji")]
        public string IconEmoji { get; set; }

        //[JsonProperty("icon_url")]
        //public Uri IconUrl { get; set; }

        [JsonProperty("mrkdwn")]
        public bool IsMarkdown { get; set; }

        //[JsonProperty("link_names")]
        //public bool LinkNames { get; set; }
    }
}
