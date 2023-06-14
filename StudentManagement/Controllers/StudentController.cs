using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using StudentManagement.Services;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {

        private readonly IConfiguration configuration;
        private readonly IStudentServices studentServices;
        public StudentController(IConfiguration configuration, IStudentServices studentServices)
        {
            this.configuration = configuration;
            this.studentServices = studentServices;
        }

        public IActionResult Index()
        {
            StudentViewModel model = new StudentViewModel();
            model.StudentList = studentServices.GetStudentList().ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddUpdateStudent(Student student)
        {
            StudentViewModel model = new StudentViewModel();
            student.CreateBy = 1;
            string url = Request.Headers["Referer"].ToString();

            string result = string.Empty;
            if (student.StudentId > 0)
            {
                result = studentServices.UpdateStudent(student);
            }
            else
            {
                result = studentServices.InsertStudent(student);
            }

            if (result == "Save Successfully")
            {
                TempData["SuccessMsg"] = "Save Successfully";
                return Redirect(url);
            }
            else
            {
                TempData["ErrorMsg"] = result;
                return Redirect(url);
            }
        }

        [HttpPost]
        public IActionResult DeleteStudent(int StudentId)
        {
            string url = Request.Headers["Referer"].ToString();
            string result = studentServices.DeleteStudent(StudentId);

            if (result == "Delete Successfully")
            {
                return Json(new { message = "Delete Successfully" });
            }
            else
            {
                return Json(new { message = "Error Successfully" });
            }

        }
















        //[HttpPost]
        //public IActionResult DeleteStudent(int StudentId)
        //{
        //    string url = Request.Headers["Referer"].ToString();
        //    string result = studentServices.DeleteStudent(StudentId);

        //    if (result == "Delete Successfully")
        //    {
        //        return Json(new
        //        {
        //            message = "Delete Successfully"
        //        });
        //    }
        //    else
        //    {
        //        return Json(new
        //        {
        //            message = "Error Occured"
        //        });
        //    }
        //}
    }
}
