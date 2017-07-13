// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;
using System;

namespace SampleExtension
{
    /// <summary>
    /// Attribute used to bind a parameter to a Slack. Message will be posted to Slack when the 
    /// method completes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
    [Binding]
    public sealed class SlackAttribute : Attribute
    {
        /// <summary>
        /// Sets the WebHookUrl for the current outgoing Slack message. May include binding parameters.
        /// </summary>
        [AppSetting(Default = "SlackWebHookKeyName")]
        public string WebHookUrl { get; set; }

        /// <summary>
        /// Sets the Text for the current outgoing Slack message. May include binding parameters.
        /// </summary>
        [AutoResolve]
        public string Text { get; set; }

        /// <summary>
        /// Sets the username for the outgoing request. May include binding parameters.
        /// </summary>
        [AutoResolve]
        public string Username { get; set; }

        /// <summary>
        /// Controls the icon emoji displayed for the message. Use ":{emoji_name}:" in your string. May include binding parameters.
        /// </summary>
        /// <remarks>
        /// The format for emoji is a keyword surrounded by ":". It supports custom emojis.
        /// </remarks>
        /// <example>
        /// Example Icon Emoji for heart: ":heart:"
        /// </example>       
        [AutoResolve]
        public string IconEmoji { get; set; }

        /// <summary>
        /// Controls the channel the message is sent to. Use "#{name}" to send to a channel, and "@{name}" to send to a specific user."
        /// </summary>
        /// <remarks>
        /// May include binding parameters.
        /// </remarks>
        [AutoResolve]
        public string Channel { get; set; }

        /// <summary>
        /// Tells Slack whether or not to process this message as Markdown. Default value is true. May include binding parameters.
        /// </summary>        
        public bool IsMarkdown { get; set; }
    }
}
