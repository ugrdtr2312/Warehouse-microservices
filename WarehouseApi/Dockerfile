FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS publish
WORKDIR /src
COPY ./API/ /src/API/
COPY ./BLL/ /src/BLL/
COPY ./DAL/ /src/DAL/
WORKDIR /src/API
RUN dotnet restore
RUN dotnet publish "API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
EXPOSE 80