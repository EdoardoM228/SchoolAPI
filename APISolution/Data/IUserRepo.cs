using System;
using System.Threading.Tasks;
using APISolution.Models;

namespace APISolution.Data
{
    public interface IUserRepo
    {
        void RegsiterUser(User user);
        Task<bool> SaveChangesAsync();
    }
}

