using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using NatureBreaks.Models;
using NatureBreaks.Utils;
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
                    cmd.CommandText = "SELECT Id, FirebaseUserId, FirstName, Email, ProfileImage, UserTypeId FROM [User];";
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
                                UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
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
                                UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
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
                        INSERT INTO [User] (FirebaseUserId, FirstName, Email, ProfileImage, UserTypeId)
                        OUTPUT INSERTED.ID
                        VALUES (@firebaseuserid, @firstname, @email, @profileimage, @usertypeid)";
                    cmd.Parameters.AddWithValue("@firebaseuserid", user.FirebaseUserId);
                    cmd.Parameters.AddWithValue("@firstname", user.FirstName);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@profileimage", user.ProfileImage);
                    cmd.Parameters.AddWithValue("@usertypeid", user.UserTypeId);
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

     

        public User GetByFirebaseUserId(string firebaseUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT u.Id, u.FirebaseUserId, u.FirstName, u.Email, u.UserTypeId,
                               ut.Name AS UserTypeName
                          FROM [User] u
                               LEFT JOIN UserType ut on u.UserTypeId = ut.Id
                         WHERE FirebaseUserId = @FirebaseuserId";

                    DbUtils.AddParameter(cmd, "@FirebaseUserId", firebaseUserId);

                    User user = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user = new User()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                            FirstName = DbUtils.GetString(reader, "FirstName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            UserTypeId = DbUtils.GetInt(reader, "UserTypeId"),
                            UserType = new UserType()
                            {
                                Id = DbUtils.GetInt(reader, "UserTypeId"),
                                Name = DbUtils.GetString(reader, "UserTypeName"),
                            }
                        };
                    }
                    reader.Close();

                    return user;
                }
            }
        }

        public User GetCurrentUser(string firebaseuserid)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, FirebaseUserId, FirstName, Email, ProfileImage, UserTypeId FROM [User];
                         WHERE  FirebaseUserId = @firebaseuserid;";
                    cmd.Parameters.AddWithValue("@firebaseuserid", firebaseuserid);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        User firebaseUser = null;
                        if (reader.Read())
                        {
                            firebaseUser = new User()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                FirebaseUserId = reader.GetString(reader.GetOrdinal("FirebaseUserId")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                ProfileImage = reader.GetString(reader.GetOrdinal("ProfileImage")),
                                UserTypeId = reader.GetInt32(reader.GetOrdinal("UserTypeId")),
                            };
                        }
                        return firebaseUser;
                    }
                }
            }
        }
    }
}

    
