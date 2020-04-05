using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using APBDZjazd2.Models;

namespace APBDZjazd2.DatabaseConnectors
{
    public class StudentDbService: IStudentDbService
    {

        private readonly string SqlConn = "Data Source=DESKTOP-M4CB7LR; Initial Catalog=pjatk; Integrated Security=True";

        public IEnumerable<Student> GetStudents()
        {
            var output = new List<Student>();
            using (var client = new SqlConnection(SqlConn))
            {
                using var command = new SqlCommand
                {
                    Connection = client,
                    CommandText = "SELECT Student.FirstName, Student.LastName, Student.BirthDate, Studies.Name as 'Studies', Enrollment.Semester FROM Student INNER JOIN Enrollment ON Student.IdEnrollment = Enrollment.IdEnrollment INNER JOIN Studies ON Studies.IdStudy = Enrollment.IdStudy"
                };

                client.Open();
                var dr = command.ExecuteReader();

                while (dr.Read())
                {
                    output.Add(new Student
                    {
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        BirthDate = dr["BirthDate"].ToString(),
                        Studies = dr["Studies"].ToString(),
                        Semester = int.Parse(dr["Semester"].ToString())
                    });
                }
            }
            return output;
        }

        public IEnumerable<string> GetEnrollments(int id)
        {
            var output = new List<string>();
            using (var client = new SqlConnection(SqlConn))
            {
                using var command = new SqlCommand();
                command.Connection = client;
                command.CommandText = "SELECT Enrollment.Semester, Studies.Name as 'Studies' FROM Student INNER JOIN Enrollment ON Student.IdEnrollment = Enrollment.IdEnrollment INNER JOIN Studies ON Studies.IdStudy = Enrollment.IdStudy WHERE Student.IndexNumber = @id";
                command.Parameters.AddWithValue("id", id);

                client.Open();
                var dr = command.ExecuteReader();

                while (dr.Read())
                {
                    output.Add($"{dr["Studies"]} on {dr["Semester"]} semester");
                }
            }
            return output;
        }

    }
}
