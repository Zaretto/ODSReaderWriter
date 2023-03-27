pushd I:\dev\OdsReaderWriter\OdsReaderWriter\OdsReaderWriter\bin\Release
dotnet nuget push OdsReaderWriter.1.0.10.nupkg --api-key %NUGET_API_KEY% --source https://api.nuget.org/v3/index.json
popd

