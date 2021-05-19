using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using StudentGrades.Repository;
using StudentGrades.ViewModels;

namespace StudentGrades.Controllers
{
    public class StudentGradesController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: StudentGrades
        public ActionResult Index()
        {
            return View(db.StudentGrades.ToList());
        }

        // GET: StudentGrades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGrades.Models.StudentGrades studentGrades = db.StudentGrades.Find(id);
            if (studentGrades == null)
            {
                return HttpNotFound();
            }
            return View(studentGrades);
        }

        // GET: StudentGrades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentGrades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Grade,ReportingDate")] StudentGrades.Models.StudentGrades studentGrades)
        {
            if (ModelState.IsValid)
            {
                db.StudentGrades.Add(studentGrades);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studentGrades);
        }

        // GET: StudentGrades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGrades.Models.StudentGrades studentGrades = db.StudentGrades.Find(id);
            if (studentGrades == null)
            {
                return HttpNotFound();
            }
            return View(studentGrades);
        }

        // POST: StudentGrades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Grade,ReportingDate")] StudentGrades.Models.StudentGrades studentGrades)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentGrades).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentGrades);
        }

        // GET: StudentGrades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentGrades.Models.StudentGrades studentGrades = db.StudentGrades.Find(id);
            if (studentGrades == null)
            {
                return HttpNotFound();
            }
            return View(studentGrades);
        }

        // POST: StudentGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentGrades.Models.StudentGrades studentGrades = db.StudentGrades.Find(id);
            db.StudentGrades.Remove(studentGrades);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: StudentGrades/Delete/5
        [HttpGet, ActionName("GetFinalGrades")]
        public ActionResult GetFinalGrades()
        {
            var finalGrades = db.StudentGrades.GroupBy(t => new { Name = t.Name })
               .Select(g => new { Average = g.Average(p => p.Grade), Name = g.Key.Name }).ToList();

            List<StudentGradeViewModel> viewModel = new List<StudentGradeViewModel>();
            foreach (var g in finalGrades) 
            {
                viewModel.Add(new StudentGradeViewModel { Name = g.Name, FinalGrade = g.Average  });
            }

            return View("FinalGrades", viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
