using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Repository.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        Task<bool> CheckIfUserExist(string username);
        Task<bool> CheckUserPassword(User user);
        Task<int> AddUser(User user);
        Task Save();
    }
}
