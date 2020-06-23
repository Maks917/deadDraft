using Microsoft.AspNetCore.Mvc;
using deadDraft.Models;
using deadDraft.ViewModels;

namespace deadDraft.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        public HomeController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public IActionResult Index()
        {
            var model = _taskRepository.GetAllTasks();
            return View(model);
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            ToDo toDo = _taskRepository.GetTask(id.Value);
            return View(toDo);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ToDo toDo)
        {
            if (toDo == null) return NotFound();

            if (ModelState.IsValid)
            {
                ToDo newTask =  _taskRepository.Add(toDo);
                return RedirectToAction("details", new { id = newTask.Id });
            }
            return View();
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            ToDo toDo =  _taskRepository.GetTask(id.Value);

            if (toDo == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }

            TaskEditViewModels taskEditViewModel = new TaskEditViewModels
            {
                Id = toDo.Id,
                Title = toDo.Title,
                Duty = toDo.Duty,
            };

            return View(taskEditViewModel);
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var model = _taskRepository.Delete(id.Value);

            if (model == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }

            return RedirectToAction("index");
        }


        [HttpPost]
        public IActionResult Edit(ToDo model)
        {
            if (ModelState.IsValid)
            {
                ToDo toDo = _taskRepository.GetTask(model.Id);

                toDo.Title = model.Title;
                toDo.Duty = model.Duty;

               ToDo updatedToDO = _taskRepository.Update(toDo);
                return RedirectToAction("index");
            }

            return View(model);
        }
    }
}
