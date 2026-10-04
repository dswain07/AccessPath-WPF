# AccessPath

AccessPath is a C# WPF desktop application for managing campus buildings, accessible routes, building accessibility features, and route reports. The project is designed around the University of South Carolina campus and demonstrates a multi-project desktop application connected to a SQL Server LocalDB database.

## Features

- Single-window WPF interface with page-style navigation
- Building management with Create, Read, Update, and Delete operations
- Route management with Create, Read, Update, and Delete operations
- Building accessibility management with Create, Read, Update, and Delete operations
- Building search by building name and building type
- Existing-address selection for building records
- Administration view for reviewing route reports and updating report status
- Admin authentication using PBKDF2 password hashing
- Light and dark application themes
- Live theme switching without restarting the application
- Input validation and confirmation dialogs
- SQL Server foreign-key relationships for relational data integrity

## Application Sections

### Buildings

The Buildings section allows users to:

- View campus buildings
- Search by building name or building type
- Add a building
- Update a selected building
- Delete a selected building
- Select a valid address from the Addresses table

### Manage Routes

The Routes section allows users to:

- View existing campus routes
- Select starting and destination buildings
- Add, update, and delete routes
- Assign route status, distance, estimated travel time, and descriptions

### Accessibility

The Accessibility section manages accessibility features associated with campus buildings and supports full Create, Read, Update, and Delete functionality.

### Administration

The Administration section displays submitted route reports and allows an authenticated administrator to update report statuses.

Supported statuses include:

- Pending
- Reviewed
- Resolved
- Rejected

Administration is protected by username/password authentication. Password verification uses PBKDF2 hashing and the authenticated account must have the `Admin` role.

### Settings

The Settings section allows the application theme to be changed between Light and Dark. Theme changes are applied immediately without restarting AccessPath.

## Architecture

AccessPath is separated into two Visual Studio projects.

### AccessPath.UI

Contains the WPF presentation layer and application shell.

Important components include:

- `MainWindow.xaml` - application shell and navigation
- `Views/BuildingsView.xaml`
- `Views/RoutesView.xaml`
- `Views/AccessibilityView.xaml`
- `Views/AdministrationView.xaml`
- `Views/SettingsView.xaml`
- `Views/AdminLoginView.xaml`
- `Themes/LightTheme.xaml`
- `Themes/DarkTheme.xaml`
- `Services/ThemeManager.cs`

`MainWindow` contains the navigation interface and loads the selected view into its main content area.

### AccessPath.Data

A separate Class Library containing the data-access layer.

It contains:

- Database connection logic
- Data models
- Repository classes
- Password hashing / verification utilities
- SQL Server operations using `Microsoft.Data.SqlClient`

The architecture is:

```text
AccessPath.UI
     |
     v
AccessPath.Data
     |
     v
Repositories / Security / Microsoft.Data.SqlClient
     |
     v
SQL Server LocalDB
```

## Database

The AccessPath database contains seven tables, satisfying the graduate requirement of five or more tables.

Database scripts are stored in:

```text
Scripts/
├── DDL.sql
└── DML.sql
```

Run the scripts in this order when recreating the database:

1. `DDL.sql`
2. `DML.sql`

The seed data includes an administrator account suitable for demonstrating the protected Administration view. The repository stores a password hash, not a plaintext password.

## CRUD Implementation

| Entity | Create | Read | Update | Delete |
| --- | --- | --- | --- | --- |
| Buildings | Yes | Yes | Yes | Yes |
| Routes | Yes | Yes | Yes | Yes |
| Building Accessibility | Yes | Yes | Yes | Yes |

## Search

The Buildings section supports text-based search across two building fields:

- Building name
- Building type

## Authentication

The optional Administration password feature was implemented using PBKDF2 with a random salt. A stored password uses the following general format:

```text
PBKDF2$iterations$salt$hash
```

The application verifies the entered password against the stored hash and then checks that the authenticated user has the `Admin` role before loading the Administration view.

## Theme Support

AccessPath uses WPF ResourceDictionaries:

```text
AccessPath.UI/Themes/LightTheme.xaml
AccessPath.UI/Themes/DarkTheme.xaml
```

`ThemeManager` replaces the active resource dictionary at runtime, allowing Light/Dark switching without restarting the application.

## Technology Stack

- C#
- WPF
- .NET 10
- SQL Server LocalDB
- Microsoft.Data.SqlClient
- XAML
- Git
- GitHub
- Visual Studio

## Project Structure

```text
AccessPath-WPF/
│
├── AccessPath.Data/
│   ├── Database/
│   ├── Models/
│   ├── Repositories/
│   ├── Security/
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

1. Clone the repository.
2. Open `AccessPath.slnx` in Visual Studio.
3. Ensure SQL Server LocalDB is installed and available.
4. Execute `Scripts/DDL.sql`.
5. Execute `Scripts/DML.sql`.
6. Verify the connection string in `AccessPath.Data/Database/DatabaseConnection.cs`.
7. Set `AccessPath.UI` as the startup project if necessary.
8. Build the solution.
9. Run the application.


## Academic Project

AccessPath was developed as a WPF database application for CSCE 547 Windows Programming.

## Author

Divyashanu Swain
