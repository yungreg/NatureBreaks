using NatureBreaks.Models;
using System.Collections.Generic;

namespace NatureBreaks.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(User user);
        void DeleteUserById(int id);
        List<User> GetAllUsers();

        User GetUserById(int id);
    }
}