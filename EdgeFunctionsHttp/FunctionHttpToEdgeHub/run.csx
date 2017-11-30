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
using System.Linq;
using Newtonsoft.Json;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, IAsyncCollector<Message> output, TraceWriter log)
{
    log.Info("C# Edge HTTP trigger function received a request.");

    // read http post body
    string body = await req.Content.ReadAsStringAsync();


    // // How to read (GET) query parameters
    // var queryDictionary = QueryHelpers.ParseQuery(req.RequestUri.Query);
    // foreach(var key in queryDictionary.Keys)
    // {
    //     var value = queryDictionary[key];
    //     //log.Info($"Query key: {key} - value: {value.ToString()}");
    // }

    if (!string.IsNullOrEmpty(body))
    {
        log.Info($"Received a message. Body content: {body}");
        var telemetryDataPoint = new
        {
            edgeHubContent = body,
            deviceTime = DateTime.UtcNow.ToString("o"),
            processcedBy = "EdgeModuleRestFunction"
        };
        var outMessageString = JsonConvert.SerializeObject(telemetryDataPoint);
        var outMessage = new Message(Encoding.UTF8.GetBytes(outMessageString));

        await output.AddAsync(outMessage);
        log.Info($"Piped out the message to IoT Edge: [ {outMessageString} ]");

        var res = new HttpResponseMessage(HttpStatusCode.OK);
        res.Content = new ByteArrayContent(Encoding.UTF8.GetBytes($"All good. {body} was sent to EdgeHub"));
        return res;
    }

    var resError = new HttpResponseMessage(HttpStatusCode.BadRequest);
    resError.Content = new ByteArrayContent(Encoding.UTF8.GetBytes($"Missing body content. Could not process your request for EdgeHub."));
    return resError;
}