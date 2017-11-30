FROM microsoft/azureiotedge-functions-binding:1.0-preview

ENV AzureWebJobsScriptRoot=/home/site/wwwroot

ARG EXE_DIR=.

EXPOSE 80

COPY $EXE_DIR/ /home/site/wwwroot