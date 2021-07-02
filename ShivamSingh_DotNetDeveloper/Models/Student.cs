using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShivamSingh_DotNetDeveloper.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string STD { get; set; }
        public string Subject { get; set; }
        public int Marks { get; set; }
    }
}