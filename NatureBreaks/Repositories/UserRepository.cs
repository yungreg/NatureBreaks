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
    public class UserRepository : BaseRepository, IUserRepository 
        {
        private readonly string _connectionString;
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private new SqlConnection Connection
        {
            get { return new SqlConnection(_connectionString); }
        }
        // refactor here to include your USers!
        public List<User> GetAllUsers()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, FirebaseUserId, FirstName, Email, ProfileImage, IsAdmin FROM [User];";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var allUsers = new List<User>();
                        while (reader.Read())
                        {
                            var user = new User()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirebaseUserId = reader.GetString(reader.GetOrdinal("FirebaseUserId")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                ProfileImage = reader.GetString(reader.GetOrdinal("ProfileImage")),
                                IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin")),
                            };
                            allUsers.Add(user);
                        }

                        return allUsers;
                    }
                }
            }
        }

        public User GetUserById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, FirebaseUserId, FirstName, Email, ProfileImage, IsAdmin FROM [User];
                         WHERE Id = @id;";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        User user = null;
                        if (reader.Read())
                        {
                            user = new User()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirebaseUserId = reader.GetString(reader.GetOrdinal("FirebaseUserId")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                ProfileImage = reader.GetString(reader.GetOrdinal("ProfileImage")),
                                IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin")),
                            };
                        }
                        return user;
                    }
                }
            }
        }


        public void AddUser(User user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO [User] (Id, FirebaseUserId, FirstName, Email, ProfileImage, IsAdmin)
                        OUTPUT INSERTED.ID
                        VALUES (@firebaseuserid, @firstname, @email, @profileimage, @isadmin)";
                    cmd.Parameters.AddWithValue("@firebaseuserid", user.FirebaseUserId);
                    cmd.Parameters.AddWithValue("@firstname", user.FirstName);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@profileimage", user.ProfileImage);
                    cmd.Parameters.AddWithValue("@isadmin", user.IsAdmin);
                    user.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
        public void DeleteUserById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM [User] WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
