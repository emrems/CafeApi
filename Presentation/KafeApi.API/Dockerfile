# Hata ayıklama kapsayıcınızı özelleştirme ve Visual Studio’nun daha hızlı hata ayıklama için görüntülerinizi derlemek üzere bu Dockerfile'ı nasıl kullandığı hakkında bilgi edinmek için https://aka.ms/customizecontainer sayfasına bakın.

# Bu aşama, VS'den hızlı modda çalıştırıldığında kullanılır (Hata ayıklama yapılandırması için varsayılan olarak ayarlıdır)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Bu aşama, hizmet projesini oluşturmak için kullanılır
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Presentation/KafeApi.API/KafeApi.API.csproj", "Presentation/KafeApi.API/"]
COPY ["core/KafeApi.Application/KafeApi.Application.csproj", "core/KafeApi.Application/"]
COPY ["core/KafeApi.Domain/KafeApi.Domain.csproj", "core/KafeApi.Domain/"]
COPY ["Infrastructure/KafeApi.Persistance/KafeApi.Persistance.csproj", "Infrastructure/KafeApi.Persistance/"]
RUN dotnet restore "./Presentation/KafeApi.API/KafeApi.API.csproj"
COPY . .
WORKDIR "/src/Presentation/KafeApi.API"
RUN dotnet build "./KafeApi.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Bu aşama, son aşamaya kopyalanacak hizmet projesini yayımlamak için kullanılır
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./KafeApi.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Bu aşama üretimde veya VS'den normal modda çalıştırıldığında kullanılır (Hata Ayıklama yapılandırması kullanılmazken varsayılan olarak ayarlıdır)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KafeApi.API.dll"]