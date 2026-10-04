USE AccessPath;
GO


/* =========================================================
   ACCESSPATH - DML SCRIPT
   Populates the AccessPath database with development data.
   ========================================================= */


/* =========================================================
   1. ADDRESSES
   ========================================================= */

INSERT INTO Addresses
(
    Street,
    City,
    State,
    Zipcode
)
VALUES
    ('1322 Greene Street', 'Columbia', 'SC', '29208'),
    ('300 Main Street', 'Columbia', 'SC', '29208'),
    ('301 Main Street', 'Columbia', 'SC', '29208'),
    ('817 Henderson Street', 'Columbia', 'SC', '29208'),
    ('1705 College Street', 'Columbia', 'SC', '29208'),
    ('1014 Greene Street', 'Columbia', 'SC', '29208');

GO


/* =========================================================
   2. BUILDINGS
   ========================================================= */

INSERT INTO Buildings
(
    BuildingName,
    BuildingType,
    OpeningHours,
    ClosingHours,
    BuildingLatitude,
    BuildingLongitude,
    BuildingDescription,
    AddressID
)
VALUES
(
    'Thomas Cooper Library',
    'Library',
    '07:30:00',
    '02:00:00',
    33.995040,
    -81.028080,
    'The primary library for students at the University of South Carolina.',
    1
),
(
    'Swearingen Engineering Center',
    'Academic',
    NULL,
    NULL,
    33.989880,
    -81.027870,
    'Engineering and computing academic facility.',
    3
),
(
    'Gambrell Hall',
    'Academic',
    NULL,
    NULL,
    33.998210,
    -81.023700,
    'Academic building containing classrooms and university departments.',
    4
),
(
    'Close-Hipp Building',
    'Academic',
    NULL,
    NULL,
    34.000410,
    -81.023340,
    'Academic and administrative building containing classrooms, offices, and student services.',
    5
),
(
    'Darla Moore School of Business',
    'Academic',
    NULL,
    NULL,
    33.994490,
    -81.033480,
    'Home of the University of South Carolina Darla Moore School of Business.',
    6
),
(
    '300 Main Street Building',
    'Academic',
    NULL,
    NULL,
    33.991000,
    -81.028000,
    'University of South Carolina academic building located at 300 Main Street.',
    2
);

GO


/* =========================================================
   3. USERS
   NOTE:
   These are development-only placeholder password hashes.
   Real passwords should never be stored as plain text.
   ========================================================= */

INSERT INTO Users
(
    Username,
    PasswordHash,
    Theme,
    UserRole
)
VALUES
    (
        'admin',
        'PBKDF2$600000$eLA16rfLhVB0FC0MaNVn4Q==$fEvbMO6u44Oa1vVe4kmws3iTEIT1SNHNbi8bJznVK/w=',
        'Dark',
        'Admin'
    ),
    (
        'student1',
        'DEVELOPMENT_HASH_PLACEHOLDER',
        'Light',
        'Student'
    ),
    (
        'student2',
        'DEVELOPMENT_HASH_PLACEHOLDER',
        'Dark',
        'Student'
    ),
    (
        'staff1',
        'DEVELOPMENT_HASH_PLACEHOLDER',
        'Light',
        'Staff'
    );

GO


/* =========================================================
   4. ROUTES

   Building IDs:
   1 = Thomas Cooper Library
   2 = Swearingen Engineering Center
   3 = Gambrell Hall
   4 = Close-Hipp Building
   5 = Darla Moore School of Business
   6 = 300 Main Street Building

   Route distances/times are development seed estimates.
   ========================================================= */

INSERT INTO Routes
(
    EstimatedMinutes,
    RouteDistance,
    RouteStatus,
    RouteDescription,
    StartBuildingID,
    DestinationBuildingID
)
VALUES
(
    12,
    0.60,
    'Active',
    'Route from Thomas Cooper Library to Swearingen Engineering Center.',
    1,
    2
),
(
    8,
    0.40,
    'Active',
    'Route from Thomas Cooper Library to Gambrell Hall.',
    1,
    3
),
(
    10,
    0.50,
    'Active',
    'Route from Thomas Cooper Library to Close-Hipp Building.',
    1,
    4
),
(
    15,
    0.80,
    'Active',
    'Route from Thomas Cooper Library to Darla Moore School of Business.',
    1,
    5
),
(
    9,
    0.45,
    'Active',
    'Route from Gambrell Hall to Close-Hipp Building.',
    3,
    4
);

