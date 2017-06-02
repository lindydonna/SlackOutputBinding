﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Azure.WebJobs;
using System;
using SampleFunctions;
using SampleExtension.Config;

namespace Host
{
    // WebJobs is .NET 4.6 
    class Program
    {
        static void Main(string[] args)
        {
            var config = new JobHostConfiguration();
            config.DashboardConnectionString = null;

            // apply config before creating the host. 
            var slackExtension = new SlackConfiguration();
            config.AddExtension(slackExtension);

            // Debug diagnostics!
            config.CreateMetadataProvider().DebugDumpGraph(Console.Out);

            var host = new JobHost(config);

            // Test some invocations. 
            // We're not using listeners here, so we can invoke directly. 
            var method = typeof(Functions).GetMethod("SimpleSlackBinding");
            host.Call(method);

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            // host.RunAndBlock();
        }
    }
}
