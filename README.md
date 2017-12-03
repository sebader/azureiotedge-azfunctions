# Azure IoT Edge - Azure Functions examples
Example for Azure Functions that can be used as IoT Edge modules. These examples provide advanced mechanisms compared to the example from the official documentation, e.g. those show HTTP and timer triggered functions.

Very important note: IoT Edge Functions (in C#) only support .NET Core! Therefore you must be careful which references you use and how to use them (e.g. HttpRequestMessage is very different in .NET Core)

Detailed description on how to build and deploy the functions can be found in the official documentation: https://docs.microsoft.com/en-us/azure/iot-edge/tutorial-deploy-function


Please feel free to extend the existing examples our create new example and make a pull request!
