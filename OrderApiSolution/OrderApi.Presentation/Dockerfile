FROM mcr.microsoft.com/dotnet/sdk:8.0 as build


WORKDIR /src

COPY eCommerce.SharedLibary/eCommerce.SharedLibary/eCommerce.SharedLibary.csproj eCommerce.SharedLibary/eCommerce.SharedLibary/
COPY OrderApiSolution/OrderApi.Domain/OrderApi.Domain.csproj OrderApiSolution/OrderApi.Domain/
COPY OrderApiSolution/OrderApi.Application/OrderApi.Application.csproj OrderApiSolution/OrderApi.Application/
COPY OrderApiSolution/OrderApi.Infrastructure/OrderApi.Infrastructure.csproj OrderApiSolution/OrderApi.Infrastructure/
COPY OrderApiSolution/OrderApi.Presentation/OrderApi.Presentation.csproj OrderApiSolution/OrderApi.Presentation/

run dotnet restore OrderApiSolution/OrderApi.Presentation/OrderApi.Presentation.csproj

COPY . .

RUN dotnet publish OrderApiSolution/OrderApi.Presentation/OrderApi.Presentation.csproj -c Release -o /app/publish /p:UserAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet" , "OrderApi.Presentation.dll"]