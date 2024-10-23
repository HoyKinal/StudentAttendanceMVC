using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentAttendance.Data;
using StudentAttendance.Models;

namespace StudentAttendance.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendanceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var attendances = await _context.Attendances.Include(a => a.Student).ToListAsync();
            return View(attendances);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Fetch the list of students and prepare for the dropdown
            ViewBag.StudentId = new SelectList(await _context.Students.ToListAsync(), "StudentId", "StudentName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Attendance att)
        {
            if (ModelState.IsValid)
            {
                _context.Add(att);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // Prepare the student list again in case of errors
            ViewBag.StudentId = new SelectList(await _context.Students.ToListAsync(), "StudentId", "StudentName", att.StudentId);
            return View(att);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            ViewBag.StudentId = new SelectList(await _context.Students.ToListAsync(), "StudentId", "StudentName", attendance.StudentId);
            return View(attendance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Attendance attendance)
        {
            if (id != attendance.AttendanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.AttendanceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.StudentId = new SelectList(await _context.Students.ToListAsync(), "StudentId", "StudentName", attendance.StudentId);
            return View(attendance);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.Include(a => a.Student).FirstOrDefaultAsync(a => a.AttendanceId == id);
            if (attendance == null)
            {
                return NotFound();
            }
            return View(attendance);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.AttendanceId == id);
        }
    }
}
