using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShivamSingh_DotNetDeveloper.Models;

namespace ShivamSingh_DotNetDeveloper.Controllers
{
    public class StudentController : Controller
    {
        StudentContext studentContextDB = new StudentContext();

        public ActionResult AllStudent(string SortOrder, string SelectedSTD, string SelectedSubject)
        {
            ViewBag.STD = string.IsNullOrEmpty(SortOrder) ? "STD_desc" : "";
            ViewBag.Subject = SortOrder == "Subject" ? "Subject_desc" : "Subject";
            var data = studentContextDB.Students.ToList();
            var emp = from s in data select s;
            if (!String.IsNullOrEmpty(SelectedSTD))
            {
                emp = emp.Where(x => x.STD.Trim().Equals(SelectedSTD.Trim()));
            }
            if (!String.IsNullOrEmpty(SelectedSubject))
            {
                emp = emp.Where(x => x.Subject.Trim().Equals(SelectedSubject.Trim()));
            }
            var UniqueSTD = from s in emp
                            group s by s.STD into newGroup
                            where newGroup.Key != null
                            select new { std = newGroup.Key };
            ViewBag.UniqueSTD = UniqueSTD.Select(x => new SelectListItem { Value = x.std, Text = x.std }).ToList();
            var UniqueSubject = from s in emp
                                group s by s.Subject into newGroup
                                where newGroup.Key != null
                                select new { subject = newGroup.Key };
            ViewBag.UniqueSubject = UniqueSubject.Select(x => new SelectListItem { Value = x.subject, Text = x.subject }).ToList();
            ViewBag.SelectedSTD = SelectedSTD;
            ViewBag.SelectedSubject = SelectedSubject;
            switch (SortOrder)
            {
                case "STD_desc":
                    emp = emp.OrderByDescending(x => x.STD);
                    break;
                case "Subject":
                    emp = emp.OrderBy(x => x.Subject);
                    break;
                case "Subject_desc":
                    emp = emp.OrderByDescending(x => x.Subject);
                    break;
                default:
                    emp = emp.OrderBy(x => x.STD);
                    break;
            }
            return View(emp);
        }
        public ActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddStudent(Student s)
        {
            if (ModelState.IsValid == true)
            {
                studentContextDB.Students.Add(s);
                studentContextDB.SaveChanges();
                return RedirectToAction("AllStudent");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            var getSpecificStudent = studentContextDB.Students.Where(x => x.ID == id).FirstOrDefault();
            return View(getSpecificStudent);
        }
        [HttpPost]
        public ActionResult Edit(Student s)
        {
            if (ModelState.IsValid == true)
            {
                studentContextDB.Entry(s).State = EntityState.Modified;
                studentContextDB.SaveChanges();
                return RedirectToAction("AllStudent");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            var getSpecificStudent = studentContextDB.Students.Where(x => x.ID == id).FirstOrDefault();
            studentContextDB.Students.Remove(getSpecificStudent);
            studentContextDB.SaveChanges();
            return RedirectToAction("AllStudent");
        }
    }
}