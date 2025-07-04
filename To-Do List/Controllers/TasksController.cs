﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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

        public IActionResult Error()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> List(string orderBy = "", bool? isUnfinished = null)
        {

            if(isUnfinished is not null) TempData["isUnfinished"] = isUnfinished;
            else if (TempData["isUnfinished"] is null) TempData["isUnfinished"]  = false;

            var taskDtoList = await _taskService.GetSortedListByUser(User, orderBy, (bool)TempData["isUnfinished"]);

            TempData["orderBy"] = orderBy;

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
