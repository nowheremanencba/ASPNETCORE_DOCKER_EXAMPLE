using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentsExampleNETMVCDocker.Models
{
    public class Enrollment
    { 
        public int EnrollmentID { get; set; }
        
        public int StudentID { get; set; }
        public int CoursesID { get; set; }
        [Required, Range(0, 10)] 
        [RegularExpression("^[0-9]*$", ErrorMessage = "Grade must be numeric")]
        public int Grade { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Courses { get; set; }
    }
} 