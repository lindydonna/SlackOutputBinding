// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SampleExtension.Config
{
    /// <summary>
    /// Extension for binding <see cref="SlackMessage"/>.
    /// </summary>
    public class SlackConfiguration : IExtensionConfigProvider
    {
        #region Global configuration defaults
        /// <summary>
        /// Gets or sets the default "from: username" that will be used for messages.
        /// This value can be overridden by job functions.
        /// </summary>
        /// <remarks>
        /// This is a useful setting if you're planning on having lots of WebJobs posting
        /// to the same WebHook, and want to differeniate them with separate usernames.
        /// </remarks>
        public string Username { get; set; }

        /// Gets or sets the default "Icon Emoji" that will be used for messages.
        /// This value can be overridden by job functions.
        /// </summary>
        /// <remarks>
        /// This is a useful setting if you're planning on having lots of WebJobs posting
        /// to the same WebHook, and want to differeniate them with separate Icons.
        /// 
        /// The format for emoji is a keyword surrounded by ":". It supports custom emojis.
        /// </remarks>
        /// <example>
        /// Example Icon Emoji for heart: ":heart:"
        /// </example>
        public string IconEmoji { get; set; }

        /// <summary>
        /// Gets or sets the default "to: channel" that will be used for messages.
        /// This value can be overridden by job functions.
        /// </summary>
        /// <remarks>
        /// To send to a specific channel, use the "#channel" pattern.
        /// 
        /// To send to a specific user, use the "@username" pattern.
        /// 
        /// This is a useful setting if you're reusing the same WebHook as another integration, but
        /// want to use a separate channel. It is also helpful for testing purposes.
        /// </remarks>
        /// <example>
        /// Example for sending to a specific channel, bot-channel: "#bot-channel"
        /// Example for sending to a specific user: "@username"
        /// </example>
        public string Channel { get; set; }
        #endregion

    public void Initialize(ExtensionConfigContext context)
    {
        // add converter between JObject and SlackMessage
        // Allows a user to bind to IAsyncCollector<JObject>, and the sdk will convert that to IAsyncCollector<SlackMessage>
        context.AddConverter<JObject, SlackMessage>(input => input.ToObject<SlackMessage>());

        // Add a binding rule for Collector
        context.AddBindingRule<SlackAttribute>()
            .BindToCollector<SlackMessage>(attr => new SlackAsyncCollector(this, attr));
    }
    }
}
