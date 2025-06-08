using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using To_Do_List.Models;
using To_Do_List.Models.DTOs;
using To_Do_List.Repository;

namespace To_Do_List.Controllers
{
    public class TasksController : Controller
    {
        private IMapper _mapper;
        private IToDoTasksRepository _tasksRepository;
        public TasksController(IToDoTasksRepository tasksRepository,IMapper mapper)
        {
            _tasksRepository = tasksRepository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> List()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            IEnumerable<ToDoTask> taskList = await _tasksRepository.GetUserTasksAsync(userId);

            var taskDtoList = _mapper.Map<IEnumerable<ToDoTaskDTO>>(taskList);

            return View(taskDtoList);
        }

        [Authorize]
        public IActionResult AddTask()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTask(ToDoTaskDTO taskDTO)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("AddTask");
            }
            var newTask = _mapper.Map<ToDoTask>(taskDTO);
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            newTask.UserId = userId;

            await _tasksRepository.AddTaskAsync(newTask);
            return RedirectToAction("List");
        }

        [Authorize]
        public async Task<IActionResult> Done(int id)
        {
            await _tasksRepository.SetDoneAsync(id);
            return RedirectToAction("List");
        }
    }
}
