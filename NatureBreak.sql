USE [master]
GO

IF db_id('NatureBreaker') IS NOT NULL
BEGIN
  ALTER DATABASE [NatureBreaker] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
  DROP DATABASE [NatureBreaker]
END
GO

CREATE DATABASE [NatureBreaker]
GO

USE [NatureBreaker]
GO

CREATE TABLE [User] (
  [Id] INTEGER PRIMARY KEY IDENTITY(1, 1),
  [FirebaseUserId] VARCHAR(255) UNIQUE NOT NULL,
  [FirstName] VARCHAR(25) NOT NULL,
  [Email] VARCHAR(255) NOT NULL,
  [ProfileImage] VARCHAR(255) NOT NULL,
  [IsAdmin] BIT NOT NULL DEFAULT 0
)
GO

CREATE TABLE [NatureType] (
  [Id] INTEGER PRIMARY KEY IDENTITY(1, 1),
  [NatureTypeName] VARCHAR(50) NOT NULL
)
GO

CREATE TABLE [Video] (
  [Id] INTEGER PRIMARY KEY IDENTITY(1, 1),
  [Season] VARCHAR(7) NOT NULL,
  [NatureTypeId] INTEGER NOT NULL,
  [UserId] INTEGER NOT NULL,
  [ClosestMajorCity] VARCHAR(255) NOT NULL,
  [VideoName] VARCHAR(255) NOT NULL,
  [VideoUrl] VARCHAR(255) NOT NULL,

  CONSTRAINT FK_Video_User FOREIGN KEY (UserId) REFERENCES [User](Id),
  CONSTRAINT FK_Video_NatureType FOREIGN KEY (NatureTypeId) REFERENCES NatureType(Id)
)
GO


CREATE TABLE [FavoriteVideos] (
  [Id] INTEGER PRIMARY KEY IDENTITY(1, 1),
  [UserId] INTEGER NOT NULL,
  [VideoId] INTEGER NOT NULL,

  CONSTRAINT FK_FavoriteVideos_Video FOREIGN KEY (VideoId) REFERENCES Video(Id),
  CONSTRAINT FK_FavoriteVideos_User FOREIGN KEY (UserId) REFERENCES [User](Id)
)
GO



ALTER TABLE [FavoriteVideos] ADD FOREIGN KEY ([UserId]) REFERENCES [User] ([Id])
GO

ALTER TABLE [NatureType] ADD FOREIGN KEY ([Id]) REFERENCES [Video] ([NatureTypeId])
GO

ALTER TABLE [FavoriteVideos] ADD FOREIGN KEY ([VideoId]) REFERENCES [Video] ([Id])
GO

ALTER TABLE [Video] ADD FOREIGN KEY ([UserId]) REFERENCES [User] ([Id])
GO

---------------------------------------------------------------------------

-- create good seed data using the Streamish as a guide. ohhh boy. go slow and proofread. no need to rush, m8

--^ note: firebaseUserId is gotten from creating a new firebase project via firebase.com. you can register the uers from there, and then grab the userId from there. you don't need a password. 
--! you will not get these passwords back so keep them here. 
--* user 1 password: pearlzep98
--* user 2 password: ihatehumans123
--* user 3 password: butterflyinthesky

--* TODO: UPDATE USER INTERST STATEMENTS TO INCLUDE NEW ISADMIN COLUMN VALUES
SET IDENTITY_INSERT [User] ON
INSERT INTO [User]
    ([Id], [FirebaseUserId], [FirstName], [Email], [ProfileImage], [IsAdmin])
VALUES
    (1, 'AGycj79VzTPpQYZLEzRYWBYZpo22','Reggie', 'reggie@williams.com', 'https://i0.wp.com/www.beyondthestagemagazine.com/wp-content/uploads/2019/11/reggie1.gif?resize=500%2C640&ssl=1', 1),

    (2, 'dCPTASc5rvOAm93iHFNJSgOhRRl1','Mononoke-Hime', 'wolfgirl@mononoke.com', 'https://res.cloudinary.com/jerrick/image/upload/v1621006227/609e9793c99922001ef7cd9a.gif', 0),

    (3, 'fUQIlihnadbLXfB9OjTJJf9cmdd2' , 'Kiki', 'kiki@kikisdeliveryservice.com', 'https://media.tenor.com/eTLScbyWqp8AAAAd/kikis-delivery-service-ghibli.gif', 0);
SET IDENTITY_INSERT [User] OFF


SET IDENTITY_INSERT [NatureType] ON 
INSERT INTO [NatureType]
    ([Id], [NatureTypeName])
VALUES
    (1, 'Moving Water'),
    (2, 'Mountains'),
    (3, 'Rainfall'),
    (4, 'Tree Talk'),
    (5, 'Skyscape'),
    (6, 'Woodland'),
    (7, 'Snow'),
    (8, 'Ice'),
    (9, 'Wildlife'),
    (10, 'Beach');
    SET IDENTITY_INSERT [NatureType] OFF


