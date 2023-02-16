
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NatureBreaks.Models;
using NatureBreaks.Interfaces;

namespace NatureBreaks.Repositories
{

    public class VideoRepository : BaseRepository, IVideoRepository
    {
        private readonly string _connectionString;
        public VideoRepository(IConfiguration configuration) : base(configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private new SqlConnection Connection
        {
            get { return new SqlConnection(_connectionString); }
        }
        //this needs to do the query for VIDEOS
        public List<Video> GetAllVideos()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, NatureTypeId, UserId, Season, VideoName, VideoUrl, ClosestMajorCity FROM Video;";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var vids = new List<Video>();
                        while (reader.Read())
                        {
                            var vid = new Video()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                NatureTypeId = reader.GetInt32(reader.GetOrdinal("NatureTypeId")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                Season = reader.GetString(reader.GetOrdinal("Season")),
                                VideoName = reader.GetString(reader.GetOrdinal("VideoName")),
                                VideoUrl = reader.GetString(reader.GetOrdinal("VideoUrl")),
                                ClosestMajorCity = reader.GetString(reader.GetOrdinal("ClosestMajorCity")),
                            };
                            vids.Add(vid);
                        }

                        return vids;
                    }
                }
            }
        }

        public Video Get(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, NatureTypeId, UserId, Season, VideoName, VideoUrl, ClosestMajorCity FROM Video
                         WHERE Id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Video vid = null;
                        if (reader.Read())
                        {
                            vid = new Video()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                NatureTypeId = reader.GetInt32(reader.GetOrdinal("NatureTypeId")),
                                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                                Season = reader.GetString(reader.GetOrdinal("Season")),
                                VideoName = reader.GetString(reader.GetOrdinal("VideoName")),
                                VideoUrl = reader.GetString(reader.GetOrdinal("VideoUrl")),
                                ClosestMajorCity = reader.GetString(reader.GetOrdinal("ClosestMajorCity")),
                            };
                        }

                        return vid;
                    }
                }
            }
        }

        public void Add(Video vid)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Video (NatureTypeId, UserId, Season, VideoName, VideoUrl, ClosestMajorCity)
                        OUTPUT INSERTED.ID
                        VALUES (@naturetypeid, @userid, @season, @videoname, @videourl, @closestmajorcity)";
                    cmd.Parameters.AddWithValue("@naturetypeid", vid.NatureTypeId);
                    cmd.Parameters.AddWithValue("@userid", vid.UserId);
                    cmd.Parameters.AddWithValue("@season", vid.Season);
                    cmd.Parameters.AddWithValue("@videoname", vid.VideoName);
                    cmd.Parameters.AddWithValue("@videourl", vid.VideoUrl);
                    cmd.Parameters.AddWithValue("@closestmajorcity", vid.ClosestMajorCity);
                    vid.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Video vid)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Video
                           SET NatureTypeId = @naturetypeid, 
                               UserId = @userid, 
                               Season = @season,
                               VideoName = @videoname,
                               VideoUrl = @videourl,
                               ClosestMajorCity = @closestmajorcity
                         WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", vid.Id);
                    cmd.Parameters.AddWithValue("@naturetypeid", vid.NatureTypeId);
                    cmd.Parameters.AddWithValue("@userid", vid.UserId);
                    cmd.Parameters.AddWithValue("@season", vid.Season);
                    cmd.Parameters.AddWithValue("@videoname", vid.VideoName);
                    cmd.Parameters.AddWithValue("@videourl", vid.VideoUrl);
                    cmd.Parameters.AddWithValue("@closestmajorcity", vid.ClosestMajorCity);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Video WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
