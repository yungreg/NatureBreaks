using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NatureBreaks.Interfaces;
using NatureBreaks.Models;
using System.Collections.Generic;

namespace NatureBreaks.Repositories
{
    public class FavoriteVideosRepository : BaseRepository, IFavoriteVideosRepository
    {

        private readonly string _connectionString;
        public FavoriteVideosRepository(IConfiguration configuration) : base(configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private new SqlConnection Connection
        {
            get { return new SqlConnection(_connectionString); }
        }

        public List<FavoriteVideos> GetAllFavorites(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                { //create the sql query for this to join the video object with the videoId
                    cmd.CommandText = "SELECT fv.Id, fv.UserId, fv.VideoId, v.Id, v.NatureTypeId, v.UserId, v.Season, v.VideoName, v.VideoUrl, v.ClosestMajorCity FROM Video v JOIN FavoriteVideos fv on fv.VideoId = v.Id; WHERE fv.UserId = @id";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var favoriteVideos = new List<FavoriteVideos>();
                        while (reader.Read())
                        {
                            var favoriteVideo = new FavoriteVideos()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                VideoId = reader.GetInt32(reader.GetOrdinal("VideoId")),
                                Video = new Video()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    NatureTypeId = reader.GetInt32(reader.GetOrdinal("NatureTypeId")),
                                    UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                    Season = reader.GetString(reader.GetOrdinal("Season")),
                                    VideoName = reader.GetString(reader.GetOrdinal("VideoName")),
                                    VideoUrl = reader.GetString(reader.GetOrdinal("VideoUrl")),
                                    ClosestMajorCity = reader.GetString(reader.GetOrdinal("ClosestMajorCity")),
                                },
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            };
                            favoriteVideos.Add(favoriteVideo);
                        }

                        return favoriteVideos;
                    }
                }
            }
        }

        public List<FavoriteVideos> GetFavoriteById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                { //these need to be join tables
                    cmd.CommandText = @"SELECT fv.Id, fv.UserId, fv.VideoId,
                        v.Id, v.NatureTypeId, v.UserId, v.Season, v.VideoName, v.VideoUrl, v.ClosestMajorCity
 
                        FROM Video v
                        JOIN FavoriteVideos fv on fv.VideoId = v.Id;
                        WHERE fv.UserId = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<FavoriteVideos> favoriteVideos = new List<FavoriteVideos>();
                        while (reader.Read())
                        {
                            var favoriteVideo = new FavoriteVideos
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                VideoId = reader.GetInt32(reader.GetOrdinal("VideoId")),
                                Video = new Video()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    NatureTypeId = reader.GetInt32(reader.GetOrdinal("NatureTypeId")),
                                    UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                    Season = reader.GetString(reader.GetOrdinal("Season")),
                                    VideoName = reader.GetString(reader.GetOrdinal("VideoName")),
                                    VideoUrl = reader.GetString(reader.GetOrdinal("VideoUrl")),
                                    ClosestMajorCity = reader.GetString(reader.GetOrdinal("ClosestMajorCity")),
                                },
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            };
                            favoriteVideos.Add(favoriteVideo);
                        }
                        return favoriteVideos;
                    }
                }
            }
        }

        public void AddFavorite(FavoriteVideos favoriteVideo)
        { //question for taylopr: do i need a join Query here for teh structure of my app? i figure if I just add teh userID to their favrites, when the GETBYID or GetAll for favorite videos runs THAT query would run it's own join tyable query, and just grab teh info then. seems like that would both speed up runtime, and make the back end hold less data. am I thinking ofthat correctly?  
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO FavoriteVideos (UserId, VideoId)
                        OUTPUT INSERTED.ID
                        VALUES (@userid, @videoid)";
                    cmd.Parameters.AddWithValue("@videoid", favoriteVideo.VideoId);
                    cmd.Parameters.AddWithValue("@userid", favoriteVideo.UserId);
                    favoriteVideo.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void DeleteFavoriteById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM FavoiteVideos WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
