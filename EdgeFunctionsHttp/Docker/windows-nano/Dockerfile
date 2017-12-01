FROM microsoft/azureiotedge-functions-binding:1.0-preview

ENV AzureWebJobsScriptRoot=c:\\approot

ARG EXE_DIR=.

EXPOSE 80

COPY $EXE_DIR/ c:\\approot
