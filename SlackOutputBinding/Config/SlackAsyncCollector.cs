using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Azure.WebJobs.Sample.Config
{
    internal class SlackAsyncCollector : IAsyncCollector<SlackMessage>
    {
        private SlackConfiguration _config;
        private SlackAttribute _attr;

        public SlackAsyncCollector(SlackConfiguration config, SlackAttribute attr)
        {
            _config = config;
            _attr = attr;
        }
            
        public Task AddAsync(SlackMessage item, CancellationToken cancellationToken = default(CancellationToken))
        {
            var mergedItem = MergeMessageProperties(item, _config, _attr);
            SendSlackMessage(mergedItem);

            return Task.CompletedTask;
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

        private static void SendSlackMessage(SlackMessage mergedItem)
        {
            Console.WriteLine(mergedItem.Text);
        }
    }
}
