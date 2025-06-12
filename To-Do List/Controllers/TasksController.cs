using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using To_Do_List.Models;
using To_Do_List.Models.DTOs;
using To_Do_List.Repository;
using To_Do_List.Services;

namespace To_Do_List.Controllers
{
    public class TasksController : Controller
    {
        private IMapper _mapper;
        private IToDoTasksRepository _tasksRepository;
        private ITaskService _taskService;
        public TasksController(IToDoTasksRepository tasksRepository, ITaskService taskService, IMapper mapper)
        {
            _tasksRepository = tasksRepository;
            _taskService = taskService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> List(string orderBy = "")
        {
            var taskDtoList = await _taskService.GetTasksListByUser(User);

            TempData["orderBy"] = orderBy;

                switch (orderBy)
                {
                    case "DateDesc":
                        taskDtoList = taskDtoList.OrderByDescending(x => x.Date);
                        break;

                    case "PriorityDesc":
                        taskDtoList = taskDtoList.OrderByDescending(x => x.Priority);
                        break;

                    case "Date":
                        taskDtoList = taskDtoList.OrderBy(x => x.Date);
                        break;

                    case "Priority":
                        taskDtoList = taskDtoList.OrderBy(x => x.Priority);
                        break;

                    _:
                        break;
                }



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

            await _taskService.AddTaskByUser(User, taskDTO);

            return RedirectToAction("List");
        }

        [Authorize]
        public async Task<IActionResult> Done(int id)
        {
            if (TempData["orderBy"] is not null) TempData.Keep("orderBy");

            await _taskService.SetDoneByTaskId(id);
            return RedirectToAction("List", new {OrderBy = TempData["orderBy"] });
        }
    }
}
