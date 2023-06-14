using StudentManagement.Models;

namespace StudentManagement.Services
{
    public interface IStudentServices
    {
        public List<Student> GetStudentList();
        public string InsertStudent(Student student);
        public string UpdateStudent(Student student);
        public string DeleteStudent(int StudentId);
    }
}
