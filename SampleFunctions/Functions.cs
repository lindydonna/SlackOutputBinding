using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using SampleExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleFunctions
{
    public class Functions
    {
        [NoAutomaticTrigger]
        public void SimpleSlackBinding(
           [Slack(Text = "Hi", WebHookUrl = "SlackWebHook", IconEmoji = ":simplesmile:")] out SlackMessage message,
           TraceWriter log)
        {
            message = new SlackMessage(); // no customization.            
        }
    }

    public class CustomMessage
    {
        public string WebHookUrl { get; set; }

        public string Text { get; set; }

        public string Username { get; set; }

        public string IconEmoji { get; set; }

        public string Channel { get; set; }

        public bool IsMarkdown { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
