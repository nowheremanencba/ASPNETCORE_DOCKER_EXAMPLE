FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 55616
EXPOSE 44308

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY StudentsExampleNETMVCDocker/StudentsExampleNETMVCDocker.csproj StudentsExampleNETMVCDocker/
RUN dotnet restore StudentsExampleNETMVCDocker/StudentsExampleNETMVCDocker.csproj
COPY . .
WORKDIR /src/StudentsExampleNETMVCDocker
RUN dotnet build StudentsExampleNETMVCDocker.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish StudentsExampleNETMVCDocker.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "StudentsExampleNETMVCDocker.dll"]
