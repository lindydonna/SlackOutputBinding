using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using SampleExtension;

namespace SampleFunctionApp
{
    public static class HttpTriggerSlack
    {
        [FunctionName("HttpTriggerSlack")]
        public static HttpResponseMessage Run(
            [HttpTrigger] SlackMessage message,
            HttpRequestMessage req,
            [Slack(WebHookUrl = "SlackWebHook")] out SlackMessage slackMessage,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            slackMessage = message;

            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}