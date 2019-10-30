ARG ASPNETCORE_RUNTIME=3.0
ARG ASPNETCORE_SDK=3.0

FROM mcr.microsoft.com/dotnet/core/sdk:${ASPNETCORE_SDK} as build

WORKDIR /app
COPY . .
RUN dotnet publish ./GiftCircle.csproj -o ./out -c Release

FROM mcr.microsoft.com/dotnet/core/aspnet:${ASPNETCORE_RUNTIME}
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "GiftCircle.dll"]