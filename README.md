# Integration Testing SQL Server
[![C#](https://img.shields.io/badge/Made%20with-C%23-239120.svg)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-5C2D91.svg)](https://dotnet.microsoft.com/)
[![MS SQL Server](https://img.shields.io/badge/Database-MS%20SQL%20Server-CC2927.svg)](https://www.microsoft.com/en-us/sql-server)
[![Docker](https://img.shields.io/badge/Powered%20by-Docker-2496ED.svg)](https://www.docker.com/)
[![NUnit](https://img.shields.io/badge/tested%20with-NUnit-22B2B0.svg)](https://nunit.org/)
[![xUnit](https://img.shields.io/badge/tested%20with-xUnit-5E1F87.svg)](https://xunit.net/)

### This is a test project for **Back-End Test Technologies** January 2024 Course @ SoftUni.
---
## Project Description
This repository contains a project designed for integration testing using SQL Server as the backend database.

## Introduction - LibroConsoleAPI
LibroConsoleAPI is a console application developed with .NET Core, enabling users to manage a library of books through CRUD operations performed on a SQL Server database.

## Key Features
This project demonstrates core software development principles like integration testing, database management, and console-based application architecture.
- **Create**: Add books to the database.
- **Read**: Retrieve details of books from the database.
- **Update**: Modify existing book information.
- **Delete**: Remove books from the database.

## Project Structure
- **LibroConsoleAPI**: The main project that serves as the application’s entry point.
- **Business**: Implements the core functionality of the application.
- **Common**: Contains validation rules and constraints for data.
- **Data**: Defines models representing the data managed by the application.
- **DataAccess**: Acts as a bridge between business logic and the data layer.
  
## Technologies Used
- .NET Core: The primary framework used for building the console application.
- SQL Server: The database management system used for storing book data.
- Entity Framework Core: An ORM (Object-Relational Mapper) for database interactions.
- xUnit: A unit-testing framework for .NET applications.
- nUnit: A unit-testing framework for .NET applications.
  
## Docker Integration
You can also run the SQL Server using a Docker image, which simplifies the setup and ensures consistency across different environments. To set up SQL Server with Docker, follow these steps:

1. **Pull the SQL Server Docker image**:

    ```bash
    docker pull mcr.microsoft.com/mssql/server
    ```

2. **Run the Docker container**:

    ```bash
    docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Your_password123' -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server
    ```

3. **Connect to the SQL Server**:

    - Use the connection string: `Server=localhost,1433;Database=YourDatabase;User Id=sa;Password=Your_password123;`
    - Replace `YourDatabase` with the name of your database.

For more information on using Docker with SQL Server, you can refer to the [official Docker documentation](https://hub.docker.com/_/microsoft-mssql-server) and the [SQL Server on Docker guide](https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-docker-container-deployment).

## Contributing
Contributions are welcome! If you have any improvements or bug fixes, feel free to open a pull request.

## License
This project is licensed under the [MIT License](LICENSE). See the [LICENSE](LICENSE) file for details.

## Contact
For any questions or suggestions, please open an issue in the repository.

---
### Happy Testing! 🚀
