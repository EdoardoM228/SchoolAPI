using System;
using System.Collections.Generic;
using System.Linq;
using APISolution.Models;

namespace APISolution.Data
{
    public class ServerClassmateRepo : IClassmateRepo
    {
        private readonly ClassmateContext _context;

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

        public IEnumerable<Classmate> GetAllClassmates()
        {
            return _context.Classmates.ToList();
        }

        public Classmate GetClassmateById(int id)
        {
            return _context.Classmates.FirstOrDefault(x => x.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateClassmate(Classmate classmate)
        {

        }
    }
}

