using lab06_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab06_BUS
{
    public class FacultyService
    {
        public List<Faculty> GetAll()
        {
            StudentContextDB context = new StudentContextDB();
            return context.Faculties.ToList();
        }
    }
}
