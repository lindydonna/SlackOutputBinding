using Microsoft.Azure.WebJobs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleExtension.Config
{
    internal class SlackAsyncCollector : IAsyncCollector<SlackMessage>
    {
        private SlackConfiguration config;
        private SlackAttribute attr;
        private static HttpClient client = new HttpClient();

        public SlackAsyncCollector(SlackConfiguration config, SlackAttribute attr)
        {
            this.config = config;
            this.attr = attr;
        }
            
        public async Task AddAsync(SlackMessage item, CancellationToken cancellationToken = default(CancellationToken))
        {
            var mergedItem = MergeMessageProperties(item, config, attr);
            await SendSlackMessage(mergedItem, attr);
        }

        public Task FlushAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.CompletedTask;
        }

        // combine properties to create final message that will be sent
        private static SlackMessage MergeMessageProperties(SlackMessage item, SlackConfiguration config, SlackAttribute attr)
        {
            var result = new SlackMessage();

            result.Text = FirstOrDefault(item.Text, attr.Text);
            result.Channel = FirstOrDefault(item.Channel, attr.Channel, config.Channel);
            result.Username = FirstOrDefault(item.Username, attr.Username, config.Username);
            result.IconEmoji = FirstOrDefault(item.IconEmoji, attr.IconEmoji, config.IconEmoji);
            result.IsMarkdown = item.IsMarkdown;

            return result;
        }

        private static string FirstOrDefault(params string[] values)
        {
            return values.FirstOrDefault(v => !string.IsNullOrEmpty(v));
        }

        private static async Task SendSlackMessage(SlackMessage mergedItem, SlackAttribute attribute)
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var payload = JObject.FromObject(new
                {
                    text = mergedItem.Text,
                    channel = mergedItem.Channel,
                    username = mergedItem.Username,
                    icon_emoji = mergedItem.IconEmoji,
                    mrkdwn = mergedItem.IsMarkdown
                });

                var response = await client.PostAsJsonAsync(attribute.WebHookUrl, payload);
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