SET IDENTITY_INSERT [Video] ON
INSERT INTO [Video]
    ([Id], [Season], [NatureTypeId], [UserId],[ClosestMajorCity], [VideoName],[VideoUrl])
VALUES 

--! make sure to create a row for each Nature type on each video. This way, they can be called by individual criteria at the search phase, AND "${user} likes this" will be implementable
--! also remmebr to keep incrementing the Id
    --*these these 6 rows are 2 videos for KIKI 
    (1, 'Winter', 2, 3, 'Denver','Nature Breaks - Mountains 002', 'https://www.youtube.com/embed/2AsNyz60D5Q'),
    (2, 'Winter', 5, 3, 'Denver','Nature Breaks - Mountains 002', 'https://www.youtube.com/embed/2AsNyz60D5Q' ),
    (3, 'Winter', 7, 3, 'Denver','Nature Breaks - Mountains 002', 'https://www.youtube.com/embed/2AsNyz60D5Q' ),

    (4, 'Spring', 1, 3, 'Denver','Nature Breaks - Mountains 003 - Horsetooth Reservoir', 'https://www.youtube.com/embed/RP0qMLrVH4s'),
    (5, 'Spring', 2, 3, 'Denver','Nature Breaks - Mountains 003 - Horsetooth Reservoir', 'https://www.youtube.com/embed/RP0qMLrVH4s'),
    (6, 'Spring', 5, 3, 'Denver','Nature Breaks - Mountains 003 - Horsetooth Reservoir', 'https://www.youtube.com/embed/RP0qMLrVH4s'),

    --* todo: at least 2 videos for Mononoke-Hime
    
    (7, 'Fall', 4, 2, 'Nashville', 'Nature Breaks - Tree Talk 001', 'https://www.youtube.com/embed/uihTWvgRGEQ'),
    (8, 'Fall', 5, 2, 'Nashville', 'Nature Breaks - Tree Talk 001', 'https://www.youtube.com/embed/uihTWvgRGEQ'),

    -- tree talk covered
    (9, 'Spring', 4, 2, 'Ajaccio', 'Nature Breaks - Tree Talk 002', 'https://www.youtube.com/embed/mfrBOkFaado'),
    (10, 'Spring', 5, 2, 'Ajaccio', 'Nature Breaks - Tree Talk 002', 'https://www.youtube.com/embed/mfrBOkFaado'),

    --* todo make videos for reggie that ghave teh rest of the nature types covered. videos for Reggie 
    -- these cover Ice, Mouintains and Moving Water
    (11, 'Winter', 2, 1, 'Denver', 'Nature Breaks - Moving Water 001', 'https://www.youtube.com/embed/Jkp3WX3ujRs'),
    (12, 'Winter', 1, 1, 'Denver', 'Nature Breaks - Moving Water 001', 'https://www.youtube.com/embed/Jkp3WX3ujRs'),
    (13, 'Winter', 8, 1, 'Denver', 'Nature Breaks - Moving Water 001', 'https://www.youtube.com/embed/Jkp3WX3ujRs'),
   
   -- these refer to the same rainfall video:
    (14, 'Summer', 3, 1, 'Nashville', 'Nature Breaks - Rainfall 001', 'https://www.youtube.com/embed/isJKhbKapeY'),
    (15, 'Summer', 4, 1, 'Nashville', 'Nature Breaks - Rainfall 001', 'https://www.youtube.com/embed/isJKhbKapeY'),
    
    -- these refer to the same beach vid
    (16, 'Summer', 10, 1, 'Ventura', 'Nature Breaks - Beach 001', 'https://www.youtube.com/embed/skdGbN2CyNY'),
    (17, 'Summer', 5, 1, 'Ventura', 'Nature Breaks - Beach 001', 'https://www.youtube.com/embed/skdGbN2CyNY'),
    (18, 'Summer', 1, 1, 'Ventura', 'Nature Breaks - Beach 001', 'https://www.youtube.com/embed/skdGbN2CyNY'),
    
    -- these are for the snowfall & woodland video
    (19, 'Winter', 7, 1, 'Nashville', 'Nature Breaks - Snow 001', 'https://youtube.com/embed/CnXfRfKfeug'),
    (20, 'Winter', 6, 1, 'Nashville', 'Nature Breaks - Snow 001', 'https://youtube.com/embed/CnXfRfKfeug'),
   
   
  -- these are teh wildlife video 
    (21, 'Winter', 9, 1, 'Denver', 'Nature Breaks - Wildlife 001', 'https://www.youtube.com/embed/ms-G4IeZwAI'),
    (22, 'Winter', 7, 1, 'Denver', 'Nature Breaks - Wildlife 001', 'https://www.youtube.com/embed/ms-G4IeZwAI')

SET IDENTITY_INSERT [Video] OFF


        