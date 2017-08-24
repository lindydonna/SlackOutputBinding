# Slack output binding sample

This sample is a Azure Functions binding extension that allows you to write a Slack message by just adding an output binding.

## Using the binding

To use the binding, register a new WebHook with Slack. Put the url in local.settings.json with the key `SlackWebHook`.

### C#

Just reference the Slack binding assembly and use the `[Slack]` attribute in your code:

```csharp
    [FunctionName("HttpTriggerSlack")]
    public static string Run(
        [HttpTrigger] SlackMessage message, 
        [Slack(WebHookUrl = "SlackWebHook")] out SlackMessage slackMessage,
        TraceWriter log)
```

### JavaScript

For JavaScript, the process is currently manual. Do the following:
1. Copy the extension to an output folder such as "extensions". This can be done in a post-build step in the .csproj
2. Add the app setting `AzureWebJobs_ExtensionsPath` to local.settings.json (or in Azure, in App Settings). Set the value to the **parent** of your "extension" folder from the previous step.

The project **SampleFunction** app already has a post-build step that copies the assembly to the folder **Extensions**.             