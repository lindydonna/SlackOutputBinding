using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleExtension
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

        [JsonProperty("mrkdwn")]
        public bool IsMarkdown { get; set; }
    }
}
