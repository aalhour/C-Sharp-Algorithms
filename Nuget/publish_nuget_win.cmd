cd ..
dotnet build -c Release
cd Nuget
nuget pack c-sharp-algorithms.nuspec -Exclude .\*.*
nuget push C-Sharp-Algorithms.1.0.0.nupkg -Source https://api.nuget.org/v3/index.json