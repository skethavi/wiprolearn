using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    internal class StudentR
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public double Cgpa { get; set; }

        public StudentR()
        {
            StudentId = 0;
            StudentName = string.Empty;
            Cgpa = 0.0;
        }

        public StudentR(int studentId, string studentName, double cgpa)
        {
            StudentId = studentId;
            StudentName = studentName;
            Cgpa = cgpa;
        }

        public override string ToString()
        {
            return "Sid " + StudentId + " Name " + StudentName + " Cgp " + Cgpa;
        }

        public void ShowStudent()
        {
            Console.WriteLine("Under Construction...");
        }

        public void DeleteStudent(int sid)
        {
            Console.WriteLine("Delete Student " + sid);
        }
        public void SearchStudent(int sid)
        {
            Console.WriteLine("Search Student  " + sid);
        }

        public void AddStudent(StudentR student)
        {
            Console.WriteLine("Please Add Student...");
        }

    }
}
