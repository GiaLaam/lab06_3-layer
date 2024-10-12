using lab06_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab06_BUS
{
    public class MaforService
    {
        public List<Major> GetAllByfaculty(int facultyId)
        {
            StudentContextDB context = new StudentContextDB();
            return context.Majors.Where(p => p.FacultyID == facultyId).ToList();
        }
    }
}
