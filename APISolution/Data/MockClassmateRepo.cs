using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APISolution.Models;

namespace APISolution.Data
{
    public class MockClassmateRepo : IClassmateRepo
    {
        List<Classmate> commands = new List<Classmate>
            {
               new Classmate { Id = 0, Name = "Danik", Age = 17 },
               new Classmate { Id = 1, Name = "Yan", Age = 17 },
               new Classmate { Id = 2, Name = "Rustam", Age = 17 },
               new Classmate { Id = 3, Name = "Edik", Age = 18 },
               new Classmate { Id = 4, Name = "Nikita", Age = 18 }
            };

        public void CreateClassmate(Classmate classmate)
        {
            throw new NotImplementedException();
        }

        public void DeleteClassmate(Classmate classmate)
        {
            throw new NotImplementedException();
        }

        public void DeleteClassmate(Task<Classmate> classmateModelFromRepo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Classmate> GetAllClassmates()
        {
            return commands.ToList();
        }

        public Task<IEnumerable<Classmate>> GetAllClassmatesAsync()
        {
            throw new NotImplementedException();
        }

        public Classmate GetClassmateById(int id)
        {
            return commands.FirstOrDefault(x => x.Id == id);
        }

        public Task<Classmate> GetClassmateByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateClassmate(Classmate classmate)
        {
            throw new NotImplementedException();
        }

        public void UpdateClassmate(Task<Classmate> classmateModelFromRepo)
        {
            throw new NotImplementedException();
        }
    }
}

