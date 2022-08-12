using System;
using System.Threading.Tasks;
using APISolution.Models;

namespace APISolution.Data
{
    public class ServerUserRepo : IUserRepo
    {
        private readonly ClassmateContext _context;

        public ServerUserRepo(ClassmateContext context)
        {
            _context = context;
        }

        public void RegsiterUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _context.Users.Add(user);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}