GO


/* =========================================================
   5. BUILDING ACCESSIBILITY

   Development/sample accessibility data.
   ========================================================= */

INSERT INTO BuildingAccessibility
(
    AccessibilityFeature,
    AccessibilityDescription,
    BuildingID
)
VALUES

    /* Thomas Cooper Library */

    (
        'Accessible Entrance',
        'Building provides an entrance designed for wheelchair accessibility.',
        1
    ),
    (
        'Elevator',
        'Elevator access is available for movement between floors.',
        1
    ),
    (
        'Accessible Restroom',
        'Accessible restroom facilities are available within the building.',
        1
    ),

    /* Swearingen Engineering Center */

    (
        'Accessible Entrance',
        'Building provides an accessible entrance for users with mobility needs.',
        2
    ),
    (
        'Elevator',
        'Elevator access is available for movement between floors.',
        2
    ),
    (
        'Wheelchair Ramp',
        'A wheelchair-accessible ramp provides an alternative to stairs.',
        2
    ),

    /* Gambrell Hall */

    (
        'Accessible Entrance',
        'Building provides an entrance designed for wheelchair accessibility.',
        3
    ),
    (
        'Elevator',
        'Elevator access is available for movement between floors.',
        3
    ),

    /* Close-Hipp Building */

    (
        'Accessible Entrance',
        'Building provides an accessible entrance for users with mobility needs.',
        4
    ),
    (
        'Elevator',
        'Elevator access is available for movement between floors.',
        4
    ),
    (
        'Accessible Restroom',
        'Accessible restroom facilities are available within the building.',
        4
    ),

    /* Darla Moore School of Business */

    (
        'Accessible Entrance',
        'Building provides an entrance designed for wheelchair accessibility.',
        5
    ),
    (
        'Automatic Door',
        'An automatic door assists users entering the building.',
        5
    ),
    (
        'Elevator',
        'Elevator access is available for movement between floors.',
        5
    ),
    (
        'Accessible Restroom',
        'Accessible restroom facilities are available within the building.',
        5
    ),

    /* 300 Main Street Building */

    (
        'Accessible Entrance',
        'Building provides an accessible entrance for users with mobility needs.',
        6
    ),
    (
        'Elevator',
        'Elevator access is available for movement between floors.',
        6
    );

GO


/* =========================================================
   6. SAVED ROUTES

   User IDs:
   1 = admin
   2 = student1
   3 = student2
   4 = staff1

   SavedDate is automatically generated by GETDATE().
   ========================================================= */

INSERT INTO SavedRoutes
(
    RouteID,
    UserID
)
VALUES
    (1, 2),  -- student1: Thomas Cooper -> Swearingen
    (4, 2),  -- student1: Thomas Cooper -> Darla Moore
    (2, 3),  -- student2: Thomas Cooper -> Gambrell
    (5, 3),  -- student2: Gambrell -> Close-Hipp
    (3, 4);  -- staff1: Thomas Cooper -> Close-Hipp

GO


/* =========================================================
   7. ROUTE REPORTS

   Status defaults to Pending.
   Report date is automatically generated by GETDATE().
   ========================================================= */

INSERT INTO RouteReports
(
    RouteReportType,
    RouteReportDescription,
    RouteID,
    UserID
)
VALUES
(
    'Construction',
    'Construction is partially blocking the normal pedestrian path.',
    1,
    2
),
(
    'Sidewalk Issue',
    'A section of the sidewalk is uneven and may be difficult to navigate.',
    2,
    3
),
(
    'Temporary Obstruction',
    'An obstruction is reducing the available width of the path.',
    3,
    2
),
(
    'Construction',
    'Construction activity may require pedestrians to use an alternate path.',
    4,
    4
),
(
    'Accessibility Issue',
    'Part of the route may be difficult for wheelchair users to navigate.',
    5,
    3
);

GO


/* =========================================================
   VERIFICATION QUERIES
   ========================================================= */

SELECT * FROM Addresses;
SELECT * FROM Buildings;
SELECT * FROM Users;
SELECT * FROM Routes;
SELECT * FROM BuildingAccessibility;
SELECT * FROM SavedRoutes;
SELECT * FROM RouteReports;

GO