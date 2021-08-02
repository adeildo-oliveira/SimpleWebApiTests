FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["SampleLinearSearch/SampleLinearSearch.csproj", "SampleLinearSearch/"]
RUN dotnet restore "SampleLinearSearch/SampleLinearSearch.csproj"
COPY . .
WORKDIR "/src/SampleLinearSearch"

FROM build AS publish
RUN dotnet publish "SampleLinearSearch.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SampleLinearSearch.dll"]