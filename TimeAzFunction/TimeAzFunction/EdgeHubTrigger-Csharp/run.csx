#r "Microsoft.Azure.Devices.Client"
#r "Newtonsoft.Json"

using System.IO;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Text;

public static async Task Run(TimerInfo myTimer, IAsyncCollector<Message> output, TraceWriter log)
{
    log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

    var telemetryDataPoint = new
    {
        deviceId = "timerFunction",
        temperatur = 1337,
        deviceTime = DateTime.UtcNow.ToString("o"),
        processcedBy = "EdgeModuleTimerFunction"
    };
    var outMessageString = JsonConvert.SerializeObject(telemetryDataPoint);
    var outMessage = new Message(Encoding.UTF8.GetBytes(outMessageString));

    await output.AddAsync(outMessage);

}