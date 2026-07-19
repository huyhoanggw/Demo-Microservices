FROM mcr.microsoft.com/dotnet/sdk:8.0 As build

WORKDIR /src

COPY eCommerce.SharedLibary/eCommerce.SharedLibary/eCommerce.SharedLibary.csproj eCommerce.SharedLibary/eCommerce.SharedLibary/
COPY DemoEcommerce.ApiGatewaySolution/ApiGateway.Presentation/ApiGateway.Presentation.csproj DemoEcommerce.ApiGatewaySolution/ApiGateway.Presentation/

RUN dotnet restore DemoEcommerce.ApiGatewaySolution/ApiGateway.Presentation/ApiGateway.Presentation.csproj 
COPY . .

RUN dotnet publish DemoEcommerce.ApiGatewaySolution/ApiGateway.Presentation/ApiGateway.Presentation.csproj -c Release -o /app/publish /p:UserAppHost=false 

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime 
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet" , "ApiGateway.Presentation.dll"]