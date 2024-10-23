using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAttendance.Data;
using StudentAttendance.Models;

namespace StudentAttendance.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
       
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //Get Student
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }
        
        // GET: Students/Create

        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student stu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stu);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");   
            }
            return View(stu);
        }
        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,StudentName,EnrollmentNumber")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Ensure the entity is attached before updating
                    _context.Attach(student).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
        // POST: Student/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
