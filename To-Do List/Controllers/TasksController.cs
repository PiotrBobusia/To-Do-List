using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using To_Do_List.Models;
using To_Do_List.Models.DTOs;

namespace To_Do_List.Controllers
{
    public class TasksController : Controller
    {
        private IMapper _mapper;
        public TasksController(IMapper mapper)
        {
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }

        [Authorize]
        public IActionResult AddTask()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddTask(ToDoTaskDTO taskDTO)
        {
            var newTask = _mapper.Map<ToDoTask>(taskDTO);
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            return View();
        }
    }
}
