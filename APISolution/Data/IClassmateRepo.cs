using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APISolution.Models;

namespace APISolution.Data
{
    public interface IClassmateRepo
    {
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Classmate>> GetAllClassmatesAsync();
        Task<Classmate> GetClassmateByIdAsync(int id);
        void CreateClassmate(Classmate classmate);
        void UpdateClassmate(Classmate classmate);
        void DeleteClassmate(Classmate classmate);
    }
}

