using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using StudentsExampleNETMVCDocker.Models;

namespace StudentsExampleNETMVCDocker.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly StudentsContext db;

        public EnrollmentController(StudentsContext _db)
        {
            db = _db;
        }

        // GET: Enrollment 
        public ActionResult Index()
        {
            var enrollments = from s in db.Enrollment
                              select s;
            return View(enrollments);
        }

       

        // GET Enrollment/GetEnrollment
        [HttpGet]
        public JsonResult GetEnrollment()
        {

            List<Enrollment> empList = db.Enrollment.ToList();
            foreach (var item in empList)
            {
                Student studentFound = db.Students.Where(x => x.ID == item.CoursesID).FirstOrDefault();
                if (studentFound != null)
                {
                    item.Student = studentFound;
                }
                Course courseFound = db.Courses.Where(x => x.CourseID == item.CoursesID).FirstOrDefault();
                if (courseFound != null)
                {
                    item.Courses = courseFound;
                }
                
                if (courseFound != null)
                {
                    item.Courses = courseFound;
                }
            }
            return Json(empList);
        }


        //POST Enrollment/AddEnrollment 
        [HttpPost]
        public JsonResult Insert(Enrollment enrollment)
        {
            Student studentFound = db.Students.Where(x => x.ID == enrollment.StudentID).FirstOrDefault();
            if (studentFound != null)
            {
                enrollment.Student = studentFound;
            }
            Course CourseFound = db.Courses.Where(x => x.CourseID == enrollment.CoursesID).FirstOrDefault();
            if (CourseFound != null)
            {
                enrollment.Courses = CourseFound;
            }
            try
            {
                if (enrollment != null)
                {
                    db.Enrollment.Add(enrollment);
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                return Json(new { success = false });
            }
         
        }


        //POST Enrollment/Update     
        [HttpPost]
        public JsonResult Update(Enrollment updatedEnrollment)
        {
            try
            {
                Enrollment existingEnrollment = db.Enrollment.Find(updatedEnrollment.EnrollmentID);
                if (existingEnrollment == null)
                {
                    return Json(new { success = false });
                }
                else
                {
                     existingEnrollment.Grade = updatedEnrollment.Grade;
                    existingEnrollment.CoursesID = updatedEnrollment.CoursesID;
                    existingEnrollment.StudentID = updatedEnrollment.StudentID;

                    db.SaveChanges();
                    return Json(new { success = true });

                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                return Json(new { success = false });
            }
        }
        //POST Employee/Delete/1
        [HttpPost]
        public JsonResult Delete(int id)
        {
            Enrollment EnrollmentFound = db.Enrollment.Find(id);
            if (EnrollmentFound == null)
            {
                return Json(new { success = false });
            }
            db.Enrollment.Remove(EnrollmentFound);
            db.SaveChanges();
            return Json(new { success = true });

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Enrollment enrollment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Enrollment.Add(enrollment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            { 
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(enrollment);
        }
    }
}