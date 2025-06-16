# Ntech Modular Architecture

This project is a modular architecture built using ASP.NET Core 2.1. It is designed to be extensible and modular, allowing for easy integration of new modules and features.

## Project Structure

The project is organized into several key directories and projects:

- **src/**: Contains the main source code of the project.
  - **Ntech.WebHost/**: The main web application project that serves as the entry point for the application. It includes the `Startup.cs` and `Program.cs` files, which configure the application and its services.
  - **Ntech.Platform.CommonService/**: Contains common services used across the application.
  - **Ntech.Platform.Repository/**: Handles data access and repository patterns.
  - **Ntech.Infrastructure/**: Provides infrastructure services and utilities.
  - **Ntech.Modules.React/**: A module for React-based components.
  - **Ntech.Core.Server/**: Core server-side functionality and interfaces.
  - **Ntech.Contract.Entity/**: Defines entity contracts and interfaces.
  - **Ntech.Contract.Entity.SubDatabase/**: Contains sub-database entity contracts.
  - **Modules/**: Contains various modules that can be integrated into the application.
    - **Ntech.Modules.DashboardModule/**: A module for dashboard functionality.
    - **Ntech.Modules.Core/**: Core module functionality.
    - **Ntech.Modules.Api.Base/**: Base API module.
    - **Ntech.Modules.Angular/**: A module for Angular-based components.

## Dependencies

The project relies on several key packages:

- **Microsoft.AspNetCore.App**: Core ASP.NET Core packages.
- **Microsoft.AspNetCore.Identity.EntityFrameworkCore**: For identity management.
- **Autofac.Extensions.DependencyInjection**: For dependency injection.
- **Microsoft.AspNetCore.SpaServices.Extensions**: For SPA (Single Page Application) support.
- **Microsoft.Composition**: For composition and modularity.

## Configuration

The application is configured using `appsettings.json` and `appsettings.Development.json`. These files contain settings for database connections, logging, and other configurations.

## Running the Application

To run the application, ensure you have the following prerequisites:

- .NET Core SDK 2.1 or later.
- SQL Server (or another compatible database) with the connection string configured in `appsettings.json`.

1. Clone the repository.
2. Navigate to the `src/Ntech.WebHost` directory.
3. Run the following command to start the application:
   ```bash
   dotnet run
   ```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the LICENSE file for details.
