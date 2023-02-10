using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NatureBreaks.Interfaces;
using NatureBreaks.Models;
using System.Collections.Generic;

namespace NatureBreaks.Repositories
{
    public class FavoriteVideoRepository : BaseRepository, IFavoriteVideoRepository
    {

        private readonly string _connectionString;
        public FavoriteVideoRepository(IConfiguration configuration) : base(configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private new SqlConnection Connection
        {
            get { return new SqlConnection(_connectionString); }
        }

        public List<FavoriteVideo> GetAllFavorites()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, UserId, VideoId FROM FavoriteVideo;";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var favoriteVideos = new List<FavoriteVideo>();
                        while (reader.Read())
                        {
                            var favoriteVideo = new FavoriteVideo()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                VideoId = reader.GetInt32(reader.GetOrdinal("VideoId")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            };
                            favoriteVideos.Add(favoriteVideo);
                        }

                        return favoriteVideos;
                    }
                }
            }
        }

        public FavoriteVideo GetFavoriteById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                { //these need to be join tables
                    cmd.CommandText = @"SELECT Id, UserId, VideoId FROM FavoriteVideo;
                         WHERE Id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        FavoriteVideo favoriteVideo = null;
                        if (reader.Read())
                        {
                            favoriteVideo = new FavoriteVideo()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                VideoId = reader.GetInt32(reader.GetOrdinal("VideoId")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            };
                        }
                        return favoriteVideo;
                    }
                }
            }
        }

        public void AddFavorite(FavoriteVideo favoriteVideo)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO FavoriteVideo (Id, UserId, VideoId)
                        OUTPUT INSERTED.ID
                        VALUES (@id, @userid, @videoid)";
                    cmd.Parameters.AddWithValue("@id", favoriteVideo.Id);
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
                    cmd.CommandText = "DELETE FROM FavoiteVideo WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
