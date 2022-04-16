using EmployeeCRUD.Data;
using EmployeeCRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Employee> objlist = _context.Employees;
            return View(objlist);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee empobj)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(empobj);
                _context.SaveChanges();

                TempData["ResultatOk"] = "Record added successfully!";

                return RedirectToAction("Index");
            }
            return View(empobj);
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var empfromdb = _context.Employees.Find(id);

            if(empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee empobj)
        {
            if(ModelState.IsValid)
            {
                _context.Employees.Update(empobj);
                _context.SaveChanges();

                TempData["ResultOk"] = "Data update successfully!";

                return RedirectToAction("Index");
            }
            return View(empobj) ;      
        }

        public IActionResult Delete(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }

            var empfromdb=_context.Employees.Find(id);

            if( empfromdb == null)
            { 
                return NotFound();
            }
            return View(empfromdb) ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmp(int? id)
        {
            var deleterecord = _context.Employees.Find(id);

            if(deleterecord == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(deleterecord);
            _context.SaveChanges();
            TempData["ResultOk"] = "Data deleted successfull !";
            return RedirectToAction("Index");
        }
    }
}
