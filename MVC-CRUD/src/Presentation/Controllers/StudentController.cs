using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using Presentation.Models.Entities;
using Presentation.Models.ViewModels;

namespace Presentation.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDBContext _dbContext;

        public StudentController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel) 
        {
            var student = new Student()
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                Address = viewModel.Address,
                Phone = viewModel.Phone,
            };
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await _dbContext.Students.ToListAsync();

            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await _dbContext.Students.FindAsync(viewModel.Id);

            if(student is not null)
            {
                student.FirstName = viewModel.FirstName;
                student.LastName = viewModel.LastName;
                student.Email = viewModel.Email;
                student.Address = viewModel.Address;
                student.Phone = viewModel.Phone;
            }
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("List", "Student");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteStudent(Student viewModel)
        {
            var student = await _dbContext.Students.FindAsync(viewModel.Id);
            if(student is not null)
            {
                _dbContext.Students.Remove(student);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Student");
        }
    }
}