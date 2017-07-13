using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using SampleExtension;

namespace SampleFunctionApp
{
    public static class HttpTriggerSlack
    {
        [FunctionName("HttpTriggerSlack")]
        public static HttpResponseMessage Run(
            [HttpTrigger] SlackMessage message, HttpRequestMessage req,
            [Slack(WebHookUrl = "SlackWebHook")] out SlackMessage slackMessage,
            TraceWriter log)
        {
            log.Info($"Request received: {req}");

            slackMessage = message;

            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}