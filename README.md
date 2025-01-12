# Instrucciones para ejecutar el proyecto

## Test

Para ejecutar los test se debe reemplazar los archivos

1. ./Api/Api.sln

```sln

Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.5.002.0
MinimumVisualStudioVersion = 10.0.40219.1
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "Api", "Api.csproj", "{88DE57AC-AC3F-41AB-B8EC-AD832F02F98B}"
EndProject
Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "ApiTest", "..\ApiTest\ApiTest.csproj", "{B132370B-0ED7-4AC0-82D6-48627F35CFE1}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{88DE57AC-AC3F-41AB-B8EC-AD832F02F98B}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{88DE57AC-AC3F-41AB-B8EC-AD832F02F98B}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{88DE57AC-AC3F-41AB-B8EC-AD832F02F98B}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{88DE57AC-AC3F-41AB-B8EC-AD832F02F98B}.Release|Any CPU.Build.0 = Release|Any CPU
		{B132370B-0ED7-4AC0-82D6-48627F35CFE1}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{B132370B-0ED7-4AC0-82D6-48627F35CFE1}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{B132370B-0ED7-4AC0-82D6-48627F35CFE1}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{B132370B-0ED7-4AC0-82D6-48627F35CFE1}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {E268D6D7-AF29-419D-90A3-55F15BBDF1C7}
	EndGlobalSection
EndGlobal

```

2. .Api/appsettings.json

```json

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=LabDev;User Id=developer;Password=abc123ABC;Encrypt=False;"
  }
}

```

Comando en la carpeta ./Api

```bash
dotnet test
```


## Run


Para ejecutar el proyecto se debe reemplazar los archivos

1. ./Api/Api.sln

```sln

Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.5.002.0
MinimumVisualStudioVersion = 10.0.40219.1
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "Api", "Api.csproj", "{88DE57AC-AC3F-41AB-B8EC-AD832F02F98B}"
EndProject
# Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "ApiTest", "..\ApiTest\ApiTest.csproj", "{B132370B-0ED7-4AC0-82D6-48627F35CFE1}"
# EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{88DE57AC-AC3F-41AB-B8EC-AD832F02F98B}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{88DE57AC-AC3F-41AB-B8EC-AD832F02F98B}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{88DE57AC-AC3F-41AB-B8EC-AD832F02F98B}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{88DE57AC-AC3F-41AB-B8EC-AD832F02F98B}.Release|Any CPU.Build.0 = Release|Any CPU
# 		{B132370B-0ED7-4AC0-82D6-48627F35CFE1}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
# 		{B132370B-0ED7-4AC0-82D6-48627F35CFE1}.Debug|Any CPU.Build.0 = Debug|Any CPU
# 		{B132370B-0ED7-4AC0-82D6-48627F35CFE1}.Release|Any CPU.ActiveCfg = Release|Any CPU
# 		{B132370B-0ED7-4AC0-82D6-48627F35CFE1}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {E268D6D7-AF29-419D-90A3-55F15BBDF1C7}
	EndGlobalSection
EndGlobal

```

2. .Api/appsettings.json

```json

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=db;Database=LabDev;User Id=developer;Password=abc123ABC;Encrypt=False;"
  }
}

```

Comando en la carpeta raiz

```bash
docker-compose up --build -d
```

Despues de que inicien los servicios esperar 30 segundos a que el back se comunique con la base de datos y corran los scripts de migracion.

Acceder a la url http://localhost:8080/ para visualizar la aplicacion.