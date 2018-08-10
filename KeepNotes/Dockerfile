FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 51418


FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /KeepNotes
COPY *.csproj ./
RUN dotnet restore 
COPY . .
WORKDIR /KeepNotes
RUN dotnet build GoogleKeepNotes.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish GoogleKeepNotes.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "GoogleKeepNotes.dll"]