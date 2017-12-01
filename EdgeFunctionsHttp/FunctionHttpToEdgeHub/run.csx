#r "Microsoft.Azure.Devices.Client"
#r "Newtonsoft.Json"
#r "System.Linq" 
#r "System.Net.Http"

using System.Net;
using Microsoft.Azure.Devices.Client;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq; 

// Not all using and reference statements are currently used. Just as an example here

/// <summary>
/// Azure Function to be executed as an IoT Edge module
/// Takes http POST request and writes the http body content out to EdgeHub for further processing
/// </summary>
/// <param name="req">http request</param>
/// <param name="output">IoT Edge Hub output queue</param>
/// <param name="log"></param>
/// <returns></returns>
public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, IAsyncCollector<Message> output, TraceWriter log)
{
    log.Info("C# Edge HTTP trigger function received a request.");

    // read http post body
    string body = await req.Content.ReadAsStringAsync();


    // // Example on how to read (GET) query parameters
    // var queryDictionary = QueryHelpers.ParseQuery(req.RequestUri.Query);
    // foreach(var key in queryDictionary.Keys)
    // {
    //     var value = queryDictionary[key];
    //     //log.Info($"Query key: {key} - value: {value.ToString()}");
    // }

    if (!string.IsNullOrEmpty(body))
    {
        log.Info($"Received a message. Body content: {body}");

        // We build some arbitrary telemetry message to put into the queue, using the http body content
        var telemetryDataPoint = new
        {
            edgeHubContent = body,
            deviceTime = DateTime.UtcNow.ToString("o"),
            processcedBy = "EdgeModuleRestFunction"
        };
        var outMessageString = JsonConvert.SerializeObject(telemetryDataPoint);
        var outMessage = new Message(Encoding.UTF8.GetBytes(outMessageString));

        log.Info($"Writing out the message to IoT Edge Hub queue: [ {outMessageString} ]");
        await output.AddAsync(outMessage);

        var res = new HttpResponseMessage(HttpStatusCode.OK);
        res.Content = new ByteArrayContent(Encoding.UTF8.GetBytes($"Done. Body content [ {body} ] was sent to IoT Edge Hub"));
        return res;
    }

    var resError = new HttpResponseMessage(HttpStatusCode.BadRequest);
    resError.Content = new ByteArrayContent(Encoding.UTF8.GetBytes($"Missing body content. Could not process your request to IoT Edge Hub."));
    return resError;
}