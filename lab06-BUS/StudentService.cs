using lab06_DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data.Entity;



namespace lab06_BUS
{

    public class StudentService
    {
        public List<Student> GetAll()
        {
            StudentContextDB context = new StudentContextDB();
            return context.Students.ToList();
        }

        public List<Student> GetAllHasNoMajor()
        {
            StudentContextDB context = new StudentContextDB();
            return context.Students.Where(p => p.MajorID == null ).ToList();
        }

        public List <Student> GetAllHasNoMajor(int facultyID)
        {
            StudentContextDB context = new StudentContextDB();
            return context.Students.Where(p => p.MajorID == null && p.FacultyID == facultyID).ToList();
        }

        public Student FinbyId(int studentId)
        {
            StudentContextDB context = new StudentContextDB();
            return context.Students.FirstOrDefault(p => p.StudentID == studentId);
        }

        public void InsertUpdate(Student s)
        {
            StudentContextDB context = new StudentContextDB();
            context.Students.Add(s);
            context.SaveChanges();
        }

        public void AddStudent(Student student)
        {
            StudentContextDB context = new StudentContextDB();
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            context.Students.Add(student);
            context.SaveChanges();
        }

        public void RemoveStudent(int studentId)
        {
            using (StudentContextDB context = new StudentContextDB())
            {
                // Tìm sinh viên trong cơ sở dữ liệu
                var student = context.Students.Find(studentId);
                if (student != null)
                {
                    context.Students.Remove(student);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Sinh viên không tồn tại.");
                }
            }
        }

    }

}

