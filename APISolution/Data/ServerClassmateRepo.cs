using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APISolution.Models;
using Microsoft.EntityFrameworkCore;

namespace APISolution.Data
{
    public class ServerClassmateRepo : IClassmateRepo, IDisposable
    {
        private ClassmateContext _context;

        public ServerClassmateRepo(ClassmateContext context)
        {
            _context = context;
        }

        public void CreateClassmate(Classmate classmate)
        {
            if (classmate == null)
            {
                throw new ArgumentNullException(nameof(classmate));
            }

            _context.Classmates.Add(classmate);
        }

        public void DeleteClassmate(Classmate classmate)
        {
            if (classmate == null)
                throw new ArgumentNullException(nameof(classmate));

            _context.Classmates.Remove(classmate);
        }

        public async Task<IEnumerable<Classmate>> GetAllClassmatesAsync()
        {
            return await _context.Classmates.ToListAsync();
        }

        public async Task<Classmate> GetClassmateByIdAsync(int id)
        {
            return await _context.Classmates.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void UpdateClassmate(Classmate classmate)
        {

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}


