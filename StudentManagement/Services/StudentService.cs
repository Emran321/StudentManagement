using Dapper;
using Microsoft.Data.SqlClient;
using StudentManagement.Models;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace StudentManagement.Services
{

    public class StudentService : IStudentServices
    {
        private readonly IConfiguration configuration;
        private string ConnectionString => configuration.GetConnectionString("DBConnection");
        private string ProviderName => "System.Data.SqlClient";


        public StudentService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        private IDbConnection Connection => new SqlConnection(ConnectionString);

        public string DeleteStudent(int StudentId)
        {
            string result = "";
            return result;
            //try
            //{
            //    using (IDbConnection dbConnection = Connection)
            //    {
            //        dbConnection.Open();
            //        var std = dbConnection.Query<Student>("DeleteStudent",
            //            new
            //            {
            //                StudentId = StudentId
            //            },
            //             commandType: CommandType.StoredProcedure);
            //        if (std != null && std.FirstOrDefault().Response == "Delete Successfully")
            //        {
            //            result = "Delete Successfully";
            //        }
            //        dbConnection.Close();
            //        return result;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    string errorMsg = ex.Message;
            //    return errorMsg;
            //}
        }

        public List<Student> GetStudentList()
        {
            List<Student> students = new List<Student>();
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    students = dbConnection.Query<Student>("GetStudentList", commandType: CommandType.StoredProcedure).ToList();
                    dbConnection.Close();
                    return students;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return students;
            }
        }

        public string InsertStudent(Student student)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var std = dbConnection.Query<Student>("InsertStudent",
                        new { 
                         StudentName = student.StudentName,
                         EmailAddress = student.EmailAddress,
                         city = student.city,
                         CreateBy = student.CreateBy },
                         commandType: CommandType.StoredProcedure);
                    if (std != null && std.FirstOrDefault().Response == "Save Successfully")
                    {
                        result = "Save Successfully";
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return errorMsg;
            }
        }

        public string UpdateStudent(Student student)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var std = dbConnection.Query<Student>("SupdateStudent",
                        new
                        {
                            StudentName = student.StudentName,
                            EmailAddress = student.EmailAddress,
                            city = student.city,
                            CreateBy = student.CreateBy,
                            StudentId = student.StudentId,
                        },
                    commandType: CommandType.StoredProcedure);
                    var firstStudent = std.FirstOrDefault();
                    if (firstStudent != null && firstStudent.Response != null && firstStudent.Response == "Save Successfully")
                    {
                        result = "Save Successfully";
                    }

                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return errorMsg;
            }
        }
    }
}
