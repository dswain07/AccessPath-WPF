# AccessPath

AccessPath is a C# WPF desktop application for managing campus
buildings, accessible routes, building accessibility features, and route
reports. The project is designed around the University of South Carolina
campus and demonstrates a multi-project WPF application connected to a
SQL Server LocalDB database.

## Features

-   Single-window WPF interface with page-style navigation
-   Building management with Create, Read, Update, and Delete operations
-   Route management with Create, Read, Update, and Delete operations
-   Building accessibility management with Create, Read, Update, and
    Delete operations
-   Building search by building name or building type
-   Address selection using existing database addresses
-   Administration view for reviewing route reports and updating report
    status
-   Light and dark application themes
-   Live theme switching without restarting the application
-   Input validation and confirmation messages for database operations
-   SQL Server foreign-key relationships for relational data integrity

## Application Sections

### Buildings

The Buildings section allows users to:

-   View campus buildings
-   Search by building name or building type
-   Add a building
-   Update a selected building
-   Delete a selected building
-   Select a valid address from the Addresses table

### Manage Routes

The Routes section allows users to:

-   View existing campus routes
-   Select starting and destination buildings
-   Add routes
-   Update routes
-   Delete routes
-   Assign route status, distance, estimated travel time, and
    descriptions

### Accessibility

The Accessibility section manages accessibility features associated with
campus buildings.

Examples include:

-   Accessible entrances
-   Elevators
-   Wheelchair ramps
-   Other building-specific accessibility information

The section supports full Create, Read, Update, and Delete
functionality.

### Administration

The Administration section displays submitted route reports and allows
an administrator to update report statuses.

Supported statuses include:

-   Pending
-   Reviewed
-   Resolved
-   Rejected

### Settings

The Settings section allows the application theme to be changed between:

-   Light
-   Dark

Theme changes are applied while the application is running and do not
require a restart.

## Architecture

AccessPath is separated into two main Visual Studio projects.

### AccessPath.UI

`AccessPath.UI` contains the WPF presentation layer.

Important components include:

-   `MainWindow.xaml` - application shell and navigation
-   `Views/BuildingsView.xaml`
-   `Views/RoutesView.xaml`
-   `Views/AccessibilityView.xaml`
-   `Views/AdministrationView.xaml`
-   `Views/SettingsView.xaml`
-   `Themes/LightTheme.xaml`
-   `Themes/DarkTheme.xaml`
-   `Services/ThemeManager.cs`

The application uses a single-window design. `MainWindow` contains the
navigation interface and loads the selected view into its main content
area.

### AccessPath.Data

`AccessPath.Data` is a separate Class Library containing the
application's data-access functionality.

It contains:

-   Database connection logic
-   Data models
-   Repository classes
-   SQL Server operations using `Microsoft.Data.SqlClient`

The UI project references the Data project rather than directly
executing SQL commands.

The overall architecture is:

``` text
AccessPath.UI
     |
     v
AccessPath.Data
     |
     v
Repositories / Microsoft.Data.SqlClient
     |
     v
SQL Server LocalDB
```

## Database

The AccessPath database contains seven related tables.

The project includes SQL scripts in the `Scripts` directory:

``` text
Scripts/
├── DDL.sql
└── DML.sql
```

### DDL.sql

Creates the AccessPath database structure, including tables, primary
keys, foreign keys, and other constraints.

### DML.sql

Populates the database with initial sample data used by the application.

To recreate the database, execute the scripts in this order:

1.  `DDL.sql`
2.  `DML.sql`

## Data Access

The application uses the repository pattern to separate database
operations from the WPF user interface.

Examples include:

-   `BuildingRepository`
-   `AddressRepository`
-   `RouteRepository`
-   `BuildingAccessibilityRepository`
-   `RouteReportRepository`

Parameterized SQL commands are used for database operations.

## CRUD Implementation

Full CRUD functionality is implemented for three database entities:

  Entity                   Create   Read   Update   Delete
  ------------------------ -------- ------ -------- --------
  Buildings                Yes      Yes    Yes      Yes
  Routes                   Yes      Yes    Yes      Yes
  Building Accessibility   Yes      Yes    Yes      Yes

## Search

The Buildings section supports text-based searching across two building
fields:

-   Building name
-   Building type

## Technology Stack

-   C#
-   WPF
-   .NET 10
-   SQL Server LocalDB
-   Microsoft.Data.SqlClient
-   XAML
-   Git
-   GitHub
-   Visual Studio

## Project Structure

``` text
AccessPath-WPF/
│
├── AccessPath.Data/
│   ├── Database/
│   ├── Models/
│   ├── Repositories/
│   └── AccessPath.Data.csproj
│
├── AccessPath.UI/
│   ├── Services/
│   ├── Themes/
│   ├── Views/
│   ├── App.xaml
│   ├── MainWindow.xaml
│   └── AccessPath.UI.csproj
│
├── Scripts/
│   ├── DDL.sql
│   └── DML.sql
│
├── AccessPath.slnx
├── .gitignore
└── README.md
```

## Running the Application

1.  Clone the repository.
2.  Open `AccessPath.slnx` in Visual Studio.
3.  Ensure SQL Server LocalDB is installed and available.
4.  Execute `Scripts/DDL.sql`.
5.  Execute `Scripts/DML.sql`.
6.  Verify the connection string in
    `AccessPath.Data/Database/DatabaseConnection.cs`.
7.  Set `AccessPath.UI` as the startup project if necessary.
8.  Build the solution.
9.  Run the application.

## Theme Support

AccessPath uses WPF resource dictionaries for application-wide styling:

``` text
AccessPath.UI/Themes/LightTheme.xaml
AccessPath.UI/Themes/DarkTheme.xaml
```

`ThemeManager` replaces the active resource dictionary at runtime,
allowing the application to switch themes without restarting.

## Repository

This repository contains the source code, database scripts, and project
files required to build and run AccessPath.

## Academic Project

AccessPath was developed as a WPF database application for CSCE 547
Windows Programming.
