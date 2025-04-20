FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY Lotto.WebApi/*.csproj ./Lotto.WebApi/
COPY Lotto.Data/*.csproj ./Lotto.Data/
COPY Lotto.Domain/*.csproj ./Lotto.Domain/
COPY Lotto.Service/*.csproj ./Lotto.Service/
RUN dotnet restore ./Lotto.WebApi/Lotto.WebApi.csproj
COPY Lotto.WebApi/ ./Lotto.WebApi/
COPY Lotto.Data/ ./Lotto.Data/
COPY Lotto.Domain/ ./Lotto.Domain/
COPY Lotto.Service/ ./Lotto.Service/
RUN dotnet publish ./Lotto.WebApi/Lotto.WebApi.csproj -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "Lotto.WebApi.dll"]