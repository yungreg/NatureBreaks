using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using NatureBreaks.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using NatureBreaks.Interfaces;

namespace NatureBreaks.Repositories
{
    //[Authorize]
    public class NatureTypeRepository : BaseRepository, INatureTypeRepository
    {
        private readonly string _connectionString;
        public NatureTypeRepository(IConfiguration configuration) : base(configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private new SqlConnection Connection
        {
            get { return new SqlConnection(_connectionString); }
        }
        // refactor here to get all types to displkay in a checkbox:
        public List<NatureType> GetAllNatureTypes()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, NatureTypeName FROM NatureType;";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var allTypes = new List<NatureType>();
                        while (reader.Read())
                        {
                            var type = new NatureType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                NatureTypeName = reader.GetString(reader.GetOrdinal("NatureTypeName")),

                            };
                            allTypes.Add(type);
                        }

                        return allTypes;
                    }
                }
            }
        }

        public NatureType GetNatureTypeById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, NatureTypeName FROM NatureType
                         WHERE Id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        NatureType natureType = null;
                        if (reader.Read())
                        {
                            natureType = new NatureType()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                NatureTypeName = reader.GetString(reader.GetOrdinal("NatureTypeName")),
                            };
                        }
                        return natureType;
                    }
                }
            }
        }

        public void AddNatureType(NatureType natureType)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO NatureType (Id, NatureTypeName)
                        OUTPUT INSERTED.ID
                        VALUES (@id, @naturetypename)";
                    cmd.Parameters.AddWithValue("@id", natureType.Id);
                    cmd.Parameters.AddWithValue("@naturetypename", natureType.NatureTypeName);
                    natureType.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateNatureType(NatureType natureType)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE NatureType
                           SET NatureTypeName = @naturetypename, 
                         WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", natureType.NatureTypeName);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteNatureTypeById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM NatureType WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
