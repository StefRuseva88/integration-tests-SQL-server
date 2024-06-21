# Integration-Testing-SQL-Server
![image](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![image](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![image](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white)
![image](https://img.shields.io/badge/Docker-2CA5E0?style=for-the-badge&logo=docker&logoColor=white)
### This is a test project for Back-End Test Technologies January 2024 Course @ SoftUni.
---
## Project Description
This repository contains a test project designed for practicing integration testing with SQL Server. 

## Introduction - LibroConsoleAPI
LibroConsoleAPI is a console-based application built using the .NET Core Framework that manages a collection of books. It allows users to perform various operations, such as creating, reading, updating, and deleting books in a SQL Server database.

## Key Features
- **Create**: Add new books to the database.
- **Read**: Retrieve details of books from the database.
- **Update**: Modify existing book information.
- **Delete**: Remove books from the database.
This application showcases fundamental software development practices, including integration testing, database management, and console-based application design.

## Project Structure
- **LibroConsoleAPI**: This is the primary project for the application, likely serving as the entry point for the API.
- **Business**: This layer is responsible for implementing the functionalities that the application is expected to provide.
- **Common**: Centralized place to define the rules and constraints related to data validation.
- **Data**: This layer of the application consists of models representing the data that the application will manage.
- **DataAccess**: The Data Access layer in the application serves as the bridge between the application's business logic and its data source.

## Technologies Used
- .NET Core: The primary framework used for building the console application.
- SQL Server: The database management system used for storing book data.
- Entity Framework Core: An ORM (Object-Relational Mapper) for database interactions.
- xUnit: A unit-testing framework for .NET applications
- nUnit: A unit-testing framework for .NET applications
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
