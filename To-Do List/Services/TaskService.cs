using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using To_Do_List.Models.DTOs;
using To_Do_List.Models;
using AutoMapper;
using To_Do_List.Repository;

namespace To_Do_List.Services
{
    public class TaskService : ITaskService
    {
        private IMapper _mapper;
        private IToDoTasksRepository _repository;
        public TaskService(IToDoTasksRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ToDoTaskDTO>> GetTasksListByUser(ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            IEnumerable<ToDoTask> taskList = await _repository.GetUserTasksAsync(userId);

            return _mapper.Map<IEnumerable<ToDoTaskDTO>>(taskList);
        }

        public async Task<IEnumerable<ToDoTaskDTO>> GetUnfinishedTasksListByUser(ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            IEnumerable<ToDoTask> taskList = await _repository.GetUnfinishedUserTasksAsync(userId);

            return _mapper.Map<IEnumerable<ToDoTaskDTO>>(taskList);
        }

        public async Task<IEnumerable<ToDoTaskDTO>> GetSortedListByUser(ClaimsPrincipal user, string orderBy, bool isUnfinished)
        {
            var taskDtoList = !isUnfinished ? await GetTasksListByUser(user) : await GetUnfinishedTasksListByUser(user);

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

            return taskDtoList;
        }


        public async Task AddTaskByUser(ClaimsPrincipal user, ToDoTaskDTO taskDTO)
        {
            var newTask = _mapper.Map<ToDoTask>(taskDTO);
            var userId = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            newTask.UserId = userId;

            await _repository.AddTaskAsync(newTask);
        }

        public async Task SetDoneByTaskId(int id)
        {
            await _repository.SetDoneAsync(id);
        }
    }
}
