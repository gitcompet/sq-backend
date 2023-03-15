FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./SkillQuizWebApi/*.csproj ./SkillQuizWebApi/
COPY ./Business_Logic_Layer/*.csproj ./Business_Logic_Layer/
COPY ./Data_Access_Layer/*.csproj ./Data_Access_Layer/
RUN dotnet restore SkillQuizWebApi/SkillQuizWebApi.csproj

# Copy everything else and build
COPY ./SkillQuizWebApi ./SkillQuizWebApi/
COPY ./Business_Logic_Layer ./Business_Logic_Layer/
COPY ./Data_Access_Layer ./Data_Access_Layer/
WORKDIR /app/SkillQuizWebApi
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/SkillQuizWebApi/out .
# ENV ASPNETCORE_URLS=http://+:$PORT_API
EXPOSE 80
ENTRYPOINT ["dotnet", "SkillQuizWebApi.dll"]
