using System;
using System.Collections.Generic;
using APISolution.Models;

namespace APISolution.Data
{
    public interface IClassmateRepo
    {
        bool SaveChanges();
        IEnumerable<Classmate> GetAllClassmates();
        Classmate GetClassmateById(int id);
        void CreateClassmate(Classmate classmate);
        void UpdateClassmate(Classmate classmate);
        void DeleteClassmate(Classmate classmate);
    }
}

