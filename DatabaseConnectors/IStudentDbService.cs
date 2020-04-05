using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBDZjazd2.Models;

namespace APBDZjazd2.DatabaseConnectors
{
    public interface IStudentDbService
    {
        IEnumerable<Student> GetStudents();
        IEnumerable<string> GetEnrollments(int id);
    }
}
