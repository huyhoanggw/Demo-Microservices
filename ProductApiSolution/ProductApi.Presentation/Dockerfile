FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src 

COPY eCommerce.SharedLibary/eCommerce.SharedLibary/eCommerce.SharedLibary.csproj eCommerce.SharedLibary/eCommerce.SharedLibary/
COPY ProductApiSolution/ProductApi.Presentation/ProductApi.Presentation.csproj ProductApiSolution/ProductApi.Presentation/
COPY ProductApiSolution/ProductApi.Domain/ProductApi.Domain.csproj ProductApiSolution/ProductApi.Domain/
COPY ProductApiSolution/ProductApi.Application/ProductApi.Application.csproj ProductApiSolution/ProductApi.Application/
COPY ProductApiSolution/ProductApi.Infrastructure/ProductApi.Infrastructure.csproj ProductApiSolution/ProductApi.Infrastructure/

RUN dotnet restore ProductApiSolution/ProductApi.Presentation/ProductApi.Presentation.csproj

COPY . .

RUN dotnet publish ProductApiSolution/ProductApi.Presentation/ProductApi.Presentation.csproj \ 
-c Release \
-o /app/publish \ 
/p:UserAppHost=false 

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet","ProductApi.Presentation.dll"]




