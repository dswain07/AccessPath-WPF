USE AccessPath;
GO

CREATE TABLE Addresses
(
	AddressID INT IDENTITY(1,1) PRIMARY KEY,
	Street VARCHAR(100) NOT NULL,
	City VARCHAR(50) NOT NULL,
	State VARCHAR(50) NOT NULL,
	Zipcode CHAR(5) NOT NULL
);
GO

CREATE TABLE Buildings
(
	BuildingID INT IDENTITY(1,1) PRIMARY KEY,
	BuildingName VARCHAR(100) NOT NULL,
	BuildingType VARCHAR(50) NOT NULL,
	OpeningHours TIME NULL,
	ClosingHours TIME NULL,
	BuildingLatitude DECIMAL(9,6) NOT NULL,
	BuildingLongitude DECIMAL(9,6) NOT NULL,
	BuildingDescription VARCHAR(200) NULL,
	AddressID INT NOT NULL,

	FOREIGN KEY (AddressID)
		REFERENCES Addresses(AddressID)
);
GO

CREATE TABLE Users
(
	UserID INT IDENTITY(1,1) PRIMARY KEY,
	Username VARCHAR(20) NOT NULL UNIQUE,
	PasswordHash VARCHAR(255) NOT NULL,
	Theme VARCHAR(10) NOT NULL DEFAULT 'Light',
	UserRole VARCHAR(20) NOT NULL DEFAULT 'Student',

	CHECK (Theme IN ('Light', 'Dark')),
	CHECK (UserRole IN ('Admin', 'Student', 'Staff'))
);
GO

CREATE TABLE Routes
(
	RouteID INT IDENTITY(1,1) PRIMARY KEY,
	EstimatedMinutes INT NOT NULL,
	RouteDistance DECIMAL(9,6) NOT NULL,
	RouteStatus VARCHAR(50) NOT NULL,
	RouteDescription VARCHAR(200) NOT NULL,
	StartBuildingID INT NOT NULL,
	DestinationBuildingID INT NOT NULL,

	FOREIGN KEY (StartBuildingID)
		REFERENCES Buildings(BuildingID),

	FOREIGN KEY (DestinationBuildingID)
		REFERENCES Buildings(BuildingID),

	CHECK (StartBuildingID <> DestinationBuildingID),
	CHECK (EstimatedMinutes > 0),
	CHECK (RouteDistance > 0),
	CHECK (RouteStatus IN ('Active', 'Temporarily Closed', 'Under Maintenance', 'Permanently Closed'))
);
GO

CREATE TABLE BuildingAccessibility     
(     
    AccessibilityID INT IDENTITY(1,1) PRIMARY KEY,          
    AccessibilityFeature VARCHAR(50) NOT NULL,         
    AccessibilityDescription VARCHAR(200) NULL,  
    BuildingID INT NOT NULL,        

    FOREIGN KEY (BuildingID)  
        REFERENCES Buildings(BuildingID)
);
GO

CREATE TABLE SavedRoutes      
(      
    SavedRouteID INT IDENTITY(1,1) PRIMARY KEY,             
    RouteID INT NOT NULL,
    UserID INT NOT NULL,
    SavedDate DATETIME NOT NULL DEFAULT GETDATE(),

    FOREIGN KEY (RouteID)   
        REFERENCES Routes(RouteID),

    FOREIGN KEY (UserID)   
        REFERENCES Users(UserID),

    UNIQUE (UserID, RouteID)
);
GO

CREATE TABLE RouteReports 
( 
    RouteReportID INT IDENTITY(1,1) PRIMARY KEY,
    RouteReportType VARCHAR(50) NOT NULL,
    RouteReportDescription VARCHAR(200) NULL,
    RouteReportStatus VARCHAR(50) NOT NULL DEFAULT 'Pending',
    RouteReportDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    RouteID INT NOT NULL,
    UserID INT NOT NULL,

    FOREIGN KEY (RouteID)
        REFERENCES Routes(RouteID),

    FOREIGN KEY (UserID)
        REFERENCES Users(UserID),

    CHECK (RouteReportStatus IN ('Pending', 'Reviewed', 'Resolved', 'Rejected'))
);
GO