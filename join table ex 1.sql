 --SELECT v.Id, v.Title, v.Description, v.Url, v.DateCreated, v.UserProfileId,

 --                     up.Name, up.Email, up.DateCreated AS UserProfileDateCreated,
 --                     up.ImageUrl AS UserProfileImageUrl
                        
 --                FROM Video v 
 --                     JOIN UserProfile up ON v.UserProfileId = up.Id

 --SELECT fv.Id, fv.UserId, fv.VideoId,
 
 --v.Id, v.NatureTypeId, v.UserId, v.Season, v.VideoName, v.VideoUrl, v.ClosestMajorCity
 
 --FROM Video v
 --   JOIN FavoriteVideos fv on fv.VideoId = v.Id;

 Select * From [User]
 Select * From Video