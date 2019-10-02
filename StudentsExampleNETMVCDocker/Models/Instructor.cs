using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsExampleNETMVCDocker.Models
{
    public class Instructor : Person
    {  
      
        public virtual ICollection<Course> Courses { get; set; }
    }
}