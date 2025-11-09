# .NET SDK ile build işlemi için
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Çözüm dosyasını kopyala
COPY ["Presentation/KafeApi.API/KafeApi.API.csproj", "Presentation/KafeApi.API/"]
COPY ["core/KafeApi.Application/KafeApi.Application.csproj", "core/KafeApi.Application/"]
COPY ["core/KafeApi.Domain/KafeApi.Domain.csproj", "core/KafeApi.Domain/"]
COPY ["Infrastructure/KafeApi.Persistance/KafeApi.Persistance.csproj", "Infrastructure/KafeApi.Persistance/"]

# Bağımlılıkları indir
RUN dotnet restore "Presentation/KafeApi.API/KafeApi.API.csproj"

# Kaynak kodlarını kopyala
COPY . .

WORKDIR "/src/Presentation/KafeApi.API"
RUN dotnet build "KafeApi.API.csproj" -c Release -o /app/build

# Publish
RUN dotnet publish "KafeApi.API.csproj" -c Release -o /app/publish

# Çalışma aşaması
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "KafeApi.API.dll"]
