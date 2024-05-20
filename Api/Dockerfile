ARG VERSION=8.0

FROM mcr.microsoft.com/dotnet/sdk:$VERSION AS build-env

WORKDIR /app

ENV PATH="${PATH}:/root/.dotnet/tools"

ADD . .

RUN dotnet restore *.sln

RUN dotnet publish \
  -c Release \
  -o ./out \
  --no-restore

#-----------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

ENV APP_HOME=/app
WORKDIR $APP_HOME

ENV ASPNETCORE_URLS=http://*:5207

RUN ls -la

COPY --from=build-env --chown=app:app /app/out $APP_HOME
EXPOSE 5207 5207

USER 1000
ENTRYPOINT ["dotnet", "Api.dll"]



# docker build -t web-api .
# docker run -d -p 5207:5207 --name web-api web-api

# docker stop web-api
# docker rm web-api

# docker stop web-api && docker rm web-api && docker build -t web-api . && docker run -d -p 5207:5207 --name web-api web-api

