# API Handling .NET

This project is the .NET 8 equivalent of the Java Spring Boot API handling demonstration project.

## OpenAPI/Swagger File Generation

Starting from step01, the project uses Microsoft's built-in OpenAPI generation capabilities to automatically create swagger.json files during build time.

### Build-Time Generation (Recommended)

The project is configured with `Microsoft.Extensions.ApiDescription.Server` to automatically generate the OpenAPI specification during build:

```xml
<PackageReference Include="Microsoft.Extensions.ApiDescription.Server" Version="8.0.0">
  <PrivateAssets>all</PrivateAssets>
  <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
</PackageReference>

<PropertyGroup>
  <OpenApiDocumentsDirectory>$(MSBuildProjectDirectory)</OpenApiDocumentsDirectory>
  <OpenApiGenerateDocuments>true</OpenApiGenerateDocuments>
  <OpenApiGenerateDocumentsOnBuild>true</OpenApiGenerateDocumentsOnBuild>
</PropertyGroup>
```

This creates a `swagger.json` file in the project root automatically when you run `dotnet build`.

### Runtime Access

The Swagger JSON is also available at runtime via the `/swagger/v1/swagger.json` endpoint when the application runs, and the Swagger UI is available at the root URL (`/`) when running in development mode.

### No External Dependencies Required

This approach eliminates the need for external CLI tools like Swashbuckle.AspNetCore.Cli, using only built-in .NET tooling for OpenAPI document generation.