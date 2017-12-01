#EdgeFunctionTimer

Example for a timer triggered Azure function which sends data into Edge Hub queue.

Note: To run this function, you need to specify an Azure storage account connection string in the IoT Edge module "Container Create Option" like this:

{
  "Env": [
    "AzureWebJobsStorage=DefaultEndpointsProtocol=https;AccountName=****MYACCOUNTNAME****;AccountKey=******StorageKey********"
  ]
}

We are setting the AzureWebJobsStorage as an environment variable to the container.
