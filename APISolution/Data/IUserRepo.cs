using System;
using APISolution.Models;

namespace APISolution.Data
{
    public interface IUserRepo
    {
        void RegsiterUser(User user);
        bool SaveChanges();
    }
}

