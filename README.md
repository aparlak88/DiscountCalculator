# Assesment project by Alparslan PARLAK

This project requires you to have .NET 6 SDK installed and
have privileges to run dotnet commands on your CLI.

To build application on root folder, run the command below:

dotnet build ./ShopRU

To run unit tests aganist the code with a coverage report and 
.lcov output, run the command below on the root folder:

dotnet test ./ShopsRU.Test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov

You can check file diagram.drawio.png for class diagram.