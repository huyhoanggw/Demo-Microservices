FROM mcr.microsoft.com/dotnet/sdk:8.0 as build

WORKDIR /src

COPY eCommerce.SharedLibary/eCommerce.SharedLibary/eCommerce.SharedLibary.csproj eCommerce.SharedLibary/eCommerce.SharedLibary/
COPY AuthenticationApiSolution/AuthenticationApi.Presentation/AuthenticationApi.Presentation.csproj AuthenticationApiSolution/AuthenticationApi.Presentation/
COPY AuthenticationApiSolution/AuthenticationApi.Domain/AuthenticationApi.Domain.csproj AuthenticationApiSolution/AuthenticationApi.Domain/
COPY AuthenticationApiSolution/AuthenticationApi.Infrastructure/AuthenticationApi.Infrastructure.csproj AuthenticationApiSolution/AuthenticationApi.Infrastructure/
COPY AuthenticationApiSolution/AuthenticationApi.Application/AuthenticationApi.Application.csproj AuthenticationApiSolution/AuthenticationApi.Application/

RUN dotnet restore AuthenticationApiSolution/AuthenticationApi.Presentation/AuthenticationApi.Presentation.csproj 

COPY . .


RUN dotnet publish AuthenticationApiSolution/AuthenticationApi.Presentation/AuthenticationApi.Presentation.csproj -c Release -o /app/publish /p:UserAppHost=false 

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish . 
ENTRYPOINT ["dotnet" , "AuthenticationApi.Presentation.dll"]